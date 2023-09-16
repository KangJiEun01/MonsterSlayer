using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class WeaponBase : GenericSingleton<WeaponBase>
//{
//    // Start is called before the first frame update

//    [SerializeField] protected float _attackSpeed = 0.167f;
//    [SerializeField] protected float _attackDamage = 1;
//    [SerializeField] protected float _impactForce = 30;
//    [SerializeField] protected float _bulletSpeed = 50;
//    [SerializeField] protected float _spreadAngle = 0;
//    // 총알 발사 관리 변수
//    [SerializeField] protected float _recoil;
//    [SerializeField] protected GameObject _bulletHole;
//    [SerializeField] protected Transform _firePosition;
//    [SerializeField] protected float reloadTime = 3.5f;
//    [SerializeField] protected GameObject _bulletParent;
   
  

//    protected GameObject currentBullet;
//    //재장전 관리 변수
//    [SerializeField] protected int _maxBullet;
//    protected int _currentIdx;
//    protected bool _isReload;
//    public bool IsReload { get { return _isReload; } }
//    protected bool inAttack = false;
//    public bool InAttack { get { return inAttack; } }
//    protected GameObject[] _bulletPool;
//    protected int _poolIndex;
//    protected float aimTime;
//    protected int _invokeIdx;
//    protected Vector3 _target;
//    protected Vector3 ScreenCenter;
//    protected Ray ray1;
//    protected Recoil recoil;
//    protected Animator _animator;
//    protected ParticleSystem _effect;
//    protected AudioSource audioSource;
//    [Header("Sound")]
//    [SerializeField] protected  AudioClip[] _shotSound;
//    [SerializeField] protected  AudioClip _reloadSound;
//    private void Start()
//    {
//        _animator = GetComponent<Animator>();
//        _effect = GetComponentInChildren<ParticleSystem>();
//        audioSource = GetComponent<AudioSource>();
//        recoil = GenericSingleton<Recoil>.Instance.GetComponent<Recoil>();
//        OnStart();
//        _currentIdx = _maxBullet;
//    }
//    protected virtual void OnStart()
//    {

//    }
//    private void Update()
//    {
//        OnUpdate();
//    }
//    protected virtual void OnUpdate()
//    {

//    }

//}
