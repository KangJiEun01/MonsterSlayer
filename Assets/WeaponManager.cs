using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : GenericSingleton<WeaponManager>
{
    WeaponBase[] _weapons;
    WeaponBase _currentWeapon;
    public WeaponBase CurrentWeapon { get { return _currentWeapon; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        _currentWeapon.OnUpdate();
    }
    void Init()
    {
        
        _weapons = GetComponentsInChildren<WeaponBase>();
        foreach (WeaponBase weapon in _weapons)
        {
            weapon.Init();
            weapon.Weapon.SetActive(false);
        }
        _currentWeapon = _weapons[0];
        Debug.Log(_weapons[0]);
        _currentWeapon.Weapon.SetActive(true);
        Debug.Log(_currentWeapon._animator);
    }
}
public abstract class WeaponBase :MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] protected float _attackSpeed = 0.167f;
    [SerializeField] protected float _attackDamage = 1;
    [SerializeField] protected int _maxBullet;
    [SerializeField] protected float _impactForce = 30;
    [SerializeField] protected float _recoilForce;
    [SerializeField] protected GameObject _weapon;
    [SerializeField] protected float _reloadTime = 3.5f;
    public GameObject Weapon { get { return _weapon; } }
    protected int _currentIdx;
    protected bool _isReload = false;
    public bool IsReload { get { return _isReload; } }
    protected bool inAttack = false;
    public bool InAttack { get { return inAttack; } } 
    
    public Animator _animator;
    public ParticleSystem _effect;
    public AudioSource _audioSource;
    public Recoil _recoil;
    [Header("Sound")]
    [SerializeField] protected AudioClip[] _shotSound;
    [SerializeField] protected AudioClip _reloadSound;
    public virtual void  Init()
    {
      
        _animator = GetComponentInChildren<Animator>();
        
        _effect = GetComponentInChildren<ParticleSystem>();
        
        _audioSource = GetComponentInChildren<AudioSource>();
        _recoil = GenericSingleton<Recoil>.Instance.GetComponent<Recoil>();
        _currentIdx = _maxBullet;
    }
    public virtual void OnUpdate()
    {

    }
    public abstract void Fire();
    public IEnumerator Reload()
    {
        _animator.Play("Reload");
        _audioSource.PlayOneShot(_reloadSound, 1f);
        for (float f = _reloadTime; f > 0; f -= 0.1f)
        {
            Debug.Log("장전중입니다");
            yield return new WaitForSeconds(0.1f);

        }
        Debug.Log("장전완료");
        _isReload = false;
        _currentIdx = _maxBullet;
    }


}
public abstract class HitScan : WeaponBase
{
    [SerializeField] protected GameObject _bulletHole;
    [SerializeField] protected Transform _firePosition;
    [SerializeField] protected GameObject _bulletParent;


    
    protected float _aimTime;
    protected GameObject _currentBullet;

    protected GameObject[] _bulletPool;
    protected int _poolIndex;
    protected Vector3 _target;
    protected Ray ray1;
    public void IndexCheck()
    {
        if (_poolIndex == 100) _poolIndex = 0;
    }
    public void InstBullet()
    {
        _bulletPool = new GameObject[100];
        for (int i = 0; i < _bulletPool.Length; i++)
        {
            GameObject gameObject = Instantiate(_bulletHole, _bulletParent.transform);
            _bulletPool[i] = gameObject;
            gameObject.SetActive(false);
        }
    }
    public override void Init()
    {
        base.Init();
        InstBullet();
    }
    public override void OnUpdate()
    {
        ray1 = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray1, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Target")))
        {
            _target = hit.point;
        }
        if (Input.GetMouseButton(0))
        {
            _aimTime += Time.deltaTime;
            if (inAttack == false) Fire();
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            _aimTime = 0;
            
        }
        if (Input.GetKeyDown(KeyCode.R) && _currentIdx < _maxBullet && !_isReload)
        {
            _isReload = true;
            StartCoroutine(Reload());
        }

    }
}
public abstract class Projectile : WeaponBase
{
    
    [SerializeField] protected GameObject _bullet;
    [SerializeField] protected Transform _firePosition;
    [SerializeField] protected GameObject _bulletParent;

    
   
    protected GameObject _currentBullet;

    protected GameObject[] _bulletPool;
    protected int _poolIndex;
    protected float _aimTime;

}
public abstract class Melee : WeaponBase
{

}
