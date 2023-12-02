using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

[Serializable]
public class EntityBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Rigidbody rb;
    public float MoveSpeed;
    public int BaseDamage;
    public Transform ThisTransform;
    public Entity.CreatureLivingTime CreatureLiving;
    public EntityAttack Attack;
    public void ChaseNon_Intellect(Transform _target)
    {
        Vector3 _direction = _target.position - ThisTransform.transform.position;
        _direction.Normalize();

        rb.velocity = (_direction * MoveSpeed).normalized * Time.deltaTime;
    }

    public void Chase_Intellect(Transform _target)
    {
        navMeshAgent.SetDestination(_target.position);
        navMeshAgent.speed = MoveSpeed;
    }

    public void Interactable()
    {

    }
    public void Attacking()
    {

    }
    public void Magic()
    {

    }
    public void Eating()
    {

    }
    public void Sleeping()
    {
    
    }
    public void WanderAsGroup(Transform _target)
    {

    }

    public void WanderLone(Transform _target)
    {

    }
}
