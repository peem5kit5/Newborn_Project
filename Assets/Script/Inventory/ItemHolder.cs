using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemHolder
{
    public Item_SO[] Items;

    public Item_SO RandomItem()
    {
        int _random = Random.Range(0, Items.Length);
        return Items[_random];
    }
}
