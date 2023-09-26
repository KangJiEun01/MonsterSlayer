using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : GenericSingleton<WeaponManager>
{
    WeaponBase[] _weapons;
    
    protected List<WeaponBase> _activeWeapons;
    WeaponBase[] _currentWeapons = new WeaponBase[3];
    WeaponBase _currentWeapon;
    int _currentIdx;  //현재 들고있는 무기가 주/보조/근접 무기인지 판별
    public WeaponBase CurrentWeapon { get { return _currentWeapon; } }

    [SerializeField] GameObject _syringe;
    bool _isHeal;
    bool _isSwap;
    public bool IsSwap { get { return _isSwap; } }

    
    //_weapons 전체무기 
    //AcitiveWeapons 언락된 무기
    //CurrentWeapons[] 현재장착중인 무기 주,보조,근접
    //CurrentWeapon 현재 들고있는 무기


    void Update()
    {
        if (GenericSingleton<GameManager>.Instance.CurrentState != GameManager.GameState.InGame) return;
        if (!_isHeal)
        {
            _currentWeapon.OnUpdate();
            SwapWeapon();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            AllOff();
            _isHeal = true;
            _syringe.SetActive(true);
            _syringe.GetComponent<Animator>().Play("First_Aid");
            Invoke("SyringeOff",3.5f);
        }
    }
    void SyringeOff()
    {
        _isHeal = false;
        _syringe.SetActive(false);
        _currentWeapon.Weapon.SetActive(true);
        _currentWeapon.Animator.Play("Get");
    }
    public void Init()
    {
        _weapons = GetComponentsInChildren<WeaponBase>();
        _activeWeapons = new List<WeaponBase> ();
        foreach (WeaponBase weapon in _weapons)
        {
            weapon.Init();
            weapon.Weapon.SetActive(false);
        }

        _currentWeapons[1] = _activeWeapons[1];                //권총기본무기설정
        _currentWeapons[2] = _activeWeapons[0];                //근접기본무기설정
        _currentIdx = 1;
        _currentWeapon = _currentWeapons[_currentIdx];                   //권총 들기
        _currentWeapon.Weapon.SetActive(true);
        GenericSingleton<UIBase>.Instance.WeaponUI.GetComponent<WeaponUI>().UIUpdate(_currentWeapon);
        //AllUnlock();                                //테스트용 모두 잠금해제
    }
    void SwapWeapon()
    {

        switch (Input.inputString)
        {
            case "1":
                _currentIdx = 0;
                if (_currentWeapons[_currentIdx] != null)
                {
                    if(_currentWeapon != _currentWeapons[_currentIdx])
                    {
                        _currentWeapon = _currentWeapons[_currentIdx];
                        SetCurrentWeapon(_currentWeapon);
                    }                
                }
                break;

            case "2":
                _currentIdx = 1;
                if (_currentWeapon != _currentWeapons[_currentIdx])
                {
                    _currentWeapon = _currentWeapons[_currentIdx];
                    SetCurrentWeapon(_currentWeapon);
                }
                break;

            case "3":
                _currentIdx = 2;
                if (_currentWeapon != _currentWeapons[_currentIdx])
                {
                    _currentWeapon = _currentWeapons[_currentIdx];
                    SetCurrentWeapon(_currentWeapon);
                }
                break;
        }
    }
    void AllOff()   //모든 무기 끄기
    {
        foreach (WeaponBase weapon in _weapons)
        {
            weapon.Weapon.SetActive(false);
        }
    }
    public void SetDefaultWeapon(WeaponBase weapon)   //기본무기 초기화
    {
        _activeWeapons.Add(weapon);
    }
    public void UnlockWeapon(ItemData item)   //교환시 아이템 끄기
    {
   
        foreach(WeaponBase weapon in _weapons)
        {
            if(weapon.WeaponIdx == item.Idx - 7)
            {
                _activeWeapons.Add(weapon);
                GenericSingleton<UIBase>.Instance.WeaponSelectUI.GetComponent<WeaponSelectUI>().WeaponUnlock(weapon.WeaponIdx - 2);
            }
        }

    }
    public void SetCurrentWeapon(WeaponBase weapon)
    {
        AllOff();
        weapon.Weapon.SetActive(true);
        GenericSingleton<PlayerCon>.Instance.AnimatorUpdate();
        _isSwap = true;
        Invoke("SwapEnd",1f);
        weapon.Animator.Play("Get");
        GenericSingleton<UIBase>.Instance.WeaponUI.GetComponent<WeaponUI>().UIUpdate(weapon);
    }
    void SwapEnd()
    {
        _isSwap = false;
    }
    public void SetMainWeapon(int idx)
    {
        foreach (WeaponBase weapon in _activeWeapons)
        {
            Debug.Log(weapon.name);
            if (weapon.WeaponIdx == idx)
            {
                _currentWeapons[0] = weapon;
                Debug.Log($"{idx}번 무기 장착완료");
            }
        }
        if (_currentIdx == 0)
        {
            _currentWeapon = _currentWeapons[_currentIdx];
            SetCurrentWeapon(_currentWeapon);
        }
    }
    void AllUnlock()
    {
        foreach (WeaponBase weapon in _weapons)
        {
            if (!_activeWeapons.Contains(weapon))
            {
                _activeWeapons.Add(weapon);
                GenericSingleton<UIBase>.Instance.WeaponSelectUI.GetComponent<WeaponSelectUI>().WeaponUnlock(weapon.WeaponIdx - 2);
            }
        }
        }
    }
    

