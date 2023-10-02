using UnityEngine;
public class Boss03NewAi : MonoBehaviour //***************쿨타임 지우고 트루펄스로 관리 하거나, 쿨타임 주고받기 +++++++++++
{
    GameObject player;
    Animator animator;

    float BossSpeed = 5f; // 보스 몬스터 이동 속도
    float attackRange = 30f;   // 공격 범위
    float attackCooldown = 3f; // 공격 쿨다운 시간 //공격패턴에 따라 시간 주고 받기
    float _hp;
    bool Attacking = false;       // 현재 공격 중인지
    bool HpMode = false; //1, 2 공격모드
    float attackTimer;      // 공격 쿨다운 타이머
    bool attackAtart = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform);

        animator = GetComponent<Animator>();
        animator.Play("In");// 테스트 후 인으로 바꿔주기
        Invoke("AttackStart", 7.8f);

        //animator.Play("Idle"); //서서대기

        //animator.Play("3_Skill1"); //양팔 밖으로 휘두른 뒤 양쪽팔 한번씩 휘두르기
        //animator.Play("3_Skill2"); //손에 달린 총쏘기
        //animator.Play("3_Skill3"); //부르부르?

        //animator.Play("3_Run"); //달리기

        //animator.Play("3_Atk1");  //한발 들어 찍기
        //animator.Play("3_Atk2"); //어깨에 달린 총쏘기
        //animator.Play("3_Atk3"); //양손 휘적휘적

        //animator.Play("3_Hit"); //맞음 

        //animator.Play("Dead"); //죽음 히트랑 연결
    }
    void Update()
    {
        _hp = GetComponent<Target>().Hp;
        if (_hp <= 0)
        {
            //GetComponent<UIBase>().AllUIOff();********오류보기
            Attacking = true;
            GetComponent<Boss03Attack01>().enabled = false;
            GetComponent<Boss03Skill01>().enabled = false;
            GetComponent<Boss03Attack02>().enabled = false;
            GetComponent<Boss03Skill02>().enabled = false;
            GetComponent<Boss03Attack03>().enabled = false;
            GetComponent<Boss03Dead>().enabled = true;
        }
        if (_hp > 0 && attackAtart)
        {
            DisCheck();
        }
        if (_hp > 0 && GetComponent<Target>().InDamage)
        {
            animator.Play("3_Hit");
        }
    }
    void AttackStart()
    {
        attackAtart = true;
    }
    void DisCheck()
    {
        transform.LookAt(player.transform);
       
        if (Attacking == false && !GetComponent<Target>().InDamage)
        {
            transform.LookAt(player.transform);
            Vector3 playerVector = new Vector3(player.transform.position.x, 0, player.transform.position.z);

            if (Vector3.Distance(player.transform.position, transform.position) > 30f)
            {
                Skill();
                Attacking = true;
            }
            if (Vector3.Distance(player.transform.position, transform.position) <= 30f && Vector3.Distance(player.transform.position, transform.position) > 7f)
            {
                animator.Play("3_Run");
                transform.position = Vector3.MoveTowards(transform.position, playerVector, BossSpeed * Time.deltaTime);
            }
            else if (Vector3.Distance(player.transform.position, transform.position) <= 7f)//플레이어와의 범위 내 공격
            {
                if (_hp > 70&& HpMode==false)
                {
                    Attackhp00();
                    //Attacking = true;
                }
                else if (_hp <= 70 && _hp > 50 && HpMode == false)
                {
                    Attackhp01();
                    //Attacking = true;
                }
                else if (_hp < 51 && HpMode == false)
                {
                    Attackhp02();
                    //Attacking = true;
                }
            }
        }
        else if (Attacking && GetComponent<Boss03Attack02>().enabled == false)
        {
            Attacking = false;
            GetComponent<Boss03Attack01>().enabled = false;
            GetComponent<Boss03Skill01>().enabled = false;
            GetComponent<Boss03Attack02>().enabled = false;
            GetComponent<Boss03Skill02>().enabled = false;
            GetComponent<Boss03Attack03>().enabled = false;
        }
    }
    void Skill() //거리가 멀때는 총알 발사
    {
        //GetComponent<Boss03Attack02>().enabled = true;
        GetComponent<Boss03Skill02>().enabled = true;
    }
    void Attackhp00()
    {
        Invoke("HpModeTrue", 3.2f);
        int rand = Random.Range(0, 2); // 매번 호출될 때마다 새로운 랜덤 값 생성
        if (rand == 0 && GetComponent<Boss03Skill01>().enabled == false)
        {
            GetComponent<Boss03Attack03>().enabled = true;
            GetComponent<Boss03Skill01>().enabled = false;
        }
        else if (rand == 1 && GetComponent<Boss03Attack03>().enabled == false)
        {
            GetComponent<Boss03Skill01>().enabled = true;
            GetComponent<Boss03Attack03>().enabled = false;
        }
    }
    void Attackhp01()
    {
        Invoke("HpModeTrue", 3.2f);
        int rand = Random.Range(0, 2);
        if (rand == 0 && GetComponent<Boss03Skill02>().enabled == false)
        {
            GetComponent<Boss03Skill01>().enabled = true;
            GetComponent<Boss03Skill02>().enabled = false;
        }
        else if (rand == 1 && GetComponent<Boss03Skill01>().enabled == false)
        {
            GetComponent<Boss03Skill02>().enabled = true;
            GetComponent<Boss03Skill01>().enabled = false;
        }
    }
    void Attackhp02()
    {
        Invoke("HpModeTrue", 2.0f);
        int rand = Random.Range(0, 2);
        if (rand == 0&& GetComponent<Boss03Attack03>().enabled == false)
        {
            GetComponent<Boss03Skill02>().enabled = true;
            GetComponent<Boss03Attack03>().enabled = false;
        }
        else if (rand == 1&& GetComponent<Boss03Skill02>().enabled == false)
        {
            GetComponent<Boss03Attack03>().enabled = true;
            GetComponent<Boss03Skill02>().enabled = false;
        }
    }
    void HpModeTrue()
    {
        HpMode = true;
        HpMode = false;
    }
}
