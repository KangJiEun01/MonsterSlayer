
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Crossbow : Projectile
{


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
            _currentBullet.transform.rotation = Quaternion.LookRotation(Camera.main.transform.up);
            _currentBullet.transform.position = _firePosition.position;
            _currentBullet.GetComponent<BoxCollider>().enabled = true;
            _currentBullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * _bulletSpeed, ForceMode.Impulse);
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

