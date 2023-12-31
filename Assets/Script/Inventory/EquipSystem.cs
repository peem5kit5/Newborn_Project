using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class EquipSystem : MonoBehaviour
{
    public UI_EquipSystem EquipmentUI;
    public List<Item_SO> ItemHolder = new List<Item_SO>();
    public int ItemHoldingAmount = 4;

    public void Equip(Item_SO _item)
    {
        if (ItemHolder.Count < ItemHoldingAmount)
        {
            ItemHolder.Add(_item);
            EquipmentUI.SetUIItem(_item);
        }

        EquipmentUI.OnUpdatingEquipment.Invoke();
    }

    public void UnEquip(Item_SO _item)
    {
        if (ItemHolder.Contains(_item))
            ItemHolder.Remove(_item);

        EquipmentUI.OnUpdatingEquipment.Invoke();
    }
}
