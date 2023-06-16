using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //공격 관련 변수
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _bulletSpeed;

    // 총알 발사 관리 변수
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _firePosition;
    [SerializeField] Transform _targetPosition;
    [SerializeField] GameObject _bulletParent;
    
    bool inAttack = false;
    GameObject[] _bulletPool;
    int _poolIndex;
    int _invokeIdx;
    void Start()
    {
        InstBullet();
    }

    void Update()
    {
        Fire();
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
    void Fire()
    {
        if (Input.GetButton("Fire1") && inAttack == false)
        {
            Rigidbody rb = _bulletPool[_poolIndex].GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            _bulletPool[_poolIndex].SetActive(true);
            _bulletPool[_poolIndex].transform.rotation =  Quaternion.Euler(Vector3.forward);
            _bulletPool[_poolIndex++].transform.position = _firePosition.position + _firePosition.forward;
            rb.gameObject.GetComponent<BulletCon>().Init();
            IndexCheck();
            transform.LookAt(_targetPosition);
            rb.AddForce(transform.forward * _bulletSpeed,ForceMode.Impulse);
            inAttack = true;
            Invoke("StopAttack", _attackSpeed);
        }

    }
    void StopAttack()
    {
        inAttack = false;
    }
}