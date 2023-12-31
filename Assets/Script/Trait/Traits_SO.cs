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
public abstract class BaseTraits
{
    public abstract Vector3 IsManipulatedPosition(Vector3 _pos);
    public abstract bool IsUsingTrueFalse(bool _conditionMet);
    public abstract int IsManipulatedValue(int _manipulatedValue);
    public abstract Event_SO IsManipulatedEvent(Event_SO _eventSO);
}
public abstract class Condition
{
    public bool Unlocked;
    public abstract bool CheckCondition();
}