public abstract class WeaponBase :MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] protected float _attackSpeed = 0.167f;
    [SerializeField] protected float _attackDamage = 1;
    [SerializeField] protected int _maxBullet;
    public int MaxBullet { get { return _maxBullet; } }
    [SerializeField] protected float _impactForce = 30;
    [SerializeField] protected float _recoilForce;
    [SerializeField] protected GameObject _weapon;
    [SerializeField] protected float _reloadTime = 3.5f;

    [SerializeField] int _weaponIdx;
    public int WeaponIdx { get { return _weaponIdx; } }
    public GameObject Weapon { get { return _weapon; } }
    protected int _currentIdx;
    public int CurrentIdx { get { return _currentIdx; } }
    protected bool _isReload = false;
    public bool IsReload { get { return _isReload; } }
    protected bool inAttack = false;
    public bool InAttack { get { return inAttack; } }
    protected WeaponBase[] _currentWeapons;
    public WeaponBase[] CurrentWeapons { get { return _currentWeapons; } }
    protected Animator _animator;
    public Animator Animator { get { return _animator; } }
    protected ParticleSystem _effect;
    public ParticleSystem Effect { get { return _effect; } }

    protected AudioSource _audioSource;
    public AudioSource AudioSource { get { return _audioSource; } }

    protected Recoil _recoil;
    public Recoil Recoil { get {  return _recoil; } }
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
    public virtual IEnumerator Reload()
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
        GenericSingleton<UIBase>.Instance.WeaponUI.GetComponent<WeaponUI>().SetCurrentBullet(_currentIdx);
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
    [SerializeField] protected float _bulletSpeed;

    
   
    protected GameObject _currentBullet;

    protected GameObject[] _bulletPool;
    protected int _poolIndex;
    protected float _aimTime;
    public void IndexCheck()
    {
        if (_poolIndex == 50) _poolIndex = 0;
    }
    public void InstBullet()
    {
        _bulletPool = new GameObject[50];
        for (int i = 0; i < _bulletPool.Length; i++)
        {
            GameObject gameObject = Instantiate(_bullet, _bulletParent.transform);
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

        if (Input.GetMouseButton(0))
        {
           if (inAttack == false) Fire();

        }

        if (Input.GetKeyDown(KeyCode.R) && _currentIdx < _maxBullet && !_isReload)
        {
            _isReload = true;
            StartCoroutine(Reload());
        }

    }

}
public abstract class Melee : WeaponBase
{
    [SerializeField] protected GameObject _hitEffect;
    [SerializeField] protected float _attackRange;
    public override void OnUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (inAttack == false) Fire();
        }
    }
}
