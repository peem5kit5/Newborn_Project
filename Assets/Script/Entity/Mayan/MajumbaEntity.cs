using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class MajumbaEntity : Entity
{
    [Header("Peaceful Stats")]
    public float IdlingCooldown = 5f;
    public float DistanceToPatrol = 2f;

    [Space]

    [Header("Chase Stats")]
    public float ChaseDistance = 5f;
    public float AttackRange = 5f;

    public override void Init()
    {
        base.Init();

        IdleState.MaxCooldown = IdlingCooldown;
        IdleState.CurrentCooldown = IdlingCooldown;

        PatrolState.SetRange();
    }

    public void Update() => StateMachine.Update();

    public override void ChangeUpdate(EntityStateBase _state) =>  StateMachine.ChangeState(_state);

    public override void ChangeUpdate(string _id)
    {
        if (StateMachine.StateDict.TryGetValue(_id, out var _state))
            StateMachine.ChangeState(_state);
    }

    public override void SetTarget(Transform _target) => CurrentTarget = _target;
}
