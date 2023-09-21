using UnityEngine;

public class Mimiccontroll : MonoBehaviour
{
    [SerializeField] float RandposXmin;
    [SerializeField] float RandposXmax;
    [SerializeField] float RandposZmin;
    [SerializeField] float RandposZmax;

    float moveSpeed = 1.0f; // 속도
    Vector3 targetPosition;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.forward);
        //transform.forward = Vector3.forward;
        // 목표지점이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        // 도착하면 다음 목표지점
        if (Vector3.Distance(transform.position, targetPosition) < 0.8f)
        {
            //rhinoAni.Play("shout");
            //float time = Random.Range(2.0f, 4.0f);
            //Invoke("SetRandomTargetPosition", 3.4f);
            SetRandomTargetPosition();
        }
    }
    private void SetRandomTargetPosition()
    {
        // 랜덤포지션
        float x = Random.Range(RandposXmin, RandposXmax);
        float z = Random.Range(RandposZmin, RandposZmax);
        targetPosition = new Vector3(x, 4.0f, z);
    }
}
