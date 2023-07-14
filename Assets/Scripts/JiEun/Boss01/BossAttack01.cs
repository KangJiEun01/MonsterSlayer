using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack01 : MonoBehaviour
{
    float BossSpeed = 20;
    public float Time_Attack03;
    private void OnEnable()
    {
        GetComponent<Animator>().Play("1_Atk1");
        Time_Attack03 = GetComponent<BossAi>().getTime_Attack();
        Invoke("AinFalse", 6f);
    }
    void AinFalse()
    {
        GetComponent<BossAttack01>().enabled = false;
        Time_Attack03 += 6f;
    }
    public float setTime_Attack()
    {
        return Time_Attack03;
    }
    void Start()
    {
    
    }
    void Update()
    {
        
    }
}
