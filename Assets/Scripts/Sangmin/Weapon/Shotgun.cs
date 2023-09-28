using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerCon;

public class Shotgun : HitScan
{
    [SerializeField] float _reloadDelay = 1.2f;
    [SerializeField] float _spread = 0.05f;

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
            _recoil.RecoilFire(_recoilForce); //반동
            int spread = 0;
            while (spread < 10)
            {
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward + new Vector3(Random.Range(-_spread, _spread), Random.Range(-_spread, _spread), Random.Range(-_spread, _spread)), out hit, 15f))
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
                        _currentBullet.transform.parent = hit.transform;
                        IndexCheck();
                    }
                }
                spread++;
            } 
           
            Invoke("StopAttack", _attackSpeed);
            if(_currentIdx == 0)
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
    public override IEnumerator Reload()
    {
        _animator.Play("Recharge_beginning");
        for (float f = 0.6f; f > 0; f -= 0.1f)
        {

            yield return new WaitForSeconds(0.1f);

        }
        for (int i = _currentIdx; i < _maxBullet; i++)
        {
            _currentIdx++;
            GenericSingleton<UIBase>.Instance.SetCurrentBullet(_currentIdx);
            _animator.Play("Recharge");
            _audioSource.PlayOneShot(_reloadSound, 1f);
            for (float f = 0.7f; f > 0; f -= 0.1f)
            {
                
                yield return new WaitForSeconds(0.1f);

            }
        }
        _animator.Play("Idle");
        for (float f = 0.2f; f > 0; f -= 0.1f)
        {
            
            yield return new WaitForSeconds(0.1f);

        }


        Debug.Log("장전완료");
        _isReload = false;
        GenericSingleton<UIBase>.Instance.SetCurrentBullet(_currentIdx);
    }
    void StopAttack()
    {
     
        inAttack = false;
    }
}
