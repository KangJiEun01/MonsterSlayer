using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 10f;
    public float gravity = 9.81f;
    private Rigidbody rb;
    private bool isJumping = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.useGravity = false; // �߷� ������� ����
        Physics.gravity = new Vector3(0, -gravity * 2);
    }

    private void Update()
    {
        //transform.LookAt(new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z));
        transform.rotation = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z));
        // ������ �Է� ó��
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward * vertical;
        Vector3 right = transform.right * horizontal;

        Vector3 movement = (forward.normalized + right.normalized).normalized; //new Vector3(horizontal, 0f, vertical).normalized;
        rb.velocity = new Vector3(movement.x * movementSpeed, rb.velocity.y, movement.z * movementSpeed);

        // ���� �Է� ó��
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            Jump();
        }

        // �߷� ����
        //rb.AddForce(Vector3.down * gravity);
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = this.transform.position;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
