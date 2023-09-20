using System.Collections;
using UnityEngine;

public class Pistol : HitScan
{
    public override void Fire()
    {
        if (_currentIdx > 0 && !_isReload)
        {
            RaycastHit hit;
            _currentIdx--;
            _effect.Play();
            _audioSource.PlayOneShot(_shotSound[Random.Range(0, _shotSound.Length)], 1f);
            _animator.Play("Shot");
            inAttack = true;
            
             _recoil.RecoilFire(_recoilForce); //¹Ýµ¿ 
            
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
            {
                Debug.Log(hit.transform.name);
                Target target = hit.transform.GetComponent<Target>();
                target?.OnDamage(_attackDamage);
                hit.rigidbody?.AddForce(-hit.normal * _impactForce);
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    _currentBullet = _bulletPool[_poolIndex++];
                    _currentBullet.SetActive(true);
                    _currentBullet.transform.rotation = Quaternion.LookRotation(hit.normal);
                    _currentBullet.transform.position = hit.point + hit.normal * 0.1f;
                    IndexCheck();
                }

            }
            Invoke("StopAttack", _attackSpeed);
            if (_currentIdx == 0)
            {
                _isReload = true;
                StartCoroutine(Reload());
            }
        }
        else if (_currentIdx <= 0 && !_isReload)
        {
            _isReload = true;
            StartCoroutine(Reload());
        }

    }

    void StopAttack()
    {
        inAttack = false;
    }
   

}
