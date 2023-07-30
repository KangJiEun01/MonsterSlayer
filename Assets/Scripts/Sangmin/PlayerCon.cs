using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCon : GenericSingleton<PlayerCon>
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
    public float RunTimer { get { return _runTimer; } }
    private bool _runToggle;

    private bool _canDash = true;
    private Vector3 _dirVector;
    float _moveDirX;
    float _moveDirZ;



    // ���� ����
    private bool _isRun = false;
    private bool _isGround = true;
    private bool _isCrouch = false;
    private bool _isIdle = true;
    private bool _helperClose = false;

    // �ɾ��� �� �󸶳� ������ �����ϴ� ����
    [SerializeField]
    private float _crouchPosY;
    private float _originPosY;
    private float _applyCrouchPosY;
    float m_HorizontalAngle, m_VerticalAngle;
    // �ΰ���
    [SerializeField]
    private float _lookSensitivity;
    [SerializeField]
    Transform CameraPosition;

    // ī�޶� �Ѱ�
    [SerializeField]
    private float _cameraRotationLimit;
    private float _currentCameraRotationX;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private Camera _camera;
    private Rigidbody _rig;
    private CapsuleCollider _collider;
    Animator _animator;
    AudioSource _audioSource;
    int walkI;
    float soundTimer;
    [Header("Sounds")]
    [SerializeField] AudioClip[] _walking;


    void Start()
    {
       
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // ������Ʈ �Ҵ�
        _collider = GetComponent<CapsuleCollider>();
        _rig = GetComponent<Rigidbody>();
        _animator = GenericSingleton<Gun>.Instance.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
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
        Rotate();
        OpenHelper();


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
            if(!_runToggle)GenericSingleton<UIBase>.Instance.GetComponent<UIBase>().OpenRunToggleUI();
            Running();
            _runTimer += Time.deltaTime;
            if (_runTimer > 2)
            {
                _runTimer = 0;
                _runToggle = true;
                GenericSingleton<UIBase>.Instance.GetComponent<UIBase>().CloseRunToggleUI();
            }
        }
        else if (_runToggle)  Running(); 
        
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
        if((_moveDirX != 0 || _moveDirZ != 0)&& !GenericSingleton<Gun>.Instance.GetComponent<Gun>().IsReload && !GenericSingleton<Gun>.Instance.GetComponent<Gun>().InAttack)
        {
            _animator.Play("Run");
            MovingSound(0.25f);
        }
        
    }

    // �޸��� ���
    private void RunningCancel()
    {
        GenericSingleton<UIBase>.Instance.GetComponent<UIBase>().CloseRunToggleUI();
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
            CameraPosition.localPosition = new Vector3(0, _posY, 0);
            if (count > 15)
                break;
            yield return null;
        }
        CameraPosition.localPosition = new Vector3(0, _applyCrouchPosY, 0);
    }

    private void Move()
    {
        _moveDirX = Input.GetAxisRaw("Horizontal");
        _moveDirZ = Input.GetAxisRaw("Vertical");
        if (_moveDirX == 0 && _moveDirZ == 0)
        {
            if (!GenericSingleton<Gun>.Instance.GetComponent<Gun>().IsReload && !GenericSingleton<Gun>.Instance.GetComponent<Gun>().InAttack)
            {
                _animator.Play("Idle");
            }
            _isIdle = true;
            _runToggle = false;
        }
        else
        {
            _isIdle = false;
            if (!_isRun && !GenericSingleton<Gun>.Instance.GetComponent<Gun>().IsReload && !GenericSingleton<Gun>.Instance.GetComponent<Gun>().InAttack)
            {
                _animator.Play("Walk");
                MovingSound(0.3f);
            }

        }
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        _dirVector = (_moveHorizontal + _moveVertical).normalized;
        Vector3 _velocity =  _dirVector * _speed;

        _rig.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    //private void CameraRotation()
    //{
    //    float _xRotation = Input.GetAxisRaw("Mouse Y");
    //    float _cameraRotationX = _xRotation * _lookSensitivity;
    //    _currentCameraRotationX -= _cameraRotationX;
    //    _currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, - _cameraRotationLimit, _cameraRotationLimit);

    //    _camera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0, 0);
    //}

    //private void CharacterRotation()
    //{
    //    float _yRotation = Input.GetAxisRaw("Mouse X");
    //    Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * _lookSensitivity;
    //    _rig.MoveRotation(_rig.rotation * Quaternion.Euler(_characterRotationY));
    //}
    void Rotate()
    {
        // Turn player
        float turnPlayer = Input.GetAxis("Mouse X") * _lookSensitivity;
        m_HorizontalAngle = m_HorizontalAngle + turnPlayer;

        if (m_HorizontalAngle > 360) m_HorizontalAngle -= 360.0f;
        if (m_HorizontalAngle < 0) m_HorizontalAngle += 360.0f;

        Vector3 currentAngles = transform.localEulerAngles;
        currentAngles.y = m_HorizontalAngle;
        transform.localEulerAngles = currentAngles;

        // Camera look up/down
        var turnCam = -Input.GetAxis("Mouse Y");
        turnCam = turnCam * _lookSensitivity;
        m_VerticalAngle = Mathf.Clamp(turnCam + m_VerticalAngle, -89.0f, 89.0f);
        currentAngles = CameraPosition.transform.localEulerAngles;
        currentAngles.x = m_VerticalAngle;
        CameraPosition.transform.localEulerAngles = currentAngles;
    }
    void OpenHelper()
    {
        RaycastHit hit;
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F)) && _helperClose)
        {
            Debug.Log("�κ�����");
            GenericSingleton<HelperAI>.Instance.GetComponent<Animator>().Play("open");
            GenericSingleton<UIBase>.Instance.GetComponent<UIBase>().CloseInvenUI();
            _helperClose = false;
            return;
        }
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, 5f, 1 << LayerMask.NameToLayer("Helper")))
        {
            if(!_helperClose)GenericSingleton<UIBase>.Instance.GetComponent<UIBase>().OpenInvenCheckUI();
            if (Input.GetKeyDown(KeyCode.F))
            {
                GenericSingleton<HelperAI>.Instance.GetComponent<Animator>().Play("close");
                _helperClose = true;
                Debug.Log("�κ�����");
                GenericSingleton<UIBase>.Instance.GetComponent<UIBase>().CloseInvenCheckUI();
                GenericSingleton<UIBase>.Instance.GetComponent<UIBase>().OpenInvenUI();
            }
        }else GenericSingleton<UIBase>.Instance.GetComponent<UIBase>().CloseInvenCheckUI();
    }
    private void MovingSound(float delay)
    {
        soundTimer += Time.deltaTime;
        if (soundTimer > delay)
        {
            soundTimer = 0;
            _audioSource.PlayOneShot(_walking[walkI], 1f);
            walkI++;
            if (walkI == 4) walkI = 0;
        }
    }
}





