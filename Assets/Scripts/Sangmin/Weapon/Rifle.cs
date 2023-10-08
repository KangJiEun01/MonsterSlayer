
using UnityEngine;
using UnityEngine.VFX;

public class Rifle : HitScan
{  
    public override void Fire()
    {
        if (_currentIdx > 0 && !_isReload)
        {
            RaycastHit hit;
            _currentIdx--;
            _effect.Play();
            _audioSource.PlayOneShot(_shotSound[Random.Range(0,_shotSound.Length)], 1f);
            _animator.Play("Shot");
            inAttack = true;
       
            _recoil.RecoilFire(_recoilForce); //¹Ýµ¿
            
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 50f))
            {
                Target target = hit.transform.GetComponent<Target>();
                target?.OnDamage(_attackDamage);
                hit.rigidbody?.AddForce(-hit.normal * _impactForce);

                GameObject temp = Instantiate(_bulletHole);
                temp.transform.rotation = Quaternion.LookRotation(hit.normal);
                temp.transform.position = hit.point + hit.normal * 0.1f;
                temp.GetComponent<VisualEffect>()?.SendEvent("Shot");
                Destroy(temp, 2f);
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
        GenericSingleton<UIBase>.Instance.SetCurrentBullet(_currentIdx);
    }

    void StopAttack()
    {
      
        inAttack = false;
    }
   
}