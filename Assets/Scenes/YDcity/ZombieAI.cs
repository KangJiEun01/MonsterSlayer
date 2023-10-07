using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public WayPointGroup wayPointGroup; // WayPointGroup 스크립트
    private Transform player; // 플레이어의 위치
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private int currentWaypointIndex = 0;
    private float attackRange = 5.0f; // 공격 범위
    private float chaseRange = 10.0f; // 추적 범위
    private float returnRange = 1.0f; // 되돌아가는 범위
    private float idleDuration = 1.0f; // Idle 상태 지속
    private float timeSinceLastAttack = 0.0f; // 마지막 공격 시간
    private float attackCooldown = 2.0f; // 공격 쿨다운 시간
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
        player = GameObject.FindGameObjectWithTag("Player").transform; // "Player" 태그를 가진 오브젝트를 찾아서 플레이어로 설정
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // 초기 포인트로 이동
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

        // 현재 위치와 목표 위치 사이의 거리
        float distanceToTarget = Vector3.Distance(transform.position, wayPointGroup.GetPoint(currentWaypointIndex));

        // 추적 범위 내에 플레이어가 있을 때
        if (Vector3.Distance(transform.position, player.position) <= chaseRange)
        {
            // 공격 범위 내에 플레이어가 있을 때
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                SetCharacterState(CharacterState.Attack);

                // 플레이어와의 거리가 5 이하이므로 제자리에서 공격
                if (Time.time - timeSinceLastAttack >= attackCooldown)
                {
                    AttackPlayer();
                    timeSinceLastAttack = Time.time;
                }
            }
            else
            {
                // 플레이어와의 거리가 5보다 크면 쫓아가기
                SetCharacterState(CharacterState.Run);
                navMeshAgent.SetDestination(player.position);
            }
        }
        else
        {
            // 추적 범위 밖에 있을 때
            if (distanceToTarget > returnRange)
            {
                // 걷기 애니메이션 실행하고 포인트 돌기
                SetCharacterState(CharacterState.Walk);
                navMeshAgent.SetDestination(wayPointGroup.GetPoint(currentWaypointIndex));
            }
            else
            {
                if (characterState == CharacterState.Idle) return;
                // 서있기 ?
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

    // 플레이어를 공격하는부분
    private void AttackPlayer()
    {
        // 플레이어에게 데미지 입히는 부분

    }

    private void Death()
    {
        SetCharacterState(CharacterState.Death);
        Destroy(this);
    }
}