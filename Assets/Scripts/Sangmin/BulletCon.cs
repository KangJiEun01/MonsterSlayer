using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCon : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("BulletDestroy", 2f);
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        BulletDestroy();
    }
    void BulletDestroy()
    {
        gameObject.SetActive(false);
        rb.velocity = Vector3.zero;
    }
}
