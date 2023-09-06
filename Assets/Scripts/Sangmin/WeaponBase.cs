using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : GenericSingleton<WeaponBase>
{
    // Start is called before the first frame update

    [SerializeField] protected float _attackSpeed = 0.167f;
    [SerializeField] protected float _attackDamage = 1;
    [SerializeField] protected float _impactForce = 30;
    [SerializeField] protected float _bulletSpeed = 50;
    [SerializeField] protected float _spreadAngle = 0;
    // �Ѿ� �߻� ���� ����

    [SerializeField] protected GameObject _bulletHole;
    [SerializeField] protected Transform _firePosition;
    [SerializeField] protected float reloadTime = 3.5f;
    [SerializeField] protected GameObject _bulletParent;
    [SerializeField] protected GameObject _player;
    [SerializeField] protected RectTransform _upCrosshair;
    [SerializeField] protected  RectTransform _rightCrosshair;
    [SerializeField] protected RectTransform _downCrosshair;
    [SerializeField] protected RectTransform _leftCrosshair;

    protected GameObject currentBullet;
    //������ ���� ����
    [SerializeField] protected int _maxBullet;
    protected int _currentIdx;
    protected bool _isReload;
    public bool IsReload { get { return _isReload; } }
    protected bool inAttack = false;
    public bool InAttack { get { return inAttack; } }
    protected GameObject[] _bulletPool;
    protected int _poolIndex;
    protected float aimTime;
    protected int _invokeIdx;
    protected Vector3 _target;
    protected Vector3 ScreenCenter;
    protected Ray ray1;
    protected Recoil recoil;
    protected Animator _animator;
    protected ParticleSystem _effect;
    protected AudioSource audioSource;
    [Header("Sound")]
    [SerializeField] protected  AudioClip _shotSound;
    [SerializeField] protected  AudioClip _reloadSound;

}