using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Melee
{
    Vector3 _target;
    public override void Fire()
    {

        RaycastHit hit;
        _audioSource.PlayOneShot(_shotSound[Random.Range(0, _shotSound.Length)], 1f);
        _animator.Play("Attack");
        inAttack = true;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, _attackRange))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            target?.OnDamage(_attackDamage);
            hit.rigidbody?.AddForce(-hit.normal * _impactForce);
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                _target = hit.point;
                Invoke("HitTarget",0.35f);
            }

        }
        Invoke("StopAttack", _attackSpeed);
    }
    void HitTarget()
    {
        GameObject temp = Instantiate(_hitEffect, _target, Quaternion.identity);
        Destroy(temp,10);
    }
    void StopAttack()
    {
        inAttack = false;
    }
}




    