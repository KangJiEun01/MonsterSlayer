using UnityEngine;

public class BossAttack01 : MonoBehaviour
{
    GameObject player02;

    float BossSpeed = 20;
    float _BossHp;

    private void OnEnable()
    {
        GetComponent<Animator>().Play("1_Atk1");
        Invoke("CameraMove", 1.7f);
        _BossHp = GetComponent<Boss01NewAi>().getBossHP();
    }
    void Start()
    {
        player02 = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        Vector3 playerVector = new Vector3(player02.transform.position.x, 0, player02.transform.position.z);
        transform.LookAt(player02.transform);
        if (Vector3.Distance(player02.transform.position, transform.position) > 10f) //Y축 빼고 따라오게 바꾸기 new Ve3
        {
            transform.position = Vector3.MoveTowards(transform.position, playerVector, BossSpeed * Time.deltaTime);
        }
    }
}


