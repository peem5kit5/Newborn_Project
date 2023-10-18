using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

[Serializable]
public class EquipSystem
{
    public Item_SO Helmet;
    public Item_SO Armour;
    public Item_SO Legging;
    public Item_SO MainHand;
    public Item_SO SecondHand;

    public void Equip(string _whichEquip, Item_SO _item)
    {
        if (_item.CanEquip)
        {
            FieldInfo _field = GetType().GetField(_whichEquip, BindingFlags.Public | BindingFlags.Instance);
            Item_SO _currentEquipment = (Item_SO)_field.GetValue(this);
            if (_currentEquipment == null || _item.CanEquip)
            {
                if (_currentEquipment != null)
                {
                    
                }
                _field.SetValue(this, _item);
                SkinChanger.Instance.Equip(_whichEquip, _item.ItemName);
            }
                
                
            SkinChanger.Instance.Equip(_whichEquip, _item.ItemName);
        }
        else
        {
            Debug.Log("Can't Equip");
            return;
        }
        

    }

    void CheckingEquipType(Item_SO _item)
    {
        switch (_item.Attribute)
        {
            case Item_SO.ItemAttribute.Offensive:
                break;
        }
    }
}
