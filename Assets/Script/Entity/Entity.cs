using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour
{
    [Header("Stat")]
    public Action<State> OnAction;
    public float Speed;
    public int Damage;

    public NavMeshAgent agent;
    public Collider col;
    public Transform CurrentTarget;

    public enum State { Idle, Patrol, Chase, Special, Death}
    public State CurrentState;

    public abstract void Idle();
    public abstract void Patrol(Vector3 _pos);
    public abstract void Chase();
    public abstract void AddBehaviour(Action<State> _action);
    public abstract void Handler(State _state);
    public abstract void SetTarget(Transform _target);

    public virtual void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        col = GetComponent<Collider>();
    }

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
