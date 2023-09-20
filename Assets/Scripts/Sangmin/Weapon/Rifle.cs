using System.Collections;
using UnityEngine;

public class Rifle : HitScan
{

    [SerializeField] protected RectTransform _upCrosshair;
    [SerializeField] protected RectTransform _rightCrosshair;
    [SerializeField] protected RectTransform _downCrosshair;
    [SerializeField] protected RectTransform _leftCrosshair;
    




    
    void AimOpen()
    {
        _upCrosshair.anchoredPosition3D += Vector3.up * 2f;
        _rightCrosshair.anchoredPosition3D += Vector3.left * 2f;
        _downCrosshair.anchoredPosition3D += Vector3.down * 2f;
        _leftCrosshair.anchoredPosition3D += Vector3.right * 2f;
    }
    void AimReturn()
    {
        _upCrosshair.anchoredPosition3D = new Vector3(0, 35, 0);
        _rightCrosshair.anchoredPosition3D = new Vector3(-40, 0, 0f);
        _downCrosshair.anchoredPosition3D = new Vector3(0, -35, 0);
        _leftCrosshair.anchoredPosition3D = new Vector3(40, 0, 0 );
    }

   
   
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
            AimOpen();           // aim¹ú¾îÁü
            
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
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
                    _currentBullet.transform.position = hit.point + hit.normal*0.1f;
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
    //void Shot()
    //{
    //    float angle = Random.Range(0.0f, _spreadAngle * 0.5f);
    //    Vector2 angleDir = Random.insideUnitCircle * Mathf.Tan(angle * Mathf.Deg2Rad);

    //    Vector3 dir = Endpoint.transform.forward + (Vector3)angleDir;
    //    dir.Normalize();
    //    _bulletPool[_poolIndex].gameObject.SetActive(true);
    //    _bulletPool[_poolIndex].Launch(dir, 200);
    //}
    //void Launch(Vector3 direction, float force)
    //{
    //    m_Owner = launcher;

    //    transform.position = launcher.GetCorrectedMuzzlePlace();
    //    transform.forward = launcher.EndPoint.forward;

    //    gameObject.SetActive(true);
    //    m_TimeSinceLaunch = 0.0f;
    //    m_Rigidbody.AddForce(direction * force);
    //}
}