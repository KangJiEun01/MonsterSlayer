using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill01 : MonoBehaviour
{
    [SerializeField] GameObject player02;

    float BossSpeed = 500;
    float initialRotationSpeed = 50f; // 초기 속도
    float maxRotationSpeed = 100f; // 최대 속도
    float circleRadius = 15f; // 크기
    Vector3 centerPosition;
    float angle;
    float rotationSpeed;

    int _BossHp;
    void Start()
    {

    }
    private void OnEnable()
    {
        GetComponent<Boss01NewAi>().enabled = false;
        GetComponent<Animator>().Play("1_Skill1");
        centerPosition = transform.position;
        _BossHp = GetComponent<Boss01NewAi>().getBossHP();
    }
    void Update()
    {
        //transform.LookAt(player02.transform);

        angle += BossSpeed * Time.deltaTime;
        if (angle >= 360f)
        {
            angle -= 360f;
        }

        Vector3 offset = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad)) * circleRadius;
        Vector3 targetPosition = centerPosition + offset;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, BossSpeed * Time.deltaTime);

        float speedIncrease = (maxRotationSpeed - initialRotationSpeed) / 10f;
        rotationSpeed = Mathf.Min(rotationSpeed + speedIncrease * Time.deltaTime, maxRotationSpeed);

        Quaternion targetRotation = Quaternion.LookRotation(player02.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (Input.GetMouseButtonDown(1))
        {
            _BossHp = 0;
        }
        if (_BossHp <= 0)
        {
            GetComponent<Boss01Dead>().enabled = true;
            GetComponent<BossSkill01>().enabled = false;
        }
    }
}
