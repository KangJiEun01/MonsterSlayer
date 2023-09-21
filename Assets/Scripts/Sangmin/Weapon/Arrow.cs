using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Arrow : MonoBehaviour
{
    
    private bool isStuck = false; // 화살이 박혀 있는지 여부

    void OnCollisionEnter(Collision collision)
    {
        if (!isStuck && collision.gameObject.CompareTag("Boss"))
        {
            StickToTarget(collision.transform);
        }
    }

    void StickToTarget(Transform target)
    {
     
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

       
        transform.parent = target;
        isStuck = true;

        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward, out hit))
        {
            transform.position = hit.point;
        }

    }
}
