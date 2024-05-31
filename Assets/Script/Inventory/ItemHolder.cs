using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

[System.Serializable]
public class ItemRandomizer
{
    private Item_SO currentItem;
    public Item_SO[] Items;

    public async void SetItem()
    {
        Item_SO _item = await RandomItem();
        currentItem = _item;
    }

    public async Task<Item_SO> RandomItem()
    {
        await Task.Delay(100);
        int _random = Random.Range(0, Items.Length);
        return Items[_random];
    }
}
