using System.Collections;
using UnityEngine;

public class RhinoPatrol : MonoBehaviour
{
    Animator rhinoAni;

    public float moveSpeed = 3.0f; // 속도

    private Vector3 targetPosition;

    private void Start()
    {
        rhinoAni = GetComponent<Animator>();
        BossAttackRoutine();
        rhinoAni.Play("Walk");
        SetRandomTargetPosition();
    }

    private IEnumerator BossAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            SetRandomTargetPosition();
            yield return StartCoroutine(Patrol()); // 이동 코루틴을 호출
            rhinoAni.Play("shout");
            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator Patrol()
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            SetRandomTargetPosition();
            yield return null; // 한 프레임 대기
        }
    }

    private void SetRandomTargetPosition()
    {
        rhinoAni.Play("Walk");
        float x = Random.Range(-10.0f, 10.0f);
        float z = Random.Range(-10.0f, 10.0f);
        targetPosition = new Vector3(x, 0.0f, z);
        transform.LookAt(targetPosition);
    }
}