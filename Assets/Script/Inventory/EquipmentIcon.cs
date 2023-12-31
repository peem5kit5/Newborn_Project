using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentIcon : MonoBehaviour
{
    public Item_SO Item;
    public int Cooldown;
    public Image Icon;
    public Slider CooldownTracker;
    public void SetItem(Item_SO _item)
    {
        Item = _item;
        Cooldown = _item.ItemAbility.Cooldown;

        if(Item.ItemAbility.Item == null)
            _item.ItemAbility.Item = _item;

        Icon.sprite = _item.ItemIcon;
        
        CooldownTracker.value = 0;
        CooldownTracker.maxValue = Cooldown;
    }

    public void UseItem()
    {
        if(Item != null && CooldownTracker.value >= Cooldown)
        {
            Item.ItemAbility.UseItem();
            CooldownTracker.value = 0;
        }
    }

    public void ClearItem()
    {
        Item = null;
        Cooldown = 0;
        Icon = null;
    }

    private void Update()
    {
        UpdateCooldownTiming();
    }

    private void UpdateCooldownTiming()
    {
        if(CooldownTracker.value < Cooldown)
        CooldownTracker.value += Time.deltaTime * 1;
    }
}
