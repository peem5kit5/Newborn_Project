using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class Entity : MonoBehaviour
{
    [Header("Alignment")]
    public string FactionName;
    public Alignment Faction;
    public enum Alignment
    {
        FatherOfFlesh,
        SisterOfDesired,
        FatherOfGift,
        Natural,
        Good,
        Beast,
        Evil
    }
    [Space]
    [Header("Creature Infomation")]
    public string CreatureName;
    public CreatureType Creature;
    public enum CreatureType
    {
        Humanoid,
        Beast,
        Veggie,
        Spirit,
        MysteriousCreature,

    }
    [Space]
    [Header("Setting")]
    public List<Event_SO> EventList = new List<Event_SO>();
    public int ItemDropRate;
    public Action Behaviour;
    public EntityBehaviour BehaviourScript;

    public Transform PlayerTransform;


    #region Test Will Removed
    public void Awake()
    {
        Init();
    }
    #endregion
    public void Init()
    {

            SetUp();

       
    }
  
    public void Update()
    {
        if(Behaviour != null)
        {
            Behaviour();
        }
        else
        {
            Debug.Log("No Behaviour");
        }
        
    }

    void SetUp()
    {
        //BehaviourScript.rb = GetComponent<Rigidbody>();
        //BehaviourScript.navMeshAgent = GetComponent<NavMeshAgent>();
        CheckingType();
    }

    public void IncreasedMoveSpeed(float _amount)
    {
        BehaviourScript.MoveSpeed += _amount;
    }
    void CheckingType()
    {
        Debug.Log("Assign");
        ClearBehaviour(Behaviour);
        switch (Creature)
        {
            case CreatureType.Beast:
                SetBehaviour(BehaviourScript.MoveNon_Intellect);
                break;
                
        }
    }
    public void SetBehaviour(Action _action)
    {
        Behaviour += _action;
    }
    public void RemoveBehaviour(Action _action)
    {
        Behaviour -= _action;
    }
    
    public void ClearBehaviour(Action _action)
    {
        Behaviour = null;
    }



}
