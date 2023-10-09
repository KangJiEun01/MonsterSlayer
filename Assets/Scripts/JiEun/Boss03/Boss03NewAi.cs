using UnityEngine;
public class Boss03NewAi : MonoBehaviour
{
    Transform player;
    Animator animator;

    float _hp = 100;
    float chaseDistance = 70f; // �÷��̾� �ִ�Ÿ�
    float attackDistance = 7f; // ���ݰŸ�
    float attackCooldown = 3.1f; // ������ٿ�
    float bossSpeed = 18f;

    bool isAttacking = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
        transform.LookAt(player.position);
        animator = GetComponent<Animator>();
        animator.Play("In");
        Invoke("AttackAct", 7.6f);
        //InvokeRepeating("CheckDistance", 7.8f, 0.5f); //���ʵڽ���//���ʸ��� �ݺ�
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
                //attackDistance ������ ����
                StartAttack();
            }
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        Invoke("StopAttack", attackCooldown);
        int rand = Random.Range(0, 3); //���ݷ���
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
        // �Ÿ��� üũ
    }
    void AttackAct()
    {
        isAttacking = false;
    }
}
