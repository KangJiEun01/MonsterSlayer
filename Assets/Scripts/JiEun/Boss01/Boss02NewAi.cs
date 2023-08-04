using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02NewAi : MonoBehaviour //���� 1 : 3�ʸ��� ����ͼ� �����ϰ� 3�� ���
    //���� 2 : ��� ����ͼ� �����ϰ� �Ѿ� ��Ʈ�ϸ� ��Ʈ�ִϸ޴ϼ� ����� �Բ� ��� ����. �ٽ� ����� (���߸鼭 �����ľ� Ŭ���� ����)
{
    [SerializeField] Transform player02;
    Animator animator;
    Transform BossTrans;

    public int BossHp = 100;

    bool Mode = false; //1, 2 ���ݸ��
    public bool _attack = false; //����on, off����

    float BossSpeed = 8;

    void Start()
    {
        animator = GetComponent<Animator>();
        BossTrans = GetComponent<Transform>();
        animator.Play("In"); //���Ŀ� ������ �ٲ� 
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
            if (Rand == 0) //2�� ����
            {
                if (_attack == false)
                {
                    _attack = true;
                    Atk02();
                    _attack = false;
                }
                Mode = true;
            }
            else if (Rand == 1)//1�� ����
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
            //animator.Play("1_Atk2");// �浹�ϸ� HP ���� �߰�
            //Invoke("CameraSk", 0.5f);
        }
        void Atk01()
        {
            GetComponent<Boss02Attack01>().enabled = true;
            //animator.Play("1_Atk2");// �浹�ϸ� HP ���� �߰�
            //Invoke("CameraSk", 0.5f);
        }
    }
        void Update()
    {
        transform.LookAt(player02.transform);
        Vector3 playerVector = new Vector3(player02.transform.position.x, 0, player02.transform.position.z);
        if (GetComponent<Boss02Attack01>().enabled == false && GetComponent<Boss02Attack02>().enabled == false && GetComponent<Boss02Hit>().enabled == false && GetComponent<Boss02Dead>().enabled == false)
        {
            if (Vector3.Distance(player02.transform.position, transform.position) > 10f) //Y�� ���� ������� �ٲٱ� new Ve3
            {
                transform.position = Vector3.MoveTowards(transform.position, playerVector, BossSpeed * Time.deltaTime);
            }
            else
            {
                StartAttack();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) //�ӽ� HP=0
        {
            BossHp = 0;
            GetComponent<Boss02Dead>().enabled = true;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) //�ӽ� ��Ʈ
        {
            GetComponent<Boss02Hit>().enabled = true;
        }
    }

}
