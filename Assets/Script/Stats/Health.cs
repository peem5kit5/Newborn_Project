using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : GameManager
{
    public int CurrentHP;
    public int MaxHP;

    bool death;
    public override void InitAwake()
    {

    }
    public override void Updating()
    {
       
    }
    public void Heal(int _amount)
    {

        int _hpToMax = CurrentHP += _amount;
        if(CurrentHP > 0)
        {
            CurrentHP += _amount;
            if(_hpToMax > MaxHP)
            {
                CurrentHP = MaxHP;
            }
        }
      
    }
    public void TakeDamage(int _amount)
    {

        CurrentHP -= _amount;
        if(CurrentHP <= 0)
        {
            death = true;
        }

    }
    public void IncreasedMaxHP(int _con)
    {
        
            MaxHP += _con;
        
    }

   
}
