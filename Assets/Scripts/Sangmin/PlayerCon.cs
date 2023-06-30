using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    // ���ǵ� ���� ����
    [Header("�÷��̾� ����")]
    [SerializeField] private float _hp;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _crouchSpeed;
    [SerializeField] private float _dashTimer;
    private float _speed;
    

    // ����, �뽬 ����
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashCool;


    //���°�������
    private float _runTimer;
    private bool _runToggle;
    
    private bool _canDash = true;
    private Vector3 _dirVector;
    
    

    // ���� ����
    private bool _isRun = false;
    private bool _isGround = true;
    private bool _isCrouch = false;
    private bool _isIdle = true;

    // �ɾ��� �� �󸶳� ������ �����ϴ� ����
    [SerializeField]
    private float _crouchPosY;
    private float _originPosY;
    private float _applyCrouchPosY;

    // �ΰ���
    [SerializeField]
    private float _lookSensitivity;

    // ī�޶� �Ѱ�
    [SerializeField]
    private float _cameraRotationLimit;
    private float _currentCameraRotationX;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private Camera _camera;
    private Rigidbody _rig;
    private CapsuleCollider _collider;
    [SerializeField] Animator _animator;


    void Start()
    {
       
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // ������Ʈ �Ҵ�
        _collider = GetComponent<CapsuleCollider>();
        _rig = GetComponent<Rigidbody>();

        // �ʱ�ȭ
        _speed = _walkSpeed;

        _originPosY = _camera.transform.localPosition.y;
        _applyCrouchPosY = _originPosY;
    }

    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        TryDash();
        TryCrouch();
        Move();
        CameraRotation();
        CharacterRotation();
        
    }

   
    // ���� üũ
    private void IsGround()
    {
        _isGround = Physics.Raycast(transform.position, Vector3.down, _collider.bounds.extents.y + 0.1f);
    }

    // ���� �õ�
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            Jump();
        }
    }

    // ����
    private void Jump()
    {
        if (_isCrouch)  //��ũ���� ���� x
            Crouch();

        _rig.velocity = transform.up * _jumpForce;
    }

    // �޸��� �õ�
    private void TryRun()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
            _runTimer += Time.deltaTime;
            if (_runTimer > 2)
            {
                _runTimer = 0;
                _runToggle = true;
            }
        }
        
        if ((Input.GetKeyUp(KeyCode.LeftShift) && !_runToggle) || _isIdle) //�޸��� ����̲����ְų� ���߸� �޸��� ���� 
        {  
            RunningCancel();
        }

    }
    private void TryDash()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_canDash)
            {
                _canDash = false;
                Invoke("DashTimer", _dashCool); ;
                _rig.velocity = _dirVector * _dashForce;
            }
            else
            {
                Debug.Log("�뽬 �� �Դϴ�");
            }
            
        }
    }
    void DashTimer()
    {
        _canDash = true;
    }
    // �޸���
    private void Running()
    {
        if (_isCrouch)
            Crouch();

        _isRun = true;
        _speed = _runSpeed;
        _animator.Play("Run");
    }

    // �޸��� ���
    private void RunningCancel()
    {
        _isRun = false;
        _speed = _walkSpeed;
        _runTimer = 0;
    }

    // �ɱ� �õ�
    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    // �ɱ� ����
    private void Crouch()
    {
        _isCrouch = !_isCrouch;  //��۽�
        if (_isCrouch)
        {
            _speed = _crouchSpeed;
            _applyCrouchPosY = _crouchPosY;
        }
        else
        {
            _speed = _walkSpeed;
            _applyCrouchPosY = _originPosY;
        }

        StartCoroutine(CrouchCoroutine());
    }

    // �ε巯�� �ɱ� ����
    IEnumerator CrouchCoroutine()
    {
        float _posY = _camera.transform.localPosition.y;
        int count = 0;

        while (_posY != _applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, _applyCrouchPosY, 0.2f);
            _camera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
                break;
            yield return null;
        }
        _camera.transform.localPosition = new Vector3(0, _applyCrouchPosY, 0);
    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        if (_moveDirX == 0 && _moveDirZ == 0)
        {
            _animator.Play("Idle");
            _isIdle = true;
            _runToggle = false;
        }
        else
        {
            _isIdle = false;
            if (!_isRun)
            {
                _animator.Play("Walk");
            }

        }
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        _dirVector = (_moveHorizontal + _moveVertical).normalized;
        Vector3 _velocity =  _dirVector * _speed;

        _rig.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * _lookSensitivity;

        _currentCameraRotationX -= _cameraRotationX;
        _currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, - _cameraRotationLimit, _cameraRotationLimit);

        _camera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * _lookSensitivity;
        _rig.MoveRotation(_rig.rotation * Quaternion.Euler(_characterRotationY));
    }
}





