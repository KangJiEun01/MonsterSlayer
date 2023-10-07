using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public WayPointGroup wayPointGroup; // WayPointGroup ��ũ��Ʈ
    private Transform player; // �÷��̾��� ��ġ
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private int currentWaypointIndex = 0;
    private float attackRange = 5.0f; // ���� ����
    private float chaseRange = 10.0f; // ���� ����
    private float returnRange = 1.0f; // �ǵ��ư��� ����
    private float idleDuration = 1.0f; // Idle ���� ����
    private float timeSinceLastAttack = 0.0f; // ������ ���� �ð�
    private float attackCooldown = 2.0f; // ���� ��ٿ� �ð�
    private CharacterState characterState = CharacterState.Idle;

    float _hp;


    private enum CharacterState
    {
        Idle,
        Walk,
        Run,
        Attack,
        Death,
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // "Player" �±׸� ���� ������Ʈ�� ã�Ƽ� �÷��̾�� ����
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // �ʱ� ����Ʈ�� �̵�
        if (wayPointGroup.IsValid(currentWaypointIndex))
        {
            navMeshAgent.SetDestination(wayPointGroup.GetPoint(currentWaypointIndex));
            SetCharacterState(CharacterState.Walk);
        }
    }

    private void Update()
    {
        _hp = GetComponent<Target>().Hp;
        if (_hp <= 0)
        {
            Death();
        }

        // ���� ��ġ�� ��ǥ ��ġ ������ �Ÿ�
        float distanceToTarget = Vector3.Distance(transform.position, wayPointGroup.GetPoint(currentWaypointIndex));

        // ���� ���� ���� �÷��̾ ���� ��
        if (Vector3.Distance(transform.position, player.position) <= chaseRange)
        {
            // ���� ���� ���� �÷��̾ ���� ��
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                SetCharacterState(CharacterState.Attack);

                // �÷��̾���� �Ÿ��� 5 �����̹Ƿ� ���ڸ����� ����
                if (Time.time - timeSinceLastAttack >= attackCooldown)
                {
                    AttackPlayer();
                    timeSinceLastAttack = Time.time;
                }
            }
            else
            {
                // �÷��̾���� �Ÿ��� 5���� ũ�� �Ѿư���
                SetCharacterState(CharacterState.Run);
                navMeshAgent.SetDestination(player.position);
            }
        }
        else
        {
            // ���� ���� �ۿ� ���� ��
            if (distanceToTarget > returnRange)
            {
                // �ȱ� �ִϸ��̼� �����ϰ� ����Ʈ ����
                SetCharacterState(CharacterState.Walk);
                navMeshAgent.SetDestination(wayPointGroup.GetPoint(currentWaypointIndex));
            }
            else
            {
                if (characterState == CharacterState.Idle) return;
                // ���ֱ� ?
                SetCharacterState(CharacterState.Idle);
                currentWaypointIndex++;
                if (wayPointGroup.IsValid(currentWaypointIndex) == false)
                {
                    currentWaypointIndex = 0;
                }
                navMeshAgent.SetDestination(wayPointGroup.GetPoint(currentWaypointIndex));
                //SetCharacterState(CharacterState.Idle);
            }
        }
    }

    private void SetCharacterState(CharacterState state)
    {
        if (characterState != state)
        {
            characterState = state;
            switch (state)
            {
                case CharacterState.Idle:
                    animator.SetTrigger("Idle");
                    break;
                case CharacterState.Walk:
                    animator.SetTrigger("Walking");
                    navMeshAgent.speed = 1.5f;
                    break;
                case CharacterState.Run:
                    animator.SetTrigger("Run");
                    navMeshAgent.speed = 3.5f;
                    break;
                case CharacterState.Attack:
                    animator.SetTrigger("Attack");
                    break;
                case CharacterState.Death:
                    animator.SetTrigger("Death");
                    break;
            }
        }
    }

    // �÷��̾ �����ϴºκ�
    private void AttackPlayer()
    {
        // �÷��̾�� ������ ������ �κ�

    }

    private void Death()
    {
        SetCharacterState(CharacterState.Death);
        Destroy(this);
    }
}