using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTest : MonoBehaviour
{
    [SerializeField] float _maxSpeed;
    [SerializeField] Transform _cam; // ī�޶��� ������ �޾Ƽ� �ڱ� �������� ����
    [SerializeField] float _speed;

    private void FixedUpdate()
    {
        //ī�޶� ���� �������� ĳ������ ������ ����
        Vector3 camRot = new Vector3(_cam.transform.forward.x, 0, _cam.transform.forward.z);
        // ī�޶� ���� ������ x��, z�ప 
        transform.rotation = Quaternion.LookRotation(camRot);

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 v3 = (transform.forward * y + transform.right * x) * _speed;
        Rigidbody rig = GetComponent<Rigidbody>();
        //rig.AddForce(v3, ForceMode.Force);
        //if(rig.velocity.x > _maxSpeed)
        //{
        //    rig.velocity = new Vector3(_maxSpeed, rig.velocity.y, rig.velocity.z);
        //}
        //if(rig.velocity.z > _maxSpeed)
        //{
        //    rig.velocity = new Vector3(rig.velocity.x, rig.velocity.y, _maxSpeed);
        //}
        //v3.y = GetComponent<Rigidbody>().velocity.y;
        //GetComponent<Rigidbody>().velocity = v3;
    }

    bool _isSloped = false;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, -transform.up, out hit, 2f) && _isSloped == false)
        {
            if (Mathf.Abs(Vector3.Angle(Vector3.up, hit.normal)) > 40) // �浹���� ���Ⱑ 40�� �̻��϶�
            {
                //Debug.Log("�浹 ���� ���� : "+ Vector3.Angle(Vector3.up, hit.normal));
                Vector3 slopeForce = Vector3.ProjectOnPlane(Physics.gravity, hit.normal).normalized * 0.5f;
                GetComponent<Rigidbody>().AddForce(slopeForce, ForceMode.Impulse);
                _isSloped = true;
                Invoke("ResetSloped", 0.1f);
            }
        }
    }
    void ResetSloped()
    {
        _isSloped = false;
    }
}
