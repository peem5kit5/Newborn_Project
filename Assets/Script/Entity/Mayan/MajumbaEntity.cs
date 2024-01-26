using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MajumbaEntity : Entity
{
    [Header("Peaceful Stats")]
    public float IdlingCooldown = 5f;
    public float DistanceToPatrol = 5f;

    [Space]

    [Header("Chase Stats")]
    public float ChaseDistance = 5f;
    public float AttackRange = 5f;

    private float currentIdlingCooldown;

    public override void Init()
    {
        base.Init();
        OnAction += Handler;
        OnAction.Invoke(CurrentState);
    }

    public override void Handler(State _state)
    {
        StartCoroutine(Pending(_state));
    }

    private IEnumerator Pending(State _state)
    {
        CurrentState = _state;
        while (true)
        {
            switch (CurrentState)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Patrol:
                    Patrol(RandomPosition());
                    break;
                case State.Chase:
                    Chase();
                    break;
                case State.Special:
                    AddBehaviour(OnAction);
                    break;
            }
        }
    }

    public override void Idle()
    { 
        if(currentIdlingCooldown > 0)
        {
            currentIdlingCooldown -= Time.deltaTime * 2;
            agent.isStopped = true;
        }
        else
        {
            currentIdlingCooldown = IdlingCooldown;
            OnAction.Invoke(State.Patrol);
        }      
    }

    public override void Patrol(Vector3 _pos)
    {
        if(agent.isStopped == false)
            agent.SetDestination(_pos);

        if(agent.remainingDistance <= 0.1)
            OnAction.Invoke(State.Idle);
    }

    private Vector3 RandomPosition()
    {
        Vector3 _randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f) * DistanceToPatrol, 0, UnityEngine.Random.Range(-1f, 1f)).normalized * DistanceToPatrol;

        _randomDirection += transform.position;

        NavMeshHit _navHit;
        NavMesh.SamplePosition(_randomDirection, out _navHit, DistanceToPatrol, -1);

        return _navHit.position;
    }

    public override void Chase()
    {

    }

    public override void SetTarget(Transform _target) => CurrentTarget = _target;

    public override void AddBehaviour(Action<State> _action)
    {
        throw new NotImplementedException();
    }

    
}
