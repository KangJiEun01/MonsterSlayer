using UnityEngine;
public class Boss03NewAi : MonoBehaviour
{
    Transform player;
    Animator animator;

    float _hp = 100;
    float chaseDistance = 70f; // 플레이어 최대거리
    float attackDistance = 7f; // 공격거리
    float attackCooldown = 3.1f; // 공격쿨다운
    float bossSpeed = 18f;

    bool isAttacking = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
        transform.LookAt(player.position);
        animator = GetComponent<Animator>();
        animator.Play("In");
        Invoke("AttackAct", 7.6f);
        //InvokeRepeating("CheckDistance", 7.8f, 0.5f); //몇초뒤실행//몇초마다 반복
    }

    void Update()
    {
        _hp = GetComponent<Target>().Hp;
        if (_hp <= 0)
        {
            GetComponent<Boss03Attack01>().enabled = false;
            GetComponent<Boss03Skill01>().enabled = false;
            GetComponent<Boss03Attack03>().enabled = false;
            GetComponent<Boss03Hit>().enabled = false;
            GetComponent<Boss03Dead>().enabled = true;
        }
        if (_hp > 0 && GetComponent<Target>().InDamage)
        {
            GetComponent<Boss03Hit>().enabled = true;
        }

        else if (_hp > 0&&!isAttacking)
        {
            transform.LookAt(player.position);
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > attackDistance)
            {
                animator.Play("3_Run");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, 0, player.position.z), bossSpeed * Time.deltaTime);
            }
            else
            {
                //attackDistance 가까우면 공격
                StartAttack();
            }
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        Invoke("StopAttack", attackCooldown);
        int rand = Random.Range(0, 3); //공격랜덤
        if (rand == 0 && GetComponent<Boss03Skill01>().enabled == false&& GetComponent<Boss03Attack01>().enabled == false)
        {
            GetComponent<Boss03Attack03>().enabled = true;
            GetComponent<Boss03Skill01>().enabled = false;
            GetComponent<Boss03Attack01>().enabled = false;
        }
        else if (rand == 1 && GetComponent<Boss03Attack03>().enabled == false&& GetComponent<Boss03Attack01>().enabled == false)
        {
            GetComponent<Boss03Skill01>().enabled = true;
            GetComponent<Boss03Attack03>().enabled = false;
            GetComponent<Boss03Attack01>().enabled = false;
        }
        else if (rand == 2 && GetComponent<Boss03Attack01>().enabled == false&& GetComponent<Boss03Attack03>().enabled == false)
        {
            GetComponent<Boss03Attack01>().enabled = true;
            GetComponent<Boss03Skill01>().enabled = false;
            GetComponent<Boss03Attack03>().enabled = false;
        }
    }

    void StopAttack()
    {
        isAttacking = false;
    }
    void CheckDistance()
    {
        // 거리를 체크
    }
    void AttackAct()
    {
        isAttacking = false;
    }
}
