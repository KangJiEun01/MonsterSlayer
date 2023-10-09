using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03LastAi : MonoBehaviour
{
    GameObject player;
    Animator animator;

    bool startAttackHP = false;
    bool startAtt = false;
    bool Mode = false; //1, 2 공격모드
    bool _attack = false; //공격on, off상태

    float BossSpeed = 18;
    float _hp = 1000;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        transform.LookAt(player.transform);
        animator = GetComponent<Animator>();
        animator.Play("In"); //추후에 인으로 바꿈 
        //animator.Play("2_Atk1"); 
        //animator.Play("2_Atk2");
        //animator.Play("2_Hit");
        //animator.Play("2_Run"); 
        //animator.Play("2_Idle");
        Invoke("UpdateAttack", 7.2f);
    }
    void UpdateAttack()
    {
        startAttackHP = true;
        startAtt = true;
        animator.Play("3_Run");
    }
    private void StartAttack()
    {
        int Rand = Random.Range(0, 2);
        Debug.Log(Rand);
        if (Rand == 0 ) //2번 공격
        {
            if (_attack == false)
            {
                _attack = true;
                Atk02();
                _attack = false;
            }
            Mode = true;
        }
        else if (Rand == 1)//1번 공격
        {
            if (_attack == false)
            {
                _attack = true;
                Atk01();
                _attack = false;
            }
            Mode = false;
        }
        void Atk02()
        {
            GetComponent<Boss03Attack01>().enabled = true;
        }
        void Atk01()
        {
            GetComponent<Boss03Skill01>().enabled = true;
        }
    }
    void Update()
    {
        if (startAttackHP)
        {
            _hp = GetComponent<Target>().Hp;
            if (_hp > 0 && GetComponent<Target>().InDamage)
            {
                GetComponent<Boss03Hit>().enabled = true;
            }
            else if (_hp <= 0)
            {
                GetComponent<Boss03Dead>().enabled = true;
                GetComponent<Boss03Attack01>().enabled = false;
                GetComponent<Boss03Skill01>().enabled = false;
                GetComponent<Boss03Attack03>().enabled = false;
                GetComponent<Boss03Hit>().enabled = false;
            }
            if (_hp > 0 && startAtt)
            {
                transform.LookAt(player.transform);
                Vector3 playerVector = new Vector3(player.transform.position.x, 0, player.transform.position.z);
                if (GetComponent<Boss03Attack01>().enabled == false && GetComponent<Boss03Skill01>().enabled == false && GetComponent<Boss03Hit>().enabled == false && GetComponent<Boss03Dead>().enabled == false)
                {
                    if (Vector3.Distance(player.transform.position, transform.position) > 6f) //Y축 빼고 따라오게 바꾸기 new Ve3
                    {
                        animator.Play("3_Run");
                        transform.position = Vector3.MoveTowards(transform.position, playerVector, BossSpeed * Time.deltaTime);
                    }
                    else
                    {
                        StartAttack();
                    }
                }
            }
        }
    }
}
