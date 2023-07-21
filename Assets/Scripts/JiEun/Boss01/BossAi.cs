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

    int second = 0; //��ǥ��
    float Time_current; //������
    float Time_start; //+���� ���� ��
    float Time_Sumcooltime = 1;//������ ����
    public float Time_Attack = 0f;
    int Patternsum = 0;

    void Start()
    {
        // BossTrans.position = new Vector3(521, 0, 570); 
        bossCenterPoint = transform.position;
        animator.Play("In"); //�⺻���
        Invoke("CameraSk", 4.7f);
        Time_Attack += 8f;
        //animator.Play("1_Atk1"); // �μ� ��� ��ġ��
        //animator.Play("1_Atk2"); //�պ�¦
        //animator.Play("Stage"); //�μ��� Dead�� ����
        //animator.Play("Dead");
        //animator.Play("1_Run"); //���
        //animator.Play("1_Skill1"); //�Ҹ� ġ�� ���ۺ��� ���� ex)�� ���鼭 �浹�ϸ� �÷��̾� ���ư� 

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
        realTime += Time.deltaTime * setTime; //�ð����
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
    void DistanceCheck() // �Ÿ��� üũ���� ���� �ð����� üũ�սô�.
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
        //animator.Play("1_Atk2");// �浹�ϸ� HP ���� �߰�
        //Invoke("CameraSk", 0.5f);
    }
    void Skill1() //���ӵ��� ���� ���� //���� ��ġ ����
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
    void Reset_CoolTime() //������ �ʱ�ȭ
    {
        Time_current = Time_Sumcooltime;
        Time_start = Time.time;
    }
    public float getTime_Attack()
    {
        return Time_Attack;
    }
}
