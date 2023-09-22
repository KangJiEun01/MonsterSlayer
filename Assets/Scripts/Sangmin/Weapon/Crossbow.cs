
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Crossbow : Projectile
{
    public override void Init()
    {
        base.Init();
        GenericSingleton<WeaponManager>.Instance.UnlockWeapon(this);
    }

    public override void Fire()
    {
        if (_currentIdx > 0 && !_isReload)
        {
           
            _currentIdx--;
            _animator.Play("Shot");
            _audioSource.PlayOneShot(_shotSound[Random.Range(0, _shotSound.Length)], 1f);

            inAttack = true;
            _currentBullet = _bulletPool[_poolIndex++];
            _currentBullet.SetActive(true);
            Vector3 dirVector = Camera.main.transform.forward;
            Debug.Log(Camera.main.transform.up);
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 50f))
            {
                 dirVector = hit.point - _firePosition.position;
            }
            _currentBullet.transform.position = _firePosition.position;
            _currentBullet.GetComponent<BoxCollider>().enabled = true;
            _currentBullet.transform.rotation = Quaternion.LookRotation(dirVector);
            _currentBullet.GetComponent<Rigidbody>().AddForce(dirVector.normalized * _bulletSpeed, ForceMode.Impulse);
           
            IndexCheck();
            Invoke("StopAttack", _attackSpeed);
        }
        else if (_currentIdx <= 0 && !_isReload)
        {
            _isReload = true;
            StartCoroutine(Reload());
        }
        GenericSingleton<UIBase>.Instance.WeaponUI.GetComponent<WeaponUI>().SetCurrentBullet(_currentIdx);
    }

    void StopAttack()
    {
        inAttack = false;
    }

}

