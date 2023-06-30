using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //공격 관련 변수
    [SerializeField] private float _attackSpeed = 0.1F;
    [SerializeField] private float _attackDamage = 1;
    [SerializeField] private float _bulletSpeed = 50;

    // 총알 발사 관리 변수
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _firePosition;
    [SerializeField] float reloadTime = 1;
    Transform _targetPosition;
    [SerializeField] GameObject _bulletParent;

    //재장전 관리 변수
    [SerializeField] int _maxBullet;
    int _currentBullet;
    bool _isReload;
    bool inAttack = false;
    GameObject[] _bulletPool;
    int _poolIndex;
    int _invokeIdx;
    Vector3 _target;
    Vector3 ScreenCenter;
    Ray ray1;
    Ray ray2;
    Animator _animator;


    void Start()
    {   
        _animator = GetComponent<Animator>();
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
        if (Input.GetMouseButton(0) && inAttack == false)
        {
            Fire();
        }
        if (Input.GetKeyDown(KeyCode.R) && _currentBullet < _maxBullet && !_isReload)
        {
            _isReload = true;
            StartCoroutine(ReloadBullet());
        }
    }

    void InstBullet()
    {
        _bulletPool = new GameObject[100];
        for (int i = 0; i < _bulletPool.Length; i++)
        {
            GameObject gameObject = Instantiate(_bullet, _bulletParent.transform);
            _bulletPool[i] = gameObject;
            gameObject.SetActive(false);
        }
    }
    void IndexCheck()
    {
        if (_poolIndex == 100) _poolIndex = 0;
    }

    IEnumerator ReloadBullet()
    {
        _animator.Play("Reload");
        for(float f = reloadTime; f > 0; f-= 0.1f)
        {
            Debug.Log("장전중입니다");
            yield return new WaitForSeconds(0.1f);
            
        }
        Debug.Log("장전완료");
        _isReload = false;
        _currentBullet = _maxBullet;
    }
    void Fire()
    {
        if (_currentBullet > 0 && !_isReload)
        {
            Debug.Log(_target);
            ray2 = new Ray(transform.position, _target);
            _currentBullet--;
            Debug.Log("현재 장탄수 : " + _currentBullet);
            Rigidbody rb = _bulletPool[_poolIndex].GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            _bulletPool[_poolIndex].SetActive(true);
            _bulletPool[_poolIndex].transform.rotation = Quaternion.LookRotation(transform.forward);
            _bulletPool[_poolIndex++].transform.position = _firePosition.position + _firePosition.forward;
            rb.gameObject.GetComponent<BulletCon>().Init();
            IndexCheck();
            transform.rotation = Quaternion.LookRotation(ray2.direction);
            rb.AddForce(transform.forward * _bulletSpeed, ForceMode.Impulse);
            inAttack = true;
            Invoke("StopAttack", _attackSpeed);
        }
        else if(_currentBullet <= 0 && !_isReload)
        {
            _isReload = true;
            StartCoroutine(ReloadBullet());
        }

    }
    void StopAttack()
    {
        inAttack = false;
    }
}