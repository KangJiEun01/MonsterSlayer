using System.Collections;
using UnityEngine;
using UnityEngine.VFX;


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
            GenericSingleton<UIBase>.Instance.SetCurrentBullet(_currentIdx);
            _effect.Play();
            _audioSource.PlayOneShot(_shotSound[Random.Range(0, _shotSound.Length)], 1f);
            _animator.Play("Shot");
            inAttack = true;
            _recoil.RecoilFire(_recoilForce); //반동
            int spread = 0;
            while (spread < 10)
            {
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward + new Vector3(Random.Range(-_spread, _spread), Random.Range(-_spread, _spread), Random.Range(-_spread, _spread)), out hit, 10f))
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
                spread++;
            } 
           
            Invoke("StopAttack", _attackSpeed);
            if(_currentIdx == 0)
            {
                _isReload = true;
                _reload = StartCoroutine(Reload());
            }
        }
        else if (_currentIdx > 0 && _isReload)
        {
            if (_reload != null)
            {
                Debug.Log("장전 종료");
                StopCoroutine(_reload);
                _isReload=false;
            }
        }
        else if (_currentIdx <= 0 && !_isReload)
        {
            _isReload = true;
            _reload = StartCoroutine(Reload());
        }
        
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
        _isReload = false;
        GenericSingleton<UIBase>.Instance.SetCurrentBullet(_currentIdx);
    }
    void StopAttack()
    {
     
        inAttack = false;
    }
}
