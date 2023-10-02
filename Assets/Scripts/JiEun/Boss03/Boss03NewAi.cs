using UnityEngine;
public class Boss03NewAi : MonoBehaviour //***************��Ÿ�� ����� Ʈ���޽��� ���� �ϰų�, ��Ÿ�� �ְ�ޱ� +++++++++++
{
    GameObject player;
    Animator animator;

    float BossSpeed = 5f; // ���� ���� �̵� �ӵ�
    float attackRange = 30f;   // ���� ����
    float attackCooldown = 3f; // ���� ��ٿ� �ð� //�������Ͽ� ���� �ð� �ְ� �ޱ�
    float _hp;
    bool Attacking = false;       // ���� ���� ������
    bool HpMode = false; //1, 2 ���ݸ��
    float attackTimer;      // ���� ��ٿ� Ÿ�̸�
    bool attackAtart = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform);

        animator = GetComponent<Animator>();
        animator.Play("In");// �׽�Ʈ �� ������ �ٲ��ֱ�
        Invoke("AttackStart", 7.8f);

        //animator.Play("Idle"); //�������

        //animator.Play("3_Skill1"); //���� ������ �ֵθ� �� ������ �ѹ��� �ֵθ���
        //animator.Play("3_Skill2"); //�տ� �޸� �ѽ��
        //animator.Play("3_Skill3"); //�θ��θ�?

        //animator.Play("3_Run"); //�޸���

        //animator.Play("3_Atk1");  //�ѹ� ��� ���
        //animator.Play("3_Atk2"); //����� �޸� �ѽ��
        //animator.Play("3_Atk3"); //��� ��������

        //animator.Play("3_Hit"); //���� 

        //animator.Play("Dead"); //���� ��Ʈ�� ����
    }
    void Update()
    {
        _hp = GetComponent<Target>().Hp;
        if (_hp <= 0)
        {
            //GetComponent<UIBase>().AllUIOff();********��������
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
            else if (Vector3.Distance(player.transform.position, transform.position) <= 7f)//�÷��̾���� ���� �� ����
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
    void Skill() //�Ÿ��� �ֶ��� �Ѿ� �߻�
    {
        //GetComponent<Boss03Attack02>().enabled = true;
        GetComponent<Boss03Skill02>().enabled = true;
    }
    void Attackhp00()
    {
        Invoke("HpModeTrue", 3.2f);
        int rand = Random.Range(0, 2); // �Ź� ȣ��� ������ ���ο� ���� �� ����
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
