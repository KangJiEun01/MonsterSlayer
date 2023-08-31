using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HelperAI : GenericSingleton<HelperAI>
{
    NavMeshAgent nav;
    GameObject target;
    Animator animator;

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Physics.CheckSphere(transform.position, 2, 1 << LayerMask.NameToLayer("Player")))
        {
            nav.SetDestination(transform.position);
            animator.SetBool("IsIdle", true);
        }
        else
        {
            nav.SetDestination(target.transform.position);
            animator.SetBool("IsIdle", false);
        }
    }
}
