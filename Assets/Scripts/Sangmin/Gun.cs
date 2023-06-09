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
    
    GameObject[] _bulletPool;
    int _poolIndex;
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
        if (Input.GetButtonDown("Fire1"))
        {

            Vector3 fireP = new Vector3(_firePosition.position.x, _firePosition.position.y, _firePosition.position.z);
            _bulletPool[_poolIndex].SetActive(true);      
            _bulletPool[_poolIndex].transform.position = fireP;
            Rigidbody rb = _bulletPool[_poolIndex++].GetComponent<Rigidbody>();
            transform.LookAt(_targetPosition);
            rb.AddForce(transform.forward * _bulletSpeed,ForceMode.Impulse);
            IndexCheck();
        }
    }
}