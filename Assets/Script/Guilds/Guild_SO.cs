using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/GuildData")]
public class Guild_SO : ScriptableObject
{
    public enum Faction
    {
        Red,
        Green
    }
    public Faction Guild;
    public string[] Allies;
    public string[] Enemies;


}
