using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour
{
    [Header("Stat")]
    public Action<State> Action;
    public float Speed;
    public int Damage;

    private NavMeshAgent agent;

    public enum State { Idle, Patrol, Chase, Special, Death}

    public State CurrentState;

    public abstract void GoToState(State _state);
    public abstract void Patrol(Transform _a, Transform _b);
    public abstract void Chase(Transform _target);
    public abstract void AddBehaviour(Action<State> _action);

    public virtual void Init()
    {
        
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
