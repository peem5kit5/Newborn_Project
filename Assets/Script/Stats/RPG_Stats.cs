using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class RPG_Stats : GameManager
{
    [Header("Attribute")]
    public int Str;
    public int Agi;
    public int Con;
    public int Int;
    public int Wis;
    public int Cha;

    [Header("Mind Status")]
    public int Sanity;
    public int Max_Sanity;


    Entity entity;
    Health hp;

    public override void InitAwake()
    {
        hp = GetComponent<Health>();
        entity = GetComponent<Entity>();
    }

    public void Increased(string _attributeName,int _amount)
    {
        FieldInfo _field = GetType().GetField(_attributeName,BindingFlags.Public | BindingFlags.Instance);
        if(_field != null && _field.FieldType == typeof(int))
        {
            int _currentValue = (int)_field.GetValue(this);
            int _newValue = _currentValue + _amount;
            _field.SetValue(this, _newValue);
            CheckingIncreased(_field.Name);
            Debug.Log("Added");
        }

    }

    void CheckingIncreased(string _statName)
    {
        switch (_statName)
        {
            case "Str":
                break;
            case "Agi":
                entity.IncreasedMoveSpeed(Agi *0.5f);
                break;
            case "Con":
                hp.IncreasedMaxHP(2 * Con);
                break;
            case "Int":
                break;
            case "Wis":
                break;
            case "Cha":
                break;
        }
    }

    public override void Updating()
    {
        
    }

    

    public bool CanDo(int _statAmount, int _statRequired)
    {
        if(_statAmount >= _statRequired)
        {
            return true;
        }
        else
        {
            int _random = Random.Range(0, _statAmount);
            _random += _statAmount;
            if(_random >= _statRequired)
            {
                return true;
            }
        }
        return false;
    }
}
