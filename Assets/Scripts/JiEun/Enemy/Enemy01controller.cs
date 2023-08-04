using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01controller : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform endPoint;

    float attackRange = 7f;//�νĹ���
    float patrolSpeed = 3f; //�����ӵ�
    float chaseSpeed = 7f; //�ν� �� �߰� �ӵ�

    bool _patrol = true;
    bool movingRight = true;
    bool _collision = false;

    Vector3 patrolStartPoint;
    Vector3 patrolEndPoint;


    // mins edit
    public float avoidanceDistance = 1.0f;
    public float rayDistance = 1.0f;
    //

    void Start()
    {
        patrolEndPoint = endPoint.position;
        patrolStartPoint= spawnPoint.position;

    }

    void Update()
    {
        if (player != null)
        {
            float distanceTarget = Vector3.Distance(transform.position, player.transform.position);

            if (distanceTarget <= attackRange)
            {
                // ����üũ 
                Attack();
            }
            else if (_patrol == true)
            {
                // ���� ��
                Patrol();
            }
            else
            {
                // �߰� ��
                Chase();
            }
        }
    }

    void Attack()
    {
        Debug.Log("���� ����");
    }
    void Patrol()
    {
        float distanceToStart = Vector3.Distance(transform.position, patrolStartPoint);
        float distanceToEnd = Vector3.Distance(transform.position, patrolEndPoint);

        if (movingRight == true)
        {
            // ���������� �̵�
            if (!_collision)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolEndPoint, patrolSpeed * Time.deltaTime);
            }
            transform.LookAt(endPoint.transform);
            Debug.Log("EndPoint �̵� ��");
            // patrolEndPoint�� �����ϸ� �̵� ������ �������� ����
            if (distanceToEnd < 2f)
            {
                movingRight = false;
            }
        }
        else
        {
            // �������� �̵�
            if (!_collision)
            { 
                transform.position = Vector3.MoveTowards(transform.position, patrolStartPoint, patrolSpeed * Time.deltaTime);
            }   
            transform.LookAt(spawnPoint.transform);
            // patrolStartPoint�� �����ϸ� �̵� ������ ���������� ����
            if (distanceToStart < 2f)
            {
                movingRight = true;
            }
        }
    }
    void Chase()
    {
        _patrol = false;
    }

    // mins edit
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
        {
            if (hit.distance < avoidanceDistance)
            {
                _collision = true;
                Vector3 avoid = transform.position + transform.right * Random.Range(-1.0f, 1.0f);
                Vector3 direction = avoid - transform.position;
                GetComponent<Rigidbody>().AddForce(direction * patrolSpeed);
            }
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(transform.right * patrolSpeed);
            _collision = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log(" �浹");
            Vector3 avoid = transform.position + transform.right * Random.Range(-1.0f, 1.0f);
            Vector3 direction = avoid - transform.position;
            GetComponent<Rigidbody>().AddForce(direction * patrolSpeed);
        }
    }
    //
}
