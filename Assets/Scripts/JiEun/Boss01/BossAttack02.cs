using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack02 : MonoBehaviour
{
    float BossSpeed = 20;
    public float Time_Attack02;
    private void OnEnable()
    {
        GetComponent<Animator>().Play("1_Atk2");
        Time_Attack02 = GetComponent<BossAi>().getTime_Attack();
        Invoke("AinFalse", 6f);
    }
    void AinFalse()
    {
        GetComponent<BossAttack02>().enabled = false;
        Time_Attack02 += 6f;
    }
    public float setTime_Attack()
    {
        return Time_Attack02;
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
