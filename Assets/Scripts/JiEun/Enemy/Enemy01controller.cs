using UnityEngine;

public class Enemy01controller : MonoBehaviour
{
    GameObject player;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletPos; //-z
    [SerializeField] GameObject detectionUi;
    [SerializeField] GameObject[] DropItem;
    [SerializeField] AudioClip bulletFire;
    AudioSource bulletSource;
    Rigidbody rb;

    Transform botPos;
    Animator anim;

    float attackRange = 7f;//인식범위
    float patrolSpeed = 2f; //순찰속도
    float chaseSpeed = 5f; //인식 후 추격 속도
    float bulletSpeed = 12f;
    float AttackAniSpeed = 2f; //공격 애니메이션 재생 속도
    float hp = 100;

    bool _patrol = true;
    bool movingRight = true;
    bool _collision = false;
    bool _attack = false;
    bool _setActive = false;
    bool _Spawn = true;

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
        bulletSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        GenericSingleton<UIBase>.Instance.EffectVolume += Sound;
        Sound(PlayerPrefs.GetFloat("EffectVolume"));
        Invoke("find", 1f);
    }
    private void OnEnable()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<EnemyActive>().enabled = false;
        hp = 100;
        gameObject.GetComponent<Target>().Hp = hp;
        Debug.Log("리스폰");
        patrolEndPoint = endPoint.position;
        patrolStartPoint = spawnPoint.position;
        botPos = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        VectorbulletPos = bulletPos.position;
        anim.Play("WalkFront_Shoot_AR");
        Invoke("find", 1f);
        _setActive = false;
        itemNum = 0;
    }
    void Update()
    {
        if (player != null)
        {
            hp = GetComponent<Target>().Hp;
            if (hp <= 0&& _setActive==false)
            {
                _setActive = true;
                anim.Play("Die");
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<BoxCollider>().enabled = false;
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
    public void Sound(float volume)
    {
        bulletSource.volume = volume;
    }
    void Attack()
    {
        _attack = true;
        transform.LookAt(player.transform);
        ShootAin();
    }
    void ShootAin()
    {
        anim.Play("Shoot_BurstShot_AR");
    }
    void BulletFire()
    {
        if (!_setActive)
        {
            Time_current = Time.time - Time_start;
            if (Time_current > Time_Sumcooltime)
            {
                bulletSource.PlayOneShot(bulletFire);
                Vector3 attackst = new Vector3(VectorbulletPos.x, VectorbulletPos.y, VectorbulletPos.z); ;
                GameObject temp = Instantiate(bullet);
                Vector3 worldPosition = bulletPos.TransformPoint(attackst);
                temp.transform.position = worldPosition;
                //Vector3 dir = transform.forward; //앞방향
                Vector3 dir = new Vector3(transform.forward.x+0.07f, transform.forward.y-0.3f, transform.forward.z);
                temp.GetComponent<FireBullet>().Init(dir, bulletSpeed);
                Time_current = Time_Sumcooltime;
                Time_start = Time.time;
            }
        }
    }
    void find()
    {
        player = GameObject.FindGameObjectWithTag("player");
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
            // 오른쪽 이동
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
            // 왼쪽 이동
            if (!_collision)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolStartPoint, patrolSpeed * Time.deltaTime);
            }
            transform.LookAt(spawnPoint.transform);
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
            Instantiate(DropItem[num], new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
        }
        itemNum++;
        _Spawn = false;
        GetComponent<EnemyActive>().enabled = true;
        GetComponent<Enemy01controller>().enabled = false;
        gameObject.SetActive(false);
    }
    void OnDestroy()
    {
        GenericSingleton<UIBase>.Instance.EffectVolume -= Sound;
    }
}
