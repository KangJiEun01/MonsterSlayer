using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack02 : MonoBehaviour
{
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

    }
}
