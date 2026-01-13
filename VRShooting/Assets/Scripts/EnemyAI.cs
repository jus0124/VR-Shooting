using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//요원(agent=enemy)에게 목적지를 알려줘서 목적지로 이동하게 한다.
//상태를 만들어서 제어하고 싶다.
// Idle : Player를 찾는다, 찾았으면 Run상태로 전이하고 싶다.
//Run : 타겟방향으로 이동(요원)
//Attack : 일정 시간마다 공격

public class EnemyAI : MonoBehaviour
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
        }
        else if (state == State.Run)
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
        if (distance > 8)
        {
            state = State.Run;
            anim.SetTrigger("Run");
        }
    }

    private void UpdateRun()
    {


        
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= 8)
        {
            state = State.Attack;
            anim.SetTrigger("Attack");
        }

        
        agent.speed = 25f;
        
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