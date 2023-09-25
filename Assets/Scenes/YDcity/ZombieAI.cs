using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public WayPointGroup wayPointGroup; // WayPointGroup 스크립트
    public Transform player; // 플레이어의 위치
    private Animator animator; 
    private NavMeshAgent navMeshAgent; 
    private int currentWaypointIndex = 0;
    private float attackRange = 2.0f; // 공격 범위
    private float chaseRange = 10.0f; // 추적 범위
    private float returnRange = 1.0f; // 되돌아가는 범위
    private float idleDuration = 1.0f; // Idle 상태 지속
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

        // 초기 포인트로 이동
        if (wayPointGroup.IsValid(currentWaypointIndex))
        {
            navMeshAgent.SetDestination(wayPointGroup.GetPoint(currentWaypointIndex));
            SetCharacterState(CharacterState.Walk);
        }
    }

    private void Update()
    {
        // 현재 위치와 목표 위치 사이의 거리
        float distanceToTarget = Vector3.Distance(transform.position, wayPointGroup.GetPoint(currentWaypointIndex));

        // 추적 범위 내에 플레이어가 있을 때
        if (Vector3.Distance(transform.position, player.position) <= chaseRange)
        {
            Debug.Log(Vector3.Distance(transform.position, player.position));
            // 공격 범위 내에 플레이어가 있을 때
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {

                SetCharacterState(CharacterState.Attack);
                AttackPlayer(); // 플레이어를 공격하는 함수
            }
            else
            {
                // 플레이어를 향해 뛰기
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
                // 서있기 ? ?? 
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

    // 캐릭터 상태 설정
    private void SetCharacterState(CharacterState state)
    {
        if (characterState != state)
        {
            characterState = state;
            // 애니메이션 상태
            switch (state)
            {
                case CharacterState.Idle:
                    animator.SetTrigger("Idle");
                    break;
                case CharacterState.Walk:
                    animator.SetTrigger("Walking");
                    navMeshAgent.speed = 1.5f; //속도
                    break;
                case CharacterState.Run:
                    animator.SetTrigger("Run");
                    navMeshAgent.speed = 3.5f; //속도
                    break;
                case CharacterState.Attack:
                    animator.SetTrigger("Attack");
                    break;
            }
        }
    }

    // 플레이어를 공격
    private void AttackPlayer()
    {
        // 플레이어와의 거리가 1 이하
        if (Vector3.Distance(transform.position, player.position) <= 1.0f)
        {
            SetCharacterState(CharacterState.Attack);
            //데미지 넣어야도ㅓㅣㅁ
            
        }
        else
        {
            // 플레이어가 멀어지면 다시 쫓아가기
            SetCharacterState(CharacterState.Run);
            navMeshAgent.SetDestination(player.position);
        }
    }

    private void Death()
    {

    }
}
