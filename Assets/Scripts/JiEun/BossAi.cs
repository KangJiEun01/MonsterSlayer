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
        
        animator.Play("Idle"); //기본대기
        //animator.Play("1_Atk1"); // 두손 들어 땅치기
        //animator.Play("1_Atk2"); //손벽짝
        //animator.Play("Stage"); //부서짐 Dead랑 같음
        //animator.Play("Dead");
        //animator.Play("1_Run"); //대기
        //animator.Play("1_Skill1"); //소리 치고 빙글빙글 돌기 ex)맵 돌면서 충돌하면 플레이어 날아감 

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
        animator.Play("1_Atk2");// 충돌하면 HP 감소
    }
}
