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
    public CreatureLivingTime CreatureLiving;
    
    public enum CreatureType
    {
        Humanoid,
        Undead,
        Beast_Lone,
        Beast_Group,
        Veggie,
        Spirit,
        MysteriousCreature,

    }
    public enum CreatureLivingTime
    {
        Day,
        Night
    }
    [Space]
    [Header("Setting")]
    public ItemHolder ItemHolders;
    public List<Event_SO> EventList = new List<Event_SO>();
    public int ItemDropRate;
    public EntityBehaviour BehaviourScript;
    public Transform Target;
    public string[] Enemies;
    public GameObject[] SplitingOBJ;
    public Action<Transform> Behaviour;
    public Action AnotherBehaviour;
    public bool isChased;
    public GameObject WeaponPrefab;

    Health HP;


    public void Init()
    {
        SetUp();
        VirtualInit();
    }
    void SetUp()
    {
        BehaviourScript.rb = GetComponent<Rigidbody>();
        BehaviourScript.navMeshAgent = GetComponent<NavMeshAgent>();
        BehaviourScript.ThisTransform = transform;
        HP = GetComponent<Health>();
    }
    public virtual void VirtualInit()
    {

    }
    public virtual void Update()
    {

        Updater();

    }
    public void Updater()
    {
            if (EntityManager.Instance.Paused() == false)
            {
                Behaviour(Target);
                AnotherBehaviour();
            }
            else
            {
                Debug.Log("Paused");
            }     
    }

    void Die()
    {
        if (HP.CurrentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Split()
    {
        if (HP.CurrentHP <= 0)
        {
            int i = 0;
            foreach(GameObject _obj in SplitingOBJ)
            {
                i++;
                float _angle = i *(360f / SplitingOBJ.Length);
                Vector3 _splitPos = transform.position + Quaternion.Euler(0, _angle, 0) * (Vector3.forward * 2.5f); ;
                _splitPos.y = 0.1f;
                Instantiate(_obj, _splitPos, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    public void IncreasedMoveSpeed(float _amount)
    {
        BehaviourScript.MoveSpeed += _amount;
    }
    public void SetBehaviour(Action _action)
    {
        AnotherBehaviour += _action;
    }
    public void RemoveBehaviour(Action _action)
    {
        AnotherBehaviour -= _action;
    }

    public void SetBehaviour(Action<Transform> _action)
    {
        Behaviour += _action;
    }
    public void RemoveBehaviour(Action<Transform> _action)
    {
        Behaviour -= _action;
    }
    public void ClearAllBehaviour()
    {
        AnotherBehaviour = null;
        Behaviour = null;
    }
    
   



}
