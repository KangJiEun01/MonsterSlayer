using Unity.VisualScripting;
using UnityEngine;

public class Boss02NewAi : MonoBehaviour //보스 1 : 3초마다 따라와서 공격하고 3초 대기
    //보스 2 : 계속 따라와서 공격하고 총알 히트하면 히트애니메니션 재생과 함께 잠시 정지. 다시 따라옴 (맞추면서 도망쳐야 클리어 가능)
{
    GameObject player;
    Animator animator;

    bool startAtt = false;
    bool Mode = false; //1, 2 공격모드
    public bool _attack = false; //공격on, off상태

    float BossSpeed = 6;
    float _hp = 5;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform);
        animator = GetComponent<Animator>();
        animator.Play("In"); //추후에 인으로 바꿈 
        //animator.Play("2_Atk1"); 
        //animator.Play("2_Atk2");
        //animator.Play("2_Hit");
        //animator.Play("2_Run"); 
        //animator.Play("2_Idle");
        Invoke("UpdateAttack", 10f);
    }
    void UpdateAttack()
    {
        startAtt = true;
        animator.Play("2_Run");
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
        _hp = GetComponent<Target>().Hp;
        if (_hp > 0 && GetComponent<Target>().InDamage)
        {
            GetComponent<Boss02Hit>().enabled = true;
        }
        else if(_hp == 0)
        {
            GetComponent<Boss02Dead>().enabled = true;
            GetComponent<Boss02NewAi>().enabled = false;
            GetComponent<Boss02Attack01>().enabled = false;
            GetComponent<Boss02Attack02>().enabled = false;
            GetComponent<Boss02Hit>().enabled = false;
            GetComponent<Target>().enabled = false;
        }
        if (_hp > 0 && startAtt)
        {
            transform.LookAt(player.transform);
            Vector3 playerVector = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            if (GetComponent<Boss02Attack01>().enabled == false && GetComponent<Boss02Attack02>().enabled == false && GetComponent<Boss02Hit>().enabled == false && GetComponent<Boss02Dead>().enabled == false)
            {
                if (Vector3.Distance(player.transform.position, transform.position) > 10f) //Y축 빼고 따라오게 바꾸기 new Ve3
                {
                    animator.Play("2_Run");
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
