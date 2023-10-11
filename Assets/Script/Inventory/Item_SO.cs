using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ItemData")]
public class Item_SO : ScriptableObject
{
    [Space]
    [Header("Item Attributes")]
    public string ItemName;
    [TextArea]
    public string ItemDescription;

    public GameObject obj;
    public itemType ItemType;
    public enum itemType
    {
        Consumable,
        Equipment,
        Misc
    }
    [Space]
    [Header("Item Info")]
    public Sprite ItemIcon;
    public bool isStackable;
    public int Prices;
    public int Values = 1;
    public int Amount;

}
