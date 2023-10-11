using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

[Serializable]
public class RPG_Stats
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


    public event Action<int> StrChanged;
    public event Action<int> AgiChanged;
    public event Action<int> ConChanged;
    public event Action<int> IntChanged;
    public event Action<int> WisChanged;
    public event Action<int> ChaChanged;

    public RPG_Stats()
    {
        
       
    }


    public void Increased(string _attributeName, int _amount)
    {
        FieldInfo _field = GetType().GetField(_attributeName, BindingFlags.Public | BindingFlags.Instance);
        if (_field != null && _field.FieldType == typeof(int))
        {
            int _currentValue = (int)_field.GetValue(this);
            int _newValue = _currentValue + _amount;
            _field.SetValue(this, _newValue);
            OnChanged();
            Debug.Log("Added");
        }

    }

    // ReModel to one Onchanged
    public void OnChanged()
    {
        OnStrChanged();
        OnAgiChanged();
        OnConChanged();
        OnIntChanged();
        OnWisChanged();
        OnChaChanged();
    }

    public void OnStrChanged()
    {
        StrChanged?.Invoke(Str);
    }
    public void OnAgiChanged()
    {
        AgiChanged?.Invoke(Agi);
    }
    public void OnConChanged()
    {
        ConChanged?.Invoke(Con);
    }
    public void OnIntChanged()
    {
        IntChanged?.Invoke(Int);
    }
    public void OnWisChanged()
    {
        WisChanged?.Invoke(Wis);
    }
  
    public void OnChaChanged()
    {
        ChaChanged?.Invoke(Cha);
    }
    public bool CanDo(int _statAmount, int _statRequired)
    {
        if(_statAmount >= _statRequired)
        {
            return true;
        }
        else
        {
            int _random = UnityEngine.Random.Range(0, _statAmount);
            _random += _statAmount;
            if(_random >= _statRequired)
            {
                return true;
            }
        }
        return false;
    }
}
