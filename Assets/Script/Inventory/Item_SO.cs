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
        Equipment,
        Misc
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
                Inventory.Instance.RemoveItem(Item);
                break;
            case Item_SO.itemType.Equipment:
                Inventory.Instance.EquipSystem.Equip(Item);
                break;
            case Item_SO.itemType.Misc:
                break;
        }
    }
}
