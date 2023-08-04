
using UnityEngine;

public class Boss03NewAi : MonoBehaviour
{
    [SerializeField] Transform player;
    Animator animator;
    public int BossHp = 100;


    public float BossSpeed = 5f; // 보스 몬스터 이동 속도
    public float attackRange = 30f;   // 공격 범위
    public float attackCooldown = 1f; // 공격 쿨다운 시간 //공격패턴에 따라 시간 주고 받기
     // 플레이어의 Transform 컴포넌트
    private bool Attacking=false;       // 현재 공격 중인지
    private float attackTimer;      // 공격 쿨다운 타이머

    void Start()
    {
        //Transform player = player02.transform;
        animator = GetComponent<Animator>();
        //animator.Play("In"); 테스트 후 인으로 바꿔주기

        //animator.Play("Idle"); //서서대기

        //animator.Play("3_Skill1"); //양팔 밖으로 휘두른 뒤 양쪽팔 한번씩 휘두르기
        //animator.Play("3_Skill2"); //손에 달린 총쏘기
        //animator.Play("3_Skill3"); //부르부르?

        animator.Play("3_Run"); //달리기

        //animator.Play("3_Atk1");  //한발 들어 찍기
        //animator.Play("3_Atk2"); //어깨에 달린 총쏘기
        //animator.Play("3_Atk3"); //양손 휘적휘적

        //animator.Play("3_Hit"); //맞음 

        //animator.Play("Dead"); //죽음 히트랑 연결
    }

    void Update()
    {
        transform.LookAt(player.transform);
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            BossHp = 60;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BossHp = 40;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BossHp = 0;
        }

        if (BossHp <=0)
        {
            Attacking = true;
            GetComponent<Boss03Attack01>().enabled = false;
            GetComponent<Boss03Skill01>().enabled = false;
            GetComponent<Boss03Attack02>().enabled = false;
            GetComponent<Boss03Skill02>().enabled = false;
            GetComponent<Boss03Attack03>().enabled = false;
            GetComponent<Boss03Dead>().enabled = true;
        }
        if (Attacking==false)
        {
            transform.LookAt(player.transform);
            Vector3 playerVector = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            
            if(Vector3.Distance(player.transform.position, transform.position) > 50f)
            {
                Skill();
                Attacking = true;
                attackTimer = attackCooldown;
            }

            else if (Vector3.Distance(player.transform.position, transform.position) <= 50f && Vector3.Distance(player.transform.position, transform.position) > 14f) 
            {
                animator.Play("3_Run");
                transform.position = Vector3.MoveTowards(transform.position, playerVector, BossSpeed * Time.deltaTime);
            }
            else
            {
                // 플레이어와의 거리가 공격 범위 내에 있으면 공격
                if (attackTimer <= 0f)
                {
                    Attack();
                    Attacking = true;
                    attackTimer = attackCooldown;
                }
            }
        }
        else
        {
         // 공격 중일 때는 공격 쿨다운 시간 감소
            if (attackTimer > 0f)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
             Attacking = false;
             GetComponent<Boss03Attack01>().enabled = false;
             GetComponent<Boss03Skill01>().enabled = false;
             GetComponent<Boss03Attack02>().enabled = false;
             GetComponent<Boss03Skill02>().enabled = false;
             GetComponent<Boss03Attack03>().enabled = false;

            }
        }
    }   
    void Skill() //거리가 멀때는 총알 발사
    {
        GetComponent<Boss03Attack02>().enabled = true;
    }
    void Attack()
    {
        if(BossHp < 101 && BossHp > 69) 
        {
            int rand = Random.Range(0, 2);
            if(rand == 0)
            {
                GetComponent<Boss03Attack01>().enabled = true;
            }
            else if(rand == 1)
            {
                GetComponent<Boss03Skill01>().enabled = true;
                //GetComponent<Boss03Skill02>().enabled = true;
            }
            
        }
        else if (BossHp < 70 && BossHp > 50)
        {
            int rand = Random.Range(0, 2);
            if(rand == 0)
            {
                GetComponent<Boss03Skill01>().enabled = true;
            }
            else if(rand ==1)
            {
                GetComponent<Boss03Skill02>().enabled = true;
            }
        }
        else if(BossHp < 51)
        {
            int rand = Random.Range(0, 2);

            if (rand == 0)
            {
                GetComponent<Boss03Skill02>().enabled = true;
            }
            else if (rand == 1)
            {
                GetComponent<Boss03Attack03>().enabled = true;
            }
        }
    }
}
