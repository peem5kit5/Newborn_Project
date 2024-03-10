using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityStateMachine
{
    private EntityStateBase currentState;
    private Entity owner;

    public Dictionary<string, EntityStateBase> StateDict => stateDict;
    private Dictionary<string, EntityStateBase> stateDict = new Dictionary<string, EntityStateBase>();

    public EntityStateMachine(Entity _entity, EntityStateBase _base)
    {
        this.owner = _entity;
        currentState = _base;
        currentState.Enter();
    }

    public void AddLength(string _id, EntityStateBase _state) => stateDict.Add(_id, _state);
    public void Enter() => currentState.Enter();
    public void Update() => currentState.Update();
    public void Exit() => currentState.Exit();

    public void ChangeState(EntityStateBase _stateBase)
    {
        //currentState.Exit();
        currentState = _stateBase;
        currentState.Enter();
    }
}

public abstract class EntityStateBase
{
    protected Entity owner;

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class IdleState : EntityStateBase
{
    public IdleState(Entity _entity) => owner = _entity;

    public float MaxCooldown;
    public float CurrentCooldown;

    public override void Enter()
    {
        Debug.Log("Enter Idle");
        owner.Agent.isStopped = true;
        CurrentCooldown = MaxCooldown;
    }

    public override void Update()
    {
        Debug.Log("Updated Idle");
        if (CurrentCooldown < MaxCooldown)
            CurrentCooldown -= Time.deltaTime / 2;
        else
            Exit();
    }

    public override void Exit()
    {
        Debug.Log("Exit Idle");
        var _stateMachine = owner.StateMachine;
        var _patrol = owner.StateMachine.StateDict["Patrol"];
        _stateMachine.ChangeState(_patrol);
        CurrentCooldown = MaxCooldown;
    }
}

public class PatrolState : EntityStateBase
{
    public PatrolState(Entity _entity) => owner = _entity;

    public float DistanceToPatrol;
    private UnityEngine.AI.NavMeshAgent agent;

    public override void Enter()
    {
        agent = owner.Agent;
        agent.isStopped = false;
        agent.SetDestination(RandomPosition());
    }

    private Vector3 RandomPosition()
    {
        float _randomX = UnityEngine.Random.Range(-1f, 1f);
        float _randomZ = UnityEngine.Random.Range(-1f, 1f);

        Vector3 _randomDirection = new Vector3(_randomX, 1, _randomZ).normalized * DistanceToPatrol;

        Vector3 _newPosition = owner.transform.position + _randomDirection;

        UnityEngine.AI.NavMeshHit _navHit;
        if (UnityEngine.AI.NavMesh.SamplePosition(_newPosition, out _navHit, DistanceToPatrol, -1))
            return _navHit.position;
        else
            return _newPosition; 
    }

    public override void Update()
    {
        if (agent.remainingDistance <= 0.1f)
            Exit();
    }

    public override void Exit()
    {
        var _stateMachine = owner.StateMachine;
        var _idle = owner.StateMachine.StateDict["Idle"];
        _stateMachine.ChangeState(_idle);
    }
}

public class ChaseState : EntityStateBase
{
    public ChaseState(Entity _entity) => owner = _entity;

    public Transform Target;
    public float AttackRange;
    public LayerMask TargetLayer;

    public virtual void SetTarget(Transform _target) => Target = _target;
    public virtual void SetAttackRange(float _value) => AttackRange = _value;
    public virtual void SetTargetLayer(LayerMask _layer) => TargetLayer = _layer;

    private UnityEngine.AI.NavMeshAgent agent;

    public override void Enter()
    {
        Debug.Log("Enter Chase");
        agent = owner.Agent;
        agent.SetDestination(Target.position);
    }

    public override void Update()
    {
        Debug.Log("Updated Chase");
        Collider[] _colliders = Physics.OverlapBox(owner.transform.position, owner.transform.localScale / 2, Quaternion.identity, TargetLayer);

        foreach (Collider _col in _colliders)
        {
            if (_col.gameObject.layer == TargetLayer)
                Attack();
        }
    }

    public virtual void Attack()
    {

    }

    public override void Exit()
    {
        Debug.Log("Exit Chase");
        var _stateMachine = owner.StateMachine;
        var _idle = owner.StateMachine.StateDict["Idle"];
        _stateMachine.ChangeState(_idle);
    }
}

