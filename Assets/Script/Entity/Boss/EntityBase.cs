using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
public abstract class Entity : MonoBehaviour
{
    [Header("Stat")]
    public Action Action;
    public float Speed;
    public int Damage;

    private NavMeshAgent agent;

    public abstract void Patrol(Transform _a, Transform _b);
    public abstract void Chase(Transform _target);
    public void AddBehaviour(Action _action) => Action += _action;
    public void Init()
    {
        
    }

    public Vector3 IsManipulatedPosition(Vector3 _pos)
    {
        return _pos;
    }
    public bool IsUsingTrueFalse(bool _conditionMet)
    {
        return _conditionMet;
    }
    public int IsManipulatedValue(int _manipulatedValue)
    {
        return _manipulatedValue;
    }
    public Event_SO IsManipulatedEvent(Event_SO _eventSO)
    {
        return _eventSO;
    }
}
