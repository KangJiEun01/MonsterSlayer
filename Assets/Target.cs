using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float Hp;
    public void OnDamage(float damage)
    {
        Hp -= damage;
        if (Hp  <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        gameObject.SetActive(false);
    }
}
