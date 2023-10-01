using Unity.VisualScripting;
using UnityEngine;

public class Boss02NewAi : MonoBehaviour //���� 1 : 3�ʸ��� ����ͼ� �����ϰ� 3�� ���
    //���� 2 : ��� ����ͼ� �����ϰ� �Ѿ� ��Ʈ�ϸ� ��Ʈ�ִϸ޴ϼ� ����� �Բ� ��� ����. �ٽ� ����� (���߸鼭 �����ľ� Ŭ���� ����)
{
    GameObject player;
    Animator animator;

    bool startAtt = false;
    bool Mode = false; //1, 2 ���ݸ��
    public bool _attack = false; //����on, off����

    float BossSpeed = 6;
    float _hp = 5;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform);
        animator = GetComponent<Animator>();
        animator.Play("In"); //���Ŀ� ������ �ٲ� 
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
                if (Vector3.Distance(player.transform.position, transform.position) > 10f) //Y�� ���� ������� �ٲٱ� new Ve3
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
