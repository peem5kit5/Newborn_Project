using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Inventory : Singleton<Inventory>
{
    public EquipSystem EquipSystem;
    public List<Item_SO> ItemLists = new List<Item_SO>();
    public event Action<Item_SO> OnItemListsChanged;

    public UI_Inventory UIInventory;

    public override void Awake()
    {
        base.Awake();
        OnItemListsChanged += ChangedList;
        //UIInventory.Init();
    }

    private void ChangedList(Item_SO _item)
    {
        UIInventory.RefreshUI();
    }

    public void AddItem(Item_SO _item)
    {
        if (_item.isStackable)
        {
            if (ItemLists.Contains(_item))
            {
                if (ItemLists.Count > 0)
                    foreach (Item_SO _singleItem in ItemLists)
                        if (_singleItem == _item)
                            _singleItem.Amount += 1;
            }
            else
                ItemLists.Add(_item);
        }
        else
            ItemLists.Add(_item);

        OnItemListsChanged.Invoke(_item);
    }
    public void RemoveItem(Item_SO _item)
    {
        if (_item.isStackable)
        {
            if (ItemLists.Contains(_item))
            {
                if (ItemLists.Count > 0)
                    foreach (Item_SO _singleItem in ItemLists)
                        if (_singleItem == _item)
                            _singleItem.Amount -= 1;
                else
                    ItemLists.Remove(_item);

            }
        }
        else
        {
            ItemLists.Remove(_item);
        }

        OnItemListsChanged.Invoke(_item);
        
    }
}
