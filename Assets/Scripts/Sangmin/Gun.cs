using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.VFX;

public class Gun : GenericSingleton<Gun>
{
    //공격 관련 변수

    [SerializeField] private float _attackSpeed = 0.167f;
    [SerializeField] private float _attackDamage = 1;
    [SerializeField] private float _impactForce = 30;
    [SerializeField] private float _bulletSpeed = 50;
    [SerializeField] private float _spreadAngle = 0;
    // 총알 발사 관리 변수
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _bulletHole;
    [SerializeField] Transform _firePosition;
    [SerializeField] float reloadTime = 3.5f;
    [SerializeField] GameObject _bulletParent;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject[] _crosshair;
    GameObject currentBullet;
    //재장전 관리 변수
    [SerializeField] int _maxBullet;
    int _currentBullet;
    bool _isReload;
    public bool IsReload { get { return _isReload; } }
    bool inAttack = false;
    public bool InAttack { get { return inAttack; } }
    GameObject[] _bulletPool;
    int _poolIndex;
    float aimTime;
    int _invokeIdx;
    Vector3 _target;
    Vector3 ScreenCenter;
    Ray ray1;
    Recoil recoil;
    Animator _animator;
    ParticleSystem _effect;
    AudioSource audioSource;
    [Header("Sound")]
    [SerializeField] AudioClip _shotSound;
    [SerializeField] AudioClip _reloadSound;

    void Start()
    {   
        _animator = GetComponent<Animator>();
        _effect = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        recoil = GenericSingleton<Recoil>.Instance.GetComponent<Recoil>();
        InstBullet();     
        _currentBullet = _maxBullet;
    }
 

    void Update()
    {
        ray1 = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray1, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Target")))
        {
            _target = hit.point;
        }
        if (Input.GetMouseButton(0))
        {
            aimTime += Time.deltaTime;
            if(inAttack ==false) RaycastShot();
           // Fire();
        }
        if (Input.GetMouseButtonUp(0))
        {
            aimTime = 0;
           AimReturn();
        }
        if (Input.GetKeyDown(KeyCode.R) && _currentBullet < _maxBullet && !_isReload)
        {
            _isReload = true;
            StartCoroutine(ReloadBullet());
        }
       
    }

    //void InstBullet()
    //{
    //    _bulletPool = new GameObject[100];
    //    for (int i = 0; i < _bulletPool.Length; i++)
    //    {
    //        GameObject gameObject = Instantiate(_bullet, _bulletParent.transform);
    //        _bulletPool[i] = gameObject;
    //        gameObject.SetActive(false);
    //    }
    //}
    void IndexCheck()
    {
        if (_poolIndex == 100) _poolIndex = 0;
    }
    void InstBullet()
    {
        _bulletPool = new GameObject[100];
        for (int i = 0; i < _bulletPool.Length; i++)
        {
            GameObject gameObject = Instantiate(_bulletHole, _bulletParent.transform);
            _bulletPool[i] = gameObject;
            gameObject.SetActive(false);
        }
    }
    void AimOpen()
    {
        foreach (var aim in _crosshair)
        {
            aim.transform.Translate(Vector3.up * 0.1f);
        }
    }
    void AimReturn()
    {
        foreach (GameObject aim in _crosshair)
        {
            aim.transform.localPosition =new Vector3(0, 0.4f, 15.0f);
        }
    }
    IEnumerator ReloadBullet()
    {
        _animator.Play("Reload");
        audioSource.PlayOneShot(_reloadSound,1f);
        for(float f = reloadTime; f > 0; f-= 0.1f)
        {
            Debug.Log("장전중입니다");
            yield return new WaitForSeconds(0.1f);
            
        }
        Debug.Log("장전완료");
        _isReload = false;
        _currentBullet = _maxBullet;
    }
    //void Fire()
    //{
    //    if (_currentBullet > 0 && !_isReload)
    //    {
    //        inAttack = true;
    //        _animator.Play("Shot");
    //        ray2 = new Ray(transform.position, _target);
    //        _currentBullet--;
    //        Debug.Log("현재 장탄수 : " + _currentBullet);
    //        Rigidbody rb = _bulletPool[_poolIndex].GetComponent<Rigidbody>();
    //        rb.velocity = Vector3.zero;
    //        _bulletPool[_poolIndex].SetActive(true);
    //        _bulletPool[_poolIndex].transform.rotation = Quaternion.LookRotation(transform.right);
    //        _bulletPool[_poolIndex++].transform.position = _firePosition.position + _firePosition.forward;
    //        rb.gameObject.GetComponent<BulletCon>().Init();
    //        IndexCheck();
    //        //transform.rotation = Quaternion.LookRotation(ray2.direction);
    //        Vector3 dir =  _target - _firePosition.position;
    //        rb.AddForce(dir * _bulletSpeed, ForceMode.Impulse);
    //        Invoke("StopAttack", _attackSpeed);
    //    }
    //    else if(_currentBullet <= 0 && !_isReload)
    //    {
    //        _isReload = true;
    //        StartCoroutine(ReloadBullet());
    //    }

    //}
    void RaycastShot()
    {
        if (_currentBullet > 0 && !_isReload)
        {
            RaycastHit hit;
            _currentBullet--;
            _effect.Play();
            audioSource.PlayOneShot(_shotSound, 1f);
            _animator.Play("Shot");
            inAttack = true;
            if(aimTime > 0.3f)
            {
                recoil.RecoilFire(); //반동
                AimOpen();           // aim벌어짐
            }
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
            {
                Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();
                target?.OnDamage(_attackDamage);
                hit.rigidbody?.AddForce(-hit.normal * _impactForce);
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    currentBullet = _bulletPool[_poolIndex++];
                    currentBullet.SetActive(true);
                    currentBullet.transform.rotation = Quaternion.LookRotation(hit.normal);
                    currentBullet.transform.position = hit.point;
                    IndexCheck();
                }
                
            }           
            Invoke("StopAttack", _attackSpeed);
        }
        else if (_currentBullet <= 0 && !_isReload)
        {
            _isReload = true;
            StartCoroutine(ReloadBullet());
        }

    }

    void StopAttack()
    {
        inAttack = false;
    }
    //void Shot()
    //{
    //    float angle = Random.Range(0.0f, _spreadAngle * 0.5f);
    //    Vector2 angleDir = Random.insideUnitCircle * Mathf.Tan(angle * Mathf.Deg2Rad);

    //    Vector3 dir = Endpoint.transform.forward + (Vector3)angleDir;
    //    dir.Normalize();
    //    _bulletPool[_poolIndex].gameObject.SetActive(true);
    //    _bulletPool[_poolIndex].Launch(dir, 200);
    //}
    //void Launch(Vector3 direction, float force)
    //{
    //    m_Owner = launcher;

    //    transform.position = launcher.GetCorrectedMuzzlePlace();
    //    transform.forward = launcher.EndPoint.forward;

    //    gameObject.SetActive(true);
    //    m_TimeSinceLaunch = 0.0f;
    //    m_Rigidbody.AddForce(direction * force);
    //}
}