using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ThemeData")]
public class ThemeSet_SO : ScriptableObject
{
    public string ThemeKey;
    public int Difficulty;
    public bool Visited;
}
