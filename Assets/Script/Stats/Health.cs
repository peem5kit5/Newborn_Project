using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : GameManager
{
    public int CurrentHP;
    public int MaxHP;

    public override void InitAwake()
    {

    }
    public override void Updating()
    {
       
    }

    public void IncreasedHP(int _con)
    {
        
            MaxHP += _con;
        
    }

   
}
