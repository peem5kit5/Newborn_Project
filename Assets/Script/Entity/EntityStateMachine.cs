using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityStateMachine
{
    private EntityStateBase currentState;
    private Entity owner;

    public Dictionary<string, EntityStateBase> StateDict => stateDict;
    private Dictionary<string,EntityStateBase> stateDict = new Dictionary<string, EntityStateBase>();

    public EntityStateMachine(Entity _entity, EntityStateBase _base)
    {
        this.owner = _entity;
        currentState = _base;
    }

    public void AddLength(string _id ,EntityStateBase _state) => stateDict.Add(_id ,_state);
    public void Enter() => currentState.Enter();
    public void Update() => currentState.Update();
    public void Exit() => currentState.Exit();

    public void ChangeState(EntityStateBase _stateBase) => currentState = _stateBase;
}

public abstract class EntityStateBase
{
    protected Entity owner;

    public EntityStateBase(Entity _entity) => this.owner = _entity;

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class IdleState : EntityStateBase
{
    public IdleState(Entity _entity) : base(_entity) { }

    public float MaxCooldown;
    public float CurrentCooldown;

    public override void Enter() 
    {
        owner.Agent.isStopped = true;
        CurrentCooldown = MaxCooldown;
    } 

    public override void Update()
    {
        CurrentCooldown -= Time.deltaTime;

        if(CurrentCooldown <= 0)
            Exit();
    }

    public override void Exit()
    {
        var _stateMachine = owner.StateMachine;
        var _patrol = owner.StateMachine.StateDict["Patrol"];
        _stateMachine.ChangeState(_patrol);
    }
}

public class PatrolState : EntityStateBase
{
    public PatrolState(Entity _entity) : base(_entity) { }

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
        Vector3 _randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f) * DistanceToPatrol, 0, UnityEngine.Random.Range(-1f, 1f)).normalized * DistanceToPatrol;

        _randomDirection += owner.transform.position;

        UnityEngine.AI.NavMeshHit _navHit;
        UnityEngine.AI.NavMesh.SamplePosition(_randomDirection, out _navHit, DistanceToPatrol, -1);

        return _navHit.position;
    }

    public override void Update()
    {
        if (agent.remainingDistance <= 0.5f)
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
    public ChaseState(Entity _entity) : base(_entity) { }
    public Transform Target;

    private UnityEngine.AI.NavMeshAgent agent;

    public override void Enter()
    {
        agent = owner.Agent;
        agent.SetDestination(Target.position);
    }

    public void SetTarget(Transform _target) => Target = _target;

    public override void Update()
    {

    }

    public override void Exit()
    {
        var _stateMachine = owner.StateMachine;
        var _idle = owner.StateMachine.StateDict["Idle"];
        _stateMachine.ChangeState(_idle);
    }
}
