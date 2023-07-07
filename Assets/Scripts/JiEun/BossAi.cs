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
    Vector3 bossCenterPoint;
    [SerializeField] Camera cam;
    float HP = 100;
    //[SerializeField] Transform Test;
    //public Transform target;
    float radius = 3f;
    float shake=1f;

    void Start()
    {
        // BossTrans.position = new Vector3(521, 0, 570); 
        bossCenterPoint = transform.position;
        animator.Play("In"); //�⺻���
        Invoke("CameraSk", 4.7f);
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
        Invoke("DistanceCheck", 8f);
        //Skill1();
        //CameraSk();
        if(HP < 0)
        {
            Dead();
        }
    }
    void DistanceCheck() // �Ÿ��� üũ���� ���� �ð����� üũ�սô�.
    {
        if (Vector3.Distance(player02.position, BossTrans.position) >8f)
        {
            Playerfollow();
        }
        else if (Vector3.Distance(player02.position, BossTrans.position) > 12f)
        {
            //Skill1();
        }
        else if (Vector3.Distance(player02.position, BossTrans.position) < 8f)
        {
            Atk02();
        }
    }
    void Playerfollow()
    {
        transform.LookAt(player02);
        transform.position = Vector3.MoveTowards(transform.position, player02.position, BossSpeed * Time.deltaTime);
    }
    void Atk01()
    {

    }
    void Atk02()
    {
        GetComponent<BossAttack02>().enabled = true;
        transform.LookAt(player02);
        Invoke("CameraSk", 1.5f);
        //animator.Play("1_Atk2");// �浹�ϸ� HP ���� �߰�
        //Invoke("CameraSk", 0.5f);
    }
    void Skill1() //���ӵ��� ���� ���� //���� ��ġ ����
    {
        GetComponent<Skill1>().enabled = true;
        float angle = Time.time * BossSpeed;
        Vector3 targetPos = bossCenterPoint + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;
        transform.position = targetPos;
    }
    void Dead()
    {
        GetComponent<BossDead>().enabled = true;
    }
    void CameraSk()
    {
        cam.GetComponent<CameraShake>().enabled = true;
    }
    public float SetShake()
    {
        return shake;
    }
}
