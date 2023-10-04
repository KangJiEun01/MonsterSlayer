using System;
using Unity.Mathematics;
using UnityEngine;

public class Enemy01controller : MonoBehaviour
{
    GameObject player;
    //[SerializeField] GameObject Enemy;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos; //-z
    [SerializeField] GameObject detectionUi;
    [SerializeField] GameObject[] DropItem;

    Transform botPos;
    Animator anim;

    float attackRange = 7f;//인식범위
    float patrolSpeed = 2f; //순찰속도
    float chaseSpeed = 5f; //인식 후 추격 속도
    float bulletSpeed = 12f;
    float AttackAniSpeed = 2f; //공격 애니메이션 재생 속도
    float hp;

    bool _patrol = true;
    bool movingRight = true;
    bool _collision = false;
    bool _attack = false;
    bool _setActive = false;
    bool _Spawn = true;
    //bool bulletFire = false;

    float Time_current; //남은초
    float Time_start; //+까지 남은 초
    float Time_Sumcooltime = 0.15f;//남은초 설정
    float Time_bulletfire = 2f;
    float Time_cool = 2f;
    float Time_LastShot = 0f;

    int itemNum = 0;

    public bool Attacker { get { return _attack; } }
    public bool InSpawn { get { return _Spawn; } }
    public bool _damage = false;

    Vector3 patrolStartPoint;
    Vector3 patrolEndPoint;
    Vector3 VectorbulletPos; //총알 시작위치 수정할것

    // mins edit
    //float avoidanceDistance = 1.0f;
    //float rayDistance = 1.0f;

    private void Awake()
    {
        detectionUi.SetActive(false);
    }
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        patrolEndPoint = endPoint.position;
        patrolStartPoint = spawnPoint.position;
        botPos = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        VectorbulletPos = bulletPos.position;
        anim.Play("WalkFront_Shoot_AR");
        Invoke("find", 1f);

    }
    private void OnEnable()
    {
        Debug.Log("리스폰");
        patrolEndPoint = endPoint.position;
        patrolStartPoint = spawnPoint.position;
        botPos = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        VectorbulletPos = bulletPos.position;
        anim.Play("WalkFront_Shoot_AR");
        find();
        _setActive = false;
        itemNum = 0;
    }
    void Update()
    {
        if (player != null)
        {
            hp = GetComponent<Target>().Hp;
            if (hp == 0)
            {
                _setActive = true;
                anim.Play("Die");
                Invoke("Die", 1.5f);
            }
            if (!_setActive)
            {
                float distanceTarget = Vector3.Distance(transform.position, player.transform.position);

                if (distanceTarget <= attackRange)
                {
                    // 범위체크 
                    Attack();
                }
                else if (_patrol == true)
                {
                    // 순찰 중
                    Patrol();
                }
                else
                {
                    // 추격 중
                    Chase();
                }
            }
            if (_attack)
            {
                BulletFire();
                detectionUi.SetActive(true);
            }
        }
    }

    void Attack()
    {
        _attack = true;
        transform.LookAt(player.transform);

        //transform.LookAt(player.transform);
        //GetComponent<Animator>().Play("Reload");
        //Invoke("ShootAin",1.2f);
        ShootAin();
    }
    void ShootAin()
    {
        //GetComponent<Animator>().Play("Shoot_SingleShot_AR");
        anim.Play("Shoot_BurstShot_AR");
        //anim.Play("Shoot_Autoshot_AR");
    }
    void BulletFire()
    {
        if (!_setActive)
        {
            Time_current = Time.time - Time_start;
            if (Time_current > Time_Sumcooltime)
            {
                //Vector3 attackst = new Vector3(VectorbulletPos.x + (-2.0f), VectorbulletPos.y+(+5.0f), VectorbulletPos.z + (-8.5f));
                Vector3 attackst = new Vector3(VectorbulletPos.x, VectorbulletPos.y, VectorbulletPos.z); ;
                GameObject temp = Instantiate(bullet);
                //Vector3 worldPosition = bulletPos.TransformPoint(Vector3.zero);
                Vector3 worldPosition = bulletPos.TransformPoint(attackst);
                temp.transform.position = worldPosition;
                //Vector3 dir =player.transform.position; 
                Vector3 dir = transform.forward; //앞방향
                temp.GetComponent<FireBullet>().Init(dir, bulletSpeed);
                Time_current = Time_Sumcooltime;
                Time_start = Time.time;
            }
        }
    }
    void find()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void BulletFire2()
    {
        Time_LastShot += Time.deltaTime;
        if (Time_LastShot < Time_cool) return;
        while (Time_LastShot < Time_bulletfire + Time_cool)
        {
            Time_current = Time.time - Time_start;
            if (Time_current > Time_Sumcooltime)
            {

                Vector3 attackst = new Vector3(VectorbulletPos.x, VectorbulletPos.y, VectorbulletPos.z); ;
                GameObject temp = Instantiate(bullet);

                Vector3 worldPosition = bulletPos.TransformPoint(attackst);
                temp.transform.position = worldPosition;

                Vector3 dir = transform.forward; //앞방향
                temp.GetComponent<FireBullet>().Init(dir, bulletSpeed);
                Time_current = Time_Sumcooltime;
                Time_start = Time.time;
            }

            Time_LastShot = Time_bulletfire + Time_cool;
        }
    }
    void Patrol()
    {
        _attack = false;
        detectionUi.SetActive(false);
        float distanceToStart = Vector3.Distance(transform.position, patrolStartPoint);
        float distanceToEnd = Vector3.Distance(transform.position, patrolEndPoint);
        anim.Play("WalkFront_Shoot_AR");
        if (movingRight == true)
        {
            // 오른쪽으로 이동
            if (!_collision)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolEndPoint, patrolSpeed * Time.deltaTime);
            }
            transform.LookAt(endPoint.transform);
            if (distanceToEnd < 2f)
            {
                movingRight = false;
            }
        }
        else
        {
            // 왼쪽으로 이동
            if (!_collision)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolStartPoint, patrolSpeed * Time.deltaTime);
            }
            transform.LookAt(spawnPoint.transform);
            // patrolStartPoint에 도달하면 이동 방향을 오른쪽으로 변경
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
    void Die()
    {
        int num = UnityEngine.Random.Range(0, 4);
        if(itemNum==0)
        {
            Instantiate(DropItem[num], new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z), Quaternion.identity);
        }
        itemNum++;
        _Spawn = false;
        //Enemy.GetComponent<Enemy01controller>().enabled = true;
        gameObject.SetActive(false);
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
    //        Debug.Log(" 충돌");
    //        Vector3 avoid = transform.position + transform.right * Random.Range(-1.0f, 1.0f);
    //        Vector3 direction = avoid - transform.position;
    //        GetComponent<Rigidbody>().AddForce(direction * patrolSpeed);
    //    }
    //}
    //
}
