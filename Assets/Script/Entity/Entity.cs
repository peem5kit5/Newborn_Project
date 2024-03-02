using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour
{
    [Header("Stat")]
    public float Speed;
    public int Damage;

    public NavMeshAgent Agent;
    public Collider Col;
    public Transform CurrentTarget;
    public float IdleTime, CurrentIdleTime;

    public EntityStateMachine StateMachine;

    public abstract void SetTarget(Transform _target);

    public IdleState IdleState;
    public PatrolState PatrolState;
    public ChaseState ChaseState;

    public virtual void Init()
    {
        Agent = GetComponent<NavMeshAgent>();
        Col = GetComponent<Collider>();

        IdleState = new IdleState(this);
        PatrolState = new PatrolState(this);
        ChaseState = new ChaseState(this);

        StateMachine.ChangeState(IdleState);
        StateMachine.AddLength("Idle", IdleState);
        StateMachine.AddLength("Patrol", PatrolState);
        StateMachine.AddLength("Chase", ChaseState);
    }

    public abstract void ChangeUpdate(EntityStateBase _state);
    public abstract void ChangeUpdate(string _id);

    public virtual Vector3 IsManipulatedPosition(Vector3 _pos)
    {
        return _pos;
    }

    public virtual bool IsUsingTrueFalse(bool _conditionMet)
    {
        return _conditionMet;
    }

    public virtual int IsManipulatedValue(int _manipulatedValue)
    {
        return _manipulatedValue;
    }

    public virtual Event_SO IsManipulatedEvent(Event_SO _eventSO)
    {
        return _eventSO;
    }
}
