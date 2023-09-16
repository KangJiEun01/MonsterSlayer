//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Axe : WeaponBase
//{
//    protected override void OnStart()
//    {
        
//    }


//    protected override void OnUpdate()
//    { 
//        if (Input.GetMouseButton(0))
//        {
//            if (inAttack == false) RaycastShot();
//            // Fire();
//        }

//        if (Input.GetKeyDown(KeyCode.R) && _currentIdx < _maxBullet && !_isReload)
//        {
//            _isReload = true;
            
//        }
//    }
//    void RaycastShot()
//    {
//        if (_currentIdx > 0 && !_isReload)
//        {
//            RaycastHit hit;
//            _currentIdx--;
//            _effect.Play();
//            audioSource.PlayOneShot(_shotSound[Random.RandomRange(0,_shotSound.Length)], 1f);
//            _animator.Play("Shot");
//            inAttack = true;
//            if (aimTime > 0.3f)
//            {
//                recoil.RecoilFire(_recoil); //¹Ýµ¿
            
//            }
//            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
//            {
//                Debug.Log(hit.transform.name);
//                Target target = hit.transform.GetComponent<Target>();
//                target?.OnDamage(_attackDamage);
//                hit.rigidbody?.AddForce(-hit.normal * _impactForce);
//                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
//                {
//                    currentBullet = _bulletPool[_poolIndex++];
//                    currentBullet.SetActive(true);
//                    currentBullet.transform.rotation = Quaternion.LookRotation(hit.normal);
//                    currentBullet.transform.position = hit.point + hit.normal * 0.1f;
                
//                }

//            }
//            Invoke("StopAttack", _attackSpeed);
//        }
//        else if (_currentIdx <= 0 && !_isReload) _isReload = true;
        
       
        

//    }
//}
