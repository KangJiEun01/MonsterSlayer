using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class ZombieAI : MonoBehaviour
{
    
    public Transform target;
    NavMeshAgent agent;

    public Animator anim;

    enum State
    {
        Idle,
        Run,
        Attack
    }
    State state;

    void Start()
    {
        state = State.Idle;

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (state == State.Idle)
        {
            UpdateIdle();
        }else if (state == State.Run)
        {
            UpdateRun();
        }
        else if (state == State.Attack)
        {
            UpdateAttack();
        }
        
    }
   
    private void UpdateAttack()
    {
        agent.speed = 0;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > 2)
        {
            state = State.Run;
            anim.SetTrigger("Run");
        }
    }

    private void UpdateRun()
    {
        

        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 2)
        {
            state = State.Attack;
            anim.SetTrigger("sword attack");
        }

        agent.speed = 3.5f;
        agent.destination = target.transform.position;

    }

    private void UpdateIdle()
    {
        agent.speed = 0;
        target = GameObject.Find("Player").transform;
        if (target != null)
        {
            state = State.Run;
            anim.SetTrigger("Run");
        }
    }
}