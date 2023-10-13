using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RecipesData")]
public class Recieps_SO : ScriptableObject
{
    
    public Item_SO GetItem;
    public Item_SO[] ItemRequires; 
}
