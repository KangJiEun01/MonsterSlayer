using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02NewAi : MonoBehaviour //보스 1 : 3초마다 따라와서 공격하고 3초 대기
    //보스 2 : 계속 따라와서 공격하고 총알 히트하면 히트애니메니션 재생과 함께 잠시 정지. 다시 따라옴 (맞추면서 도망쳐야 클리어 가능)
{
    [SerializeField] Transform player02;
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
        animator.Play("In"); //추후에 인으로 바꿈 
        //animator.Play("2_Atk1"); 
        //animator.Play("2_Atk2");
        //animator.Play("2_Hit");
        //animator.Play("2_Run"); 
        //animator.Play("2_Idle");
    }

    private void StartAttack()
    {
        int Rand = Random.Range(0, 2);
        Debug.Log(Rand);
            if (Rand == 0) //2번 공격
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
            GetComponent<Boss02Attack02>().enabled = true;
            //animator.Play("1_Atk2");// 충돌하면 HP 감소 추가
            //Invoke("CameraSk", 0.5f);
        }
        void Atk01()
        {
            GetComponent<Boss02Attack01>().enabled = true;
            //animator.Play("1_Atk2");// 충돌하면 HP 감소 추가
            //Invoke("CameraSk", 0.5f);
        }
    }
        void Update()
    {
        transform.LookAt(player02.transform);
        Vector3 playerVector = new Vector3(player02.transform.position.x, 0, player02.transform.position.z);
        if (GetComponent<Boss02Attack01>().enabled == false && GetComponent<Boss02Attack02>().enabled == false && GetComponent<Boss02Hit>().enabled == false && GetComponent<Boss02Dead>().enabled == false)
        {
            if (Vector3.Distance(player02.transform.position, transform.position) > 10f) //Y축 빼고 따라오게 바꾸기 new Ve3
            {
                transform.position = Vector3.MoveTowards(transform.position, playerVector, BossSpeed * Time.deltaTime);
            }
            else
            {
                StartAttack();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) //임시 HP=0
        {
            BossHp = 0;
            GetComponent<Boss02Dead>().enabled = true;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) //임시 히트
        {
            GetComponent<Boss02Hit>().enabled = true;
        }
    }

}
