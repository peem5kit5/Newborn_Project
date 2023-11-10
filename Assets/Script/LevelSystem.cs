
using System;


[Serializable]
public class LevelSystem
{
    public int CurrentEXP;
    public int MaxEXP;

    public int Level;


    public void IncreasedEXP(int _amount)
    {
        CurrentEXP += _amount;
        if(CurrentEXP >= MaxEXP)
        {
            int _addition = CurrentEXP % MaxEXP;
            Level++;
            CurrentEXP = 0;
            CurrentEXP += _addition;
        }
    }


    
 
}
