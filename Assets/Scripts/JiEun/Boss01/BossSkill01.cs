
using UnityEngine;

public class BossSkill01 : MonoBehaviour
{
    GameObject player02;
    GameObject camera;

    float BossSpeed = 500;
    float initialRotationSpeed = 50f; // 초기 속도
    float maxRotationSpeed = 100f; // 최대 속도
    float circleRadius = 15f; // 크기
    Vector3 centerPosition;
    float angle;
    float rotationSpeed;

    float _BossHp;
    void Start()
    {
        player02 = GameObject.FindGameObjectWithTag("Player");
        camera = Camera.main.gameObject;
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
        Invoke("Skill01", 3f);
    }
    void Skill01()
    {
        //transform.LookAt(player02.transform);

        angle += BossSpeed * Time.deltaTime; //애니메이션의 일정 부분만 반복하게 하고 싶으면 새로 만들?
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
        if (Input.GetKeyDown(KeyCode.Alpha2))
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
