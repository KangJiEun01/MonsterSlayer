
using UnityEngine;

public class Boss03NewAi : MonoBehaviour
{
    [SerializeField] Transform player;
    Animator animator;
    public int BossHp = 100;


    public float BossSpeed = 5f; // ���� ���� �̵� �ӵ�
    public float attackRange = 30f;   // ���� ����
    public float attackCooldown = 1f; // ���� ��ٿ� �ð� //�������Ͽ� ���� �ð� �ְ� �ޱ�
     // �÷��̾��� Transform ������Ʈ
    private bool Attacking=false;       // ���� ���� ������
    private float attackTimer;      // ���� ��ٿ� Ÿ�̸�

    void Start()
    {
        //Transform player = player02.transform;
        animator = GetComponent<Animator>();
        //animator.Play("In"); �׽�Ʈ �� ������ �ٲ��ֱ�

        //animator.Play("Idle"); //�������

        //animator.Play("3_Skill1"); //���� ������ �ֵθ� �� ������ �ѹ��� �ֵθ���
        //animator.Play("3_Skill2"); //�տ� �޸� �ѽ��
        //animator.Play("3_Skill3"); //�θ��θ�?

        animator.Play("3_Run"); //�޸���

        //animator.Play("3_Atk1");  //�ѹ� ��� ���
        //animator.Play("3_Atk2"); //����� �޸� �ѽ��
        //animator.Play("3_Atk3"); //��� ��������

        //animator.Play("3_Hit"); //���� 

        //animator.Play("Dead"); //���� ��Ʈ�� ����
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
                // �÷��̾���� �Ÿ��� ���� ���� ���� ������ ����
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
         // ���� ���� ���� ���� ��ٿ� �ð� ����
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
    void Skill() //�Ÿ��� �ֶ��� �Ѿ� �߻�
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
