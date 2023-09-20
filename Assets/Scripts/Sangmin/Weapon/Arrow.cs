using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Arrow : MonoBehaviour
{
    
    private bool isStuck = false; // ȭ���� ���� �ִ��� ����

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

        // ȭ���� Ÿ�ٿ� �����մϴ�.
        transform.parent = target;
        isStuck = true;

        // ȭ���� Ÿ���� Surface�� �������� ��ġ ����
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward, out hit))
        {
            transform.position = hit.point;
        }

        // ���⼭ �ʿ��� �߰� �۾��� �����մϴ�.
        Debug.Log("ȭ���� Ÿ�ٿ� �������ϴ�!");
    }
}
