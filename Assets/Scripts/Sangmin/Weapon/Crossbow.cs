
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
            Vector3 dirVector = Camera.main.transform.forward;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 50f))
            {
                 dirVector = hit.point - _firePosition.position;
            }
            GameObject temp = Instantiate(_bullet);
            temp.transform.position = _firePosition.position;
            temp.GetComponent<BoxCollider>().enabled = true;
            temp.transform.rotation = Quaternion.LookRotation(dirVector);
            temp.GetComponent<Rigidbody>().AddForce(dirVector.normalized * _bulletSpeed, ForceMode.Impulse);
            Destroy(temp, 10f);

            Invoke("StopAttack", _attackSpeed);
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

