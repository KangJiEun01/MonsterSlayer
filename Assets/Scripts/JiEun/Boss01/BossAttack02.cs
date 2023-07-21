using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack02 : MonoBehaviour
{
    [SerializeField] GameObject player02;

    float BossSpeed = 20;
    private void OnEnable()
    {
        GetComponent<Animator>().Play("1_Atk2");
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player02.transform.position, transform.position) > 10f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player02.transform.position, BossSpeed * Time.deltaTime);
        }
    }
}
