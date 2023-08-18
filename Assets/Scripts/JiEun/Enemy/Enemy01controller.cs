using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01controller : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos; //-z

    Transform botPos;

    float attackRange = 7f;//�νĹ���
    float patrolSpeed = 3f; //�����ӵ�
    float chaseSpeed = 7f; //�ν� �� �߰� �ӵ�
    float bulletSpeed = 20f;
    float AttackAniSpeed = 2f; //���� �ִϸ��̼� ��� �ӵ�

    bool _patrol = true;
    bool movingRight = true;
    bool _collision = false;
    bool _attack = false;

    float Time_current; //������
    float Time_start; //+���� ���� ��
    float Time_Sumcooltime = 0.2f;//������ ����

    public bool Attacker { get { return _attack; } }

    Vector3 patrolStartPoint;
    Vector3 patrolEndPoint;
    Vector3 VectorbulletPos; //�Ѿ� ������ġ �����Ұ�


    // mins edit
    public float avoidanceDistance = 1.0f;
    public float rayDistance = 1.0f;
    //

    void Start()
    {
        patrolEndPoint = endPoint.position;
        patrolStartPoint= spawnPoint.position;
        botPos=GetComponent<Transform>();
        VectorbulletPos = bulletPos.position;
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
        if(_attack)
        {
            BulletFire();
        }
    }

    void Attack()
    {
        _attack = true;
        transform.LookAt(player.transform);
        
        //transform.LookAt(player.transform);
        Debug.Log("���� ����");
        //GetComponent<Animator>().Play("Reload");
        //Invoke("ShootAin",1.2f);
        ShootAin();
    }
    void ShootAin()
    {
        GetComponent<Animator>().Play("Shoot_SingleShot_AR");
    }
    void BulletFire()
    {
        Time_current = Time.time - Time_start;
        if (Time_current > Time_Sumcooltime)
        {
            Vector3 attackst = new Vector3(VectorbulletPos.x + (-2.0f), VectorbulletPos.y+(+5.0f), VectorbulletPos.z + (-8.5f));
            GameObject temp = Instantiate(bullet);
            //Vector3 worldPosition = bulletPos.TransformPoint(Vector3.zero);
            Vector3 worldPosition = bulletPos.TransformPoint(attackst);
            temp.transform.position = worldPosition;
           // Vector3 dir =player.transform.position; //����ź
            Vector3 dir = transform.forward; //�չ���
            temp.GetComponent<FireBullet>().Init(dir, bulletSpeed);
            Time_current = Time_Sumcooltime;
            Time_start = Time.time;
        }
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
    //void FixedUpdate()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
    //    {
    //        if (hit.distance < avoidanceDistance)
    //        {
    //            _collision = true;
    //            Vector3 avoid = transform.position + transform.right * Random.Range(-1.0f, 1.0f);
    //            Vector3 direction = avoid - transform.position;
    //            GetComponent<Rigidbody>().AddForce(direction * patrolSpeed);
    //        }
    //    }
    //    else
    //    {
    //        GetComponent<Rigidbody>().AddForce(transform.right * patrolSpeed);
    //        _collision = false;
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Obstacle")
    //    {
    //        Debug.Log(" �浹");
    //        Vector3 avoid = transform.position + transform.right * Random.Range(-1.0f, 1.0f);
    //        Vector3 direction = avoid - transform.position;
    //        GetComponent<Rigidbody>().AddForce(direction * patrolSpeed);
    //    }
    //}
    //
}
