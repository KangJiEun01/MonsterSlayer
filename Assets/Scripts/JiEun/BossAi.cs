using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BossAi : MonoBehaviour
{
    [SerializeField] Animator animator;
    //[SerializeField] GameObject player;
    [SerializeField] Transform player02;
    [SerializeField] Transform BossTrans;
    float BossSpeed = 20;
    //[SerializeField] Transform Test;

    void Start()
    {
        // BossTrans.position = new Vector3(521, 0, 570); 
        
        animator.Play("Idle"); //�⺻���
        //animator.Play("1_Atk1"); // �μ� ��� ��ġ��
        //animator.Play("1_Atk2"); //�պ�¦
        //animator.Play("Stage"); //�μ��� Dead�� ����
        //animator.Play("Dead");
        //animator.Play("1_Run"); //���
        //animator.Play("1_Skill1"); //�Ҹ� ġ�� ���ۺ��� ���� ex)�� ���鼭 �浹�ϸ� �÷��̾� ���ư� 

        //animator.Play("In");
        //Invoke("Update", 3f);
    }
    void Update()
    {
        if (Vector3.Distance(player02.position, BossTrans.position) > 8f)
        {
            Player();
        }
        else if(Vector3.Distance(player02.position, BossTrans.position) < 8f)
        {
            Atk02();
        }
    }
    void Player()
    {
        transform.LookAt(player02);
        transform.position = Vector3.MoveTowards(transform.position, player02.position, BossSpeed * Time.deltaTime);
    }
    void Atk02()
    {
        transform.LookAt(player02);
        animator.Play("1_Atk2");// �浹�ϸ� HP ����
    }
}
