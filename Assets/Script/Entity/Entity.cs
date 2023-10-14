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
        Undead,
        Beast_Lone,
        Beast_Group,
        Veggie,
        Spirit,
        MysteriousCreature,

    }
    [Space]
    [Header("Setting")]
    public List<Event_SO> EventList = new List<Event_SO>();
    public int ItemDropRate;
    public EntityBehaviour BehaviourScript;
    public Transform Target;
    public string[] Enemies;
    public GameObject[] SplitingOBJ;


    public Action<Transform> Behaviour;
    public Action AnotherBehaviour;
    Health HP;
    bool isChased;

    public GameObject WeaponPrefab;
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

        Updater();

    }
    void Updater()
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
    void Split()
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
    void SetUp()
    {
        BehaviourScript.rb = GetComponent<Rigidbody>();
        BehaviourScript.navMeshAgent = GetComponent<NavMeshAgent>();
        BehaviourScript.ThisTransform = transform;
        HP = GetComponent<Health>();
        CheckingType();
    }

    public void IncreasedMoveSpeed(float _amount)
    {
        BehaviourScript.MoveSpeed += _amount;
    }

    #region Action List
    public List<Action> NormalActionBehaviour()
    {
        List<Action> _actionList = new List<Action>()
        {
            BehaviourScript.Attacking,
            BehaviourScript.Eating,
            BehaviourScript.Sleeping
        };

        return _actionList;
    }

    public List<Action> ToughActionBehaviour()
    {
        List<Action> _actionList = new List<Action>()
        {
            BehaviourScript.Attacking,
            BehaviourScript.Sleeping

        };

        return _actionList;
    }

    public List<Action<Transform>> Intellect_GroupMoveAction()
    {
        List<Action<Transform>> _actionList = new List<Action<Transform>>()
        {
            BehaviourScript.Chase_Intellect,
            BehaviourScript.WanderAsGroup,

        
        };
       
        return _actionList;
    }
    public List<Action<Transform>> Intellect_LoneMoveAction()
    {
        List<Action<Transform>> _actionList = new List<Action<Transform>>()
        {
            BehaviourScript.Chase_Intellect,
            BehaviourScript.WanderLone,


        };

        return _actionList;
    }


    public List<Action<Transform>> NonIntellect_LoneMoveAction()
    {
        List<Action<Transform>> _actionList = new List<Action<Transform>>()
        {
            BehaviourScript.ChaseNon_Intellect,
            BehaviourScript.WanderLone,

        };

        return _actionList;
    }

    public List<Action<Transform>> NonIntellect_GroupMoveAction()
    {
        List<Action<Transform>> _actionList = new List<Action<Transform>>()
        {
            BehaviourScript.ChaseNon_Intellect,
            BehaviourScript.WanderAsGroup,

        };

        return _actionList;
    }
    #endregion
    void CheckingType()
    {
        Debug.Log("Assign");
        ClearAllBehaviour();
        List<Action> _actionList = new List<Action>();
        List<Action<Transform>> _actionTransformList = new List<Action<Transform>>();
        switch (Creature)
        {
           
            case CreatureType.Humanoid:
                _actionList = NormalActionBehaviour();
                _actionTransformList = Intellect_GroupMoveAction();
                AnotherBehaviour += Die;
                break;
            case CreatureType.Undead:
                _actionList = ToughActionBehaviour();
                _actionTransformList = NonIntellect_LoneMoveAction();
                AnotherBehaviour += Die;

                break;
            case CreatureType.Beast_Lone:
                _actionList = NormalActionBehaviour();
                _actionTransformList = NonIntellect_LoneMoveAction();
                AnotherBehaviour += Die;
                break;
            case CreatureType.Beast_Group:
                _actionList = NormalActionBehaviour();
                _actionTransformList = NonIntellect_GroupMoveAction();
                AnotherBehaviour += Die;
                break;
            case CreatureType.Spirit:
                _actionList = ToughActionBehaviour();
                _actionTransformList = NonIntellect_GroupMoveAction();
                AnotherBehaviour += Split;
                break;
          

        }
        foreach(Action _action in _actionList)
        {
            SetBehaviour(_action);
        }
        foreach(Action<Transform> _action in _actionTransformList)
        {
            SetBehaviour(_action);
        }
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
