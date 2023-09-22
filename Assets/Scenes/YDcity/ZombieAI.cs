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

    private bool _isRun = false;
    private bool _isIdle = false;
    private bool _isWalking = false;
    private bool _isAttack = false;
    private bool _isShootGun = false;
    private bool _attackTimerActive = false;
    private float _attackTimerDuration = 0.5f;
    private float _attackTimer = 0.0f;
    private float _shootGunCheckInterval = 1.0f;
    private float _shootGunCheckTimer = 0.0f;


    public List<Transform> wayPoints;
    public int nextIdx = 0;



    private void Start()
    {
        _isRun = false;
        _isIdle = false;
        _isWalking = true;
        _isAttack = false;
        _isShootGun = false;

        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking= false;

        var group = GameObject.Find("WayPointGroup");

        if(group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);
        }

        MoveWayPoint();
    }

    private void MoveWayPoint()
    {
        if(agent.isPathStale)
        {
            return;
        }

        agent.destination = wayPoints[nextIdx].position;   
        agent.isStopped= false;
    }

    private void Update()
    {
        if(agent.remainingDistance <= 0.5f)
        {
            nextIdx = UnityEngine.Random.Range(0, wayPoints.Count);
            MoveWayPoint();
        }
        //UpdateAnimation();

        
        if (_attackTimerActive)
        {
            _attackTimer -= Time.deltaTime;
            if (_attackTimer <= 0)
            {
                _attackTimerActive = false;
            }
        }

        
        if (_isShootGun && _shootGunCheckTimer > 0)
        {
            _shootGunCheckTimer -= Time.deltaTime;
            if (_shootGunCheckTimer <= 0)
            {
                _shootGunCheckTimer = 0;
                
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance <= 2)
                {
                    _isShootGun = true;
                }
            }
        }

        //if (_isIdle)
        //{
        //    UpdateIdle();
        //}
        else if (_isRun)
        {
            UpdateRun();
        }
        else if (_isWalking)
        {
            UpdateWalking();
        }
        else if (_isAttack)
        {
            if (!_attackTimerActive)
            {
                Attack();
                _attackTimerActive = true;
                _attackTimer = _attackTimerDuration;
            }
        }
    }

    private void UpdateAnimation()
    {
        anim.SetBool("isRun", _isRun);
        anim.SetBool("isIdle", _isIdle);
        anim.SetBool("isWalking", _isWalking);
        anim.SetBool("isAttack", _isAttack);
        anim.SetBool("isShootGun", _isShootGun);
    }

    private void UpdateRun()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 0.5f)
        {
            Attack();
        }
        else if (distance <= 2 && !_isShootGun)
        {
            _isShootGun = true;
            _shootGunCheckTimer = _shootGunCheckInterval;
        }
        else
        {
            _isRun = true;
            agent.speed = 2.5f;
            agent.destination = target.transform.position;
        }
    }

    private void UpdateWalking()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 8)
        {
            _isRun = true; 
            _isWalking = false;
        }
    }

    private void Attack()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 0.5f)
        {
            _isAttack = true;
            agent.speed = 0; 
        }
        else
        {
            _isRun = false;
            _isShootGun = false;
            _isAttack = true;
            agent.speed = 0; 
        }
    }
}
