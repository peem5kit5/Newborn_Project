using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ItemData")]
public class Item_SO : ScriptableObject
{
    [Space]
    [Header("Item Attributes")]
    public string ItemName;
    public string ItemDescription;

    public GameObject obj;
    public itemType ItemType;

    public enum itemType
    {
        Consumable,
        Usable,
        Etc
    }

    [Space]
    [Header("Item Info")]
    public Sprite ItemIcon;
    public bool isStackable;
    public int Prices;
    public int Amount;
    public bool CanEquip;
    public Action OnUseItem;

    public ItemAbilityBase ItemAbility;
}
public class ItemAbilityBase : MonoBehaviour
{
    public Item_SO Item;
    public int Cooldown;
    public virtual void UseItem()
    {
        switch (Item.ItemType)
        {
            case Item_SO.itemType.Consumable:
                break;
            case Item_SO.itemType.Usable:
                break;
            case Item_SO.itemType.Etc:
                break;
        }
    }
}
