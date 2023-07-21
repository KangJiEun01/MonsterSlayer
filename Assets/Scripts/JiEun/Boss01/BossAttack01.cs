using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack01 : MonoBehaviour
{
    [SerializeField] GameObject player02;

    float BossSpeed = 20;

    private void OnEnable()
    {
        GetComponent<Animator>().Play("1_Atk1");
    }
    void Start()
    {

    }
    void Update()
    {
        transform.LookAt(player02.transform);
        if (Vector3.Distance(player02.transform.position, transform.position) > 10f) //Y축 빼고 따라오게 바꾸기 new Ve3
        {
            transform.position = Vector3.MoveTowards(transform.position, player02.transform.position, BossSpeed * Time.deltaTime);
        }
    }
}


