using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02NewAi : MonoBehaviour
{
    Animator animator;
    Transform BossTrans;

    public int BossHp = 100;

    bool Mode = false; //1, 2 공격모드
    public bool _attack = false; //공격on, off상태

    float BossSpeed = 8;

    void Start()
    {
        animator = GetComponent<Animator>();
        BossTrans = GetComponent<Transform>();
        animator.Play("In");
        //animator.Play("2_Atk1"); 
        //animator.Play("2_Atk2");
        //animator.Play("2_Hit");
        //animator.Play("2_Run"); 
        //animator.Play("2_Idle");
        StartCoroutine(BossAttackRoutine());
    }
    private IEnumerator BossAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            StartAttack();

            yield return new WaitForSeconds(3f);
            GetComponent<BossAttack02>().enabled = false;
            GetComponent<BossAttack01>().enabled = false;
        }
    }
    private void StartAttack()
    {
        if (BossHp > 30)
        {
            if (Mode == false) //2번 공격
            {
                if (_attack == false)
                {
                    _attack = true;
                    Atk02();
                    _attack = false;
                }
                Mode = true;
            }
            else if (Mode == true)//1번 공격
            {
                if (_attack == false)
                {
                    _attack = true;
                    Atk01();
                    _attack = false;
                }
                Mode = false;
            }
        }
        void Atk02()
        {
            GetComponent<BossAttack02>().enabled = true;
            //animator.Play("1_Atk2");// 충돌하면 HP 감소 추가
            //Invoke("CameraSk", 0.5f);
        }
        void Atk01()
        {
            GetComponent<BossAttack01>().enabled = true;
            //animator.Play("1_Atk2");// 충돌하면 HP 감소 추가
            //Invoke("CameraSk", 0.5f);
        }
    }
        void Update()
    {
        
    }
}
