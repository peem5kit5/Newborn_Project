using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/TraitsData")]
public class Traits_SO : ScriptableObject
{
    public string TraitName;
    public bool IsUnlocked;
    public int Value;
    public Condition UnlockCondition;
}
public class BaseTraits
{
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
public abstract class Condition
{
    public bool Unlocked;
    public abstract bool CheckCondition();
}
