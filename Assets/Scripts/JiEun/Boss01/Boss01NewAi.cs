using System.Collections;
using UnityEngine;

public class Boss01NewAi : MonoBehaviour
{
    [SerializeField] GameObject player;

    Animator animator;
    Transform BossTrans;

    public int BossHp = 100;
    int PlayerHP = 100; //���߿� �÷��̾� �ڵ�� �̵�

    bool Mode = false; //1, 2 ���ݸ��
    public bool _attack = false; //����on, off����
    void Start()
    {
        BossTrans = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        animator.Play("In");
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
            if (Mode == false) //2�� ����
            {
                if (_attack == false)
                {
                    _attack = true;
                    Atk02();
                    _attack = false;
                }
                Mode = true;
            }
            else if (Mode == true)//1�� ����
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
        //else if(BossHp<30)
        //{
        //    if (Mode == false) //ȸ������
        //    {
        //        if (_attack == false)
        //        {
        //            _attack = true;
        //            GetComponent<BossSkill01>().enabled = true;
        //            _attack = false;
        //        }
        //        Mode = true;
        //    }

        //}
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BossHp = 10;
            GetComponent<BossSkill01>().enabled = true;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            BossHp = 0;
            GetComponent<Boss01Dead>().enabled = true;
        }
    }
    void Atk02()
    {
        GetComponent<BossAttack02>().enabled = true;
        //animator.Play("1_Atk2");// �浹�ϸ� HP ���� �߰�
        //Invoke("CameraSk", 0.5f);
    }
    void Atk01()
    {
        GetComponent<BossAttack01>().enabled = true;
        //animator.Play("1_Atk2");// �浹�ϸ� HP ���� �߰�
        //Invoke("CameraSk", 0.5f);
    }
    public int getBossHP()
    {
        return BossHp;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHP -= 10;
        }
    }
}


