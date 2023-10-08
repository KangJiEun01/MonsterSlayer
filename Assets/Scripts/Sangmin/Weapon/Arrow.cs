
using UnityEngine;
using UnityEngine.VFX;

public class Arrow : MonoBehaviour
{
    
    private bool _isStuck = false; // ȭ���� ���� �ִ��� ����
    VisualEffect _effect;
    private void Start()
    {
        _effect = GetComponentInChildren<VisualEffect>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!_isStuck)
        {
            _effect.SendEvent("Shot");
            transform.position = collision.GetContact(0).point;
            StickToTarget(collision.transform);
        }
    }

    void StickToTarget(Transform target)
    {

        
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        GetComponent<BoxCollider>().enabled = false;

       
        transform.parent = target;
        _isStuck = true;

    }
}
