using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Inventory : MonoBehaviour
{   
    public static Inventory Instance { get; private set; }

    public Dictionary<string, Item_SO> itemDictionary = new Dictionary<string, Item_SO>();
    public event Action<Item_SO> OnItemListsChanged;

 
    public void Init()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        OnItemListsChanged += ChangedList;
    }

    private void ChangedList(Item_SO _item)
    {
        UI_Inventory.Instance.RefreshUI();
        OnItemListsChanged?.Invoke(_item);
    }


    public void AddItem(Item_SO _item)
    {
        if (_item.isStackable)
        {
            if (itemDictionary.ContainsKey(_item.ItemName))
            {
                itemDictionary[_item.ItemName].Amount += _item.Amount;
                
            }
            else
            {
                itemDictionary.Add(_item.ItemName,_item);

                
            }
        }
        else
        {
            itemDictionary.Add(_item.ItemName, _item);

        }
        ChangedList(_item);
    }
    public void RemoveItem(Item_SO _item)
    {
        if (itemDictionary.ContainsKey(_item.ItemName))
        {
            if (_item.Amount > 1)
            {
                _item.Amount--;
            }
            else
            {
                itemDictionary.Remove(_item.ItemName);

            }
            ChangedList(_item);
        }
    }
}
