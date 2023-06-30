using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    // 스피드 조정 변수
    [Header("플레이어 스탯")]
    [SerializeField] private float _hp;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _crouchSpeed;
    [SerializeField] private float _dashTimer;
    private float _speed;
    

    // 점프, 대쉬 정도
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashCool;


    //상태관리변수
    private float _runTimer;
    private bool _runToggle;
    
    private bool _canDash = true;
    private Vector3 _dirVector;
    
    

    // 상태 변수
    private bool _isRun = false;
    private bool _isGround = true;
    private bool _isCrouch = false;
    private bool _isIdle = true;

    // 앉았을 때 얼마나 앉을지 결정하는 변수
    [SerializeField]
    private float _crouchPosY;
    private float _originPosY;
    private float _applyCrouchPosY;

    // 민감도
    [SerializeField]
    private float _lookSensitivity;

    // 카메라 한계
    [SerializeField]
    private float _cameraRotationLimit;
    private float _currentCameraRotationX;

    // 필요한 컴포넌트
    [SerializeField]
    private Camera _camera;
    private Rigidbody _rig;
    private CapsuleCollider _collider;
    [SerializeField] Animator _animator;


    void Start()
    {
       
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // 컴포넌트 할당
        _collider = GetComponent<CapsuleCollider>();
        _rig = GetComponent<Rigidbody>();

        // 초기화
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

   
    // 지면 체크
    private void IsGround()
    {
        _isGround = Physics.Raycast(transform.position, Vector3.down, _collider.bounds.extents.y + 0.1f);
    }

    // 점프 시도
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            Jump();
        }
    }

    // 점프
    private void Jump()
    {
        if (_isCrouch)  //웅크릴때 점프 x
            Crouch();

        _rig.velocity = transform.up * _jumpForce;
    }

    // 달리기 시도
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
        
        if ((Input.GetKeyUp(KeyCode.LeftShift) && !_runToggle) || _isIdle) //달리기 토글이꺼져있거나 멈추면 달리기 끄기 
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
                Debug.Log("대쉬 쿨 입니다");
            }
            
        }
    }
    void DashTimer()
    {
        _canDash = true;
    }
    // 달리기
    private void Running()
    {
        if (_isCrouch)
            Crouch();

        _isRun = true;
        _speed = _runSpeed;
        _animator.Play("Run");
    }

    // 달리기 취소
    private void RunningCancel()
    {
        _isRun = false;
        _speed = _walkSpeed;
        _runTimer = 0;
    }

    // 앉기 시도
    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    // 앉기 동작
    private void Crouch()
    {
        _isCrouch = !_isCrouch;  //토글식
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

    // 부드러운 앉기 동작
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





