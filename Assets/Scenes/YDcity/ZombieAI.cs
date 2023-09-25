using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public WayPointGroup wayPointGroup; // WayPointGroup ��ũ��Ʈ
    public Transform player; // �÷��̾��� ��ġ
    private Animator animator; 
    private NavMeshAgent navMeshAgent; 
    private int currentWaypointIndex = 0;
    private float attackRange = 2.0f; // ���� ����
    private float chaseRange = 10.0f; // ���� ����
    private float returnRange = 1.0f; // �ǵ��ư��� ����
    private float idleDuration = 1.0f; // Idle ���� ����
    private CharacterState characterState = CharacterState.Idle;

    private enum CharacterState
    {
        Idle,
        Walk,
        Run,
        Attack
    }

    private void Start()
    {
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
        // ���� ��ġ�� ��ǥ ��ġ ������ �Ÿ�
        float distanceToTarget = Vector3.Distance(transform.position, wayPointGroup.GetPoint(currentWaypointIndex));

        // ���� ���� ���� �÷��̾ ���� ��
        if (Vector3.Distance(transform.position, player.position) <= chaseRange)
        {
            Debug.Log(Vector3.Distance(transform.position, player.position));
            // ���� ���� ���� �÷��̾ ���� ��
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {

                SetCharacterState(CharacterState.Attack);
                AttackPlayer(); // �÷��̾ �����ϴ� �Լ�
            }
            else
            {
                // �÷��̾ ���� �ٱ�
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
                // ���ֱ� ? ?? 
                SetCharacterState(CharacterState.Idle);
                currentWaypointIndex++;
                if(wayPointGroup.IsValid(currentWaypointIndex) == false)
                {
                    currentWaypointIndex = 0;
                }
                navMeshAgent.SetDestination(wayPointGroup.GetPoint(currentWaypointIndex));
                //SetCharacterState(CharacterState.Idle);
            }
        }
    }

    // ĳ���� ���� ����
    private void SetCharacterState(CharacterState state)
    {
        if (characterState != state)
        {
            characterState = state;
            // �ִϸ��̼� ����
            switch (state)
            {
                case CharacterState.Idle:
                    animator.SetTrigger("Idle");
                    break;
                case CharacterState.Walk:
                    animator.SetTrigger("Walking");
                    navMeshAgent.speed = 1.5f; //�ӵ�
                    break;
                case CharacterState.Run:
                    animator.SetTrigger("Run");
                    navMeshAgent.speed = 3.5f; //�ӵ�
                    break;
                case CharacterState.Attack:
                    animator.SetTrigger("Attack");
                    break;
            }
        }
    }

    // �÷��̾ ����
    private void AttackPlayer()
    {
        // �÷��̾���� �Ÿ��� 1 ����
        if (Vector3.Distance(transform.position, player.position) <= 1.0f)
        {
            SetCharacterState(CharacterState.Attack);
            //������ �־�ߵ��äӤ�
            
        }
        else
        {
            // �÷��̾ �־����� �ٽ� �Ѿư���
            SetCharacterState(CharacterState.Run);
            navMeshAgent.SetDestination(player.position);
        }
    }

    private void Death()
    {

    }
}
