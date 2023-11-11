using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
public class EntityParty : Entity
{
    public Sprite Icon;
    public Action AddEntityParty;

    public override void VirtualInit()
    {
       
        bool _hasWander = HasMethod(Behaviour, "WanderLone");
        bool _hasWanderGroup = HasMethod(Behaviour, "WanderAsGroup");
        if (_hasWander)
        {
            Behaviour -= BehaviourScript.WanderLone;
        }
        if (_hasWanderGroup)
        {
            Behaviour -= BehaviourScript.WanderAsGroup;
        }
        AnotherBehaviour += FollowPlayer;
        AnotherBehaviour += AddEntityParty;
    }
   
    public void FollowPlayer()
    {
        if (!isChased)
        {
            return;
        }
        else
        {
            
            Behaviour(GameManager.Instance.Player.transform);

        }
    }

    static bool HasMethod(object _obj, string _methodName)
    {
        Type _type = _obj.GetType();
        MethodInfo _methodInfo = _type.GetMethod(_methodName);

        return _methodInfo != null;
    }
}
