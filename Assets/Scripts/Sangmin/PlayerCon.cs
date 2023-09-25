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
    public bool RunToggle {  get { return _runToggle; } }

    //HpUi���� ����
    public float HpStat { get { return _hp; } }

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
    [SerializeField] float _startRotY;
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

    public void Init()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // ������Ʈ �Ҵ�
        _collider = GetComponent<CapsuleCollider>();
        _rig = GetComponent<Rigidbody>();
        Debug.Log(GenericSingleton<WeaponManager>.Instance.CurrentWeapon);
        _animator = GenericSingleton<WeaponManager>.Instance.CurrentWeapon.Animator;
        _audioSource = GetComponent<AudioSource>();
        // �ʱ�ȭ
        _speed = _walkSpeed;
        transform.localEulerAngles = new Vector3(0, _startRotY,0);
        _originPosY = _camera.transform.localPosition.y;
        _applyCrouchPosY = _originPosY;
    }


    void Update()
    {
        if (GenericSingleton<GameManager>.Instance.CurrentState != GameManager.GameState.InGame) return;
        IsGround();
        TryJump();
        TryRun();
        TryDash();
        TryCrouch();
        Rotate();
       // OpenHelper();

    }
    private void FixedUpdate()
    {
        if (GenericSingleton<GameManager>.Instance.CurrentState != GameManager.GameState.InGame) return;
        Move();
    }


    // ���� üũ
    private void IsGround()
    {
        Vector3 colliderOffset = _collider.bounds.center - transform.position;
        _isGround = Physics.Raycast(transform.position + colliderOffset, Vector3.down, _collider.bounds.extents.y + 0.1f);
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
            //if(!_runToggle)GenericSingleton<UIBase>.Instance.ShowRunToggleUI(true);
            Running();
            _runTimer += Time.deltaTime;
            if (_runTimer > 2)
            {
                _runTimer = 0;
                _runToggle = true;
                //GenericSingleton<UIBase>.Instance.ShowRunToggleUI(false);
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
        if((_moveDirX != 0 || _moveDirZ != 0)&& !GenericSingleton<WeaponManager>.Instance.CurrentWeapon.IsReload && !GenericSingleton<WeaponManager>.Instance.CurrentWeapon.InAttack)
        {
            _animator.Play("Run");
            MovingSound(0.25f);
        }
        
    }

    // �޸��� ���
    private void RunningCancel()
    {
        //GenericSingleton<UIBase>.Instance.ShowRunToggleUI(false);
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
            if (!GenericSingleton<WeaponManager>.Instance.CurrentWeapon.IsReload && !GenericSingleton<WeaponManager>.Instance.CurrentWeapon.InAttack)
            {
                _animator.Play("Idle");
            }
            _isIdle = true;
            _runToggle = false;
        }
        else
        {
            _isIdle = false;
            if (!_isRun && !GenericSingleton<WeaponManager>.Instance.CurrentWeapon.IsReload && !GenericSingleton<WeaponManager>.Instance.CurrentWeapon.InAttack)
            {
                _animator.Play("Walk");
                MovingSound(0.3f);
            }

        }
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        _dirVector = (_moveHorizontal + _moveVertical).normalized;
        Vector3 _velocity =  _dirVector * _speed;

        _rig.AddForce( _velocity);
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
    //void OpenHelper()
    //{
    //    RaycastHit hit;
    //    if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F)) && _helperClose)
    //    {
    //        Debug.Log("�κ�����");
    //        GenericSingleton<HelperAI>.Instance.GetComponent<Animator>().Play("open");
    //        GenericSingleton<UIBase>.Instance.ShowInvenUI(false);
    //        _helperClose = false;
    //        return;
    //    }
    //    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
    //    if (Physics.Raycast(ray, out hit, 5f, 1 << LayerMask.NameToLayer("Helper")))
    //    {
    //        if(!_helperClose)GenericSingleton<UIBase>.Instance.ShowInvenCheckUI(true);
    //        if (Input.GetKeyDown(KeyCode.F))
    //        {
    //            GenericSingleton<HelperAI>.Instance.GetComponent<Animator>().Play("close");
    //            _helperClose = true;
    //            Debug.Log("�κ�����");
    //            GenericSingleton<UIBase>.Instance.ShowInvenCheckUI(false);
    //            GenericSingleton<UIBase>.Instance.ShowInvenUI(true);
    //        }
    //    }else GenericSingleton<UIBase>.Instance.ShowInvenCheckUI(false);
    //}
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Boss"))
        {
            _hp -= 10;
        }
        if (collision.collider.CompareTag("Item"))
        {
            GenericSingleton<ItemSaver>.Instance.AddItem(collision.collider.GetComponent<Item>().GetItem());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            _hp -= 1;
        }
    }
}





