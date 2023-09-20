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

        // 화살을 타겟에 부착합니다.
        transform.parent = target;
        isStuck = true;

        // 화살을 타겟의 Surface에 박히도록 위치 조정
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward, out hit))
        {
            transform.position = hit.point;
        }

        // 여기서 필요한 추가 작업을 수행합니다.
        Debug.Log("화살이 타겟에 박혔습니다!");
    }
}
