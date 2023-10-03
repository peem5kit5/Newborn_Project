using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : EntityManager
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
    public float MoveSpeed;
    public int BaseDamage;
    public int ItemDropRate;

    public enum AttributeType
    {
        STR,
        AGI,
        INT
    }

    public Transform PlayerTransform;

    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    private RPG_Stats stat;

    public override void InitAwake()
    {
        SetUp();
    }
    public override void InitStart()
    {
        
    }
    public override void Updating()
    {

    }

    void SetUp()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        stat = GetComponent<RPG_Stats>();
    }

    public void IncreasedMoveSpeed(float _amount)
    {
        MoveSpeed += _amount;
    }



}
