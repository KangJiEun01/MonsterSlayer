using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi02 : MonoBehaviour
{
    [SerializeField] Animator animator;
    float BossSpeed = 5;
    int BossHp = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("In");
        StartCoroutine(BossAttackRoutine());
    }
    private IEnumerator BossAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            StartAttack01();

            yield return new WaitForSeconds(3f);
            StartAttack02();
        }
    }
    private void StartAttack01()
    {
        GetComponent<Boss02Attack02>().enabled = false;
        GetComponent<Boss02Attack01>().enabled = true;
    }
    private void StartAttack02()
    {
        GetComponent<Boss02Attack01>().enabled = false;
        GetComponent<Boss02Attack02>().enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BossHp = 0;
            GetComponent<Boss01Dead>().enabled = true;
        }
    }
}
