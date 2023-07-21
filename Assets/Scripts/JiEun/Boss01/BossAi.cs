using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BossAi : MonoBehaviour
{
    [SerializeField] Animator animator;
    //[SerializeField] GameObject player;
    [SerializeField] Transform player02;
    [SerializeField] Transform BossTrans;
    float BossSpeed = 20;
    Vector3 bossCenterPoint;
    [SerializeField] Camera cam;
    float HP = 100;
    //[SerializeField] Transform Test;
    //public Transform target;
    float radius = 3f;
    float shake=1f;

    float setTime = 0.1f;
    float realTime = 0f;

    int second = 0; //초표시
    float Time_current; //남은초
    float Time_start; //+까지 남은 초
    float Time_Sumcooltime = 1;//남은초 설정
    public float Time_Attack = 0f;
    int Patternsum = 0;

    void Start()
    {
        // BossTrans.position = new Vector3(521, 0, 570); 
        bossCenterPoint = transform.position;
        animator.Play("In"); //기본대기
        Invoke("CameraSk", 4.7f);
        Time_Attack += 8f;
        //animator.Play("1_Atk1"); // 두손 들어 땅치기
        //animator.Play("1_Atk2"); //손벽짝
        //animator.Play("Stage"); //부서짐 Dead랑 같음
        //animator.Play("Dead");
        //animator.Play("1_Run"); //대기
        //animator.Play("1_Skill1"); //소리 치고 빙글빙글 돌기 ex)맵 돌면서 충돌하면 플레이어 날아감 

        //animator.Play("In");
        //Invoke("Update", 3f);
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HP = 40;
        }
        TimeCheck();
        Invoke("AttackPattern", 8f);

        if (HP < 0)
        {
            Dead();
        }
    }
    void TimeCheck()
    {
        realTime += Time.deltaTime * setTime; //시간계산
        Time_current = Time.time - Time_start;
        if (Time_current >Time_Sumcooltime)
        {
            second += 1;
            Reset_CoolTime();
        }
    }
    void AttackPattern()
    {
        if(HP>50&&Patternsum ==0 && second > Time_Attack)
        {
            Playerfollow();
        }
        else if (HP > 50 && Patternsum ==1&& second > Time_Attack)
        {
            Playerfollow02();
        }
        else if(HP<50)
        {
            GetComponent<BossAttack01>().enabled = false;
            GetComponent<BossAttack02>().enabled = false;
            Skill1();
        }

        //int atkran;
        //if (second>8&&second<10)
        //{
        //    Playerfollow();
        //}
        //else if(second>10&&second<12) 
        //{
        //    atkran = Random.Range(0, 3);
        //    Atk02();
        //}
    }
    void DistanceCheck() // 거리로 체크하지 말고 시간으로 체크합시다.
    {
        if (Vector3.Distance(player02.position, BossTrans.position) >8f)
        {
            Playerfollow();
        }
        else if (Vector3.Distance(player02.position, BossTrans.position) > 12f)
        {
            //Skill1();
        }
        else if (Vector3.Distance(player02.position, BossTrans.position) < 8f)
        {
            Atk02();
        }
    }

    void Playerfollow()
    {
        transform.LookAt(player02);
        transform.position = Vector3.MoveTowards(transform.position, player02.position, BossSpeed * Time.deltaTime);
        Invoke("TimePlayerfollow", 2f);
    }
    void TimePlayerfollow()
    {
        Atk02();
        Patternsum = 1;
       // Time_Attack = GetComponent<BossAttack02>().setTime_Attack();

    }
    void Playerfollow02()
    {
        transform.LookAt(player02);
        transform.position = Vector3.MoveTowards(transform.position, player02.position, BossSpeed * Time.deltaTime);
        Invoke("TimePlayerfollow02", 2f);
    }
    void TimePlayerfollow02()
    {
        Atk01();
        Patternsum = 0;
    }

    void Atk01()
    {
        GetComponent<BossAttack01>().enabled = true;
        transform.LookAt(player02);
        Invoke("CameraSk", 1.5f);
        
    }
    void Atk02()
    {
        GetComponent<BossAttack02>().enabled = true;
        transform.LookAt(player02);
        Invoke("CameraSk", 1.5f);
        Debug.Log(Patternsum );
        //animator.Play("1_Atk2");// 충돌하면 HP 감소 추가
        //Invoke("CameraSk", 0.5f);
    }
    void Skill1() //가속도로 돌게 수정 //시작 위치 수정
    {
        GetComponent<Skill1>().enabled = true;
        float angle =+ Time.time * 5;
        Vector3 targetPos = bossCenterPoint + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;
        transform.position = targetPos;
    }
    void Dead()
    {
        GetComponent<BossDead>().enabled = true;
    }
    void CameraSk()
    {
        cam.GetComponent<CameraShake>().enabled = true;
    }
    public float SetShake()
    {
        return shake;
    }
    void Reset_CoolTime() //남은초 초기화
    {
        Time_current = Time_Sumcooltime;
        Time_start = Time.time;
    }
    public float getTime_Attack()
    {
        return Time_Attack;
    }
}
