using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UI_Inventory : GameManager
{
    public static UI_Inventory Instance { get; private set; }

    public GameObject InventorySlotTemplate;
    public Transform ButtonContainer;

    Vector3 buttonStartPos;
    float buttonSpacing = 10f;

    public override void InitAwake()
    {
        SetUp();
    }
    void SetUp()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        buttonStartPos = ButtonContainer.position;
    }
    public void RefreshUI()
    {
        List<Item_SO> _itemList = new List<Item_SO>(Inventory.Instance.itemDictionary.Values);
        for (int i = 0; i < _itemList.Count; i++)
        {
            Item_SO _item = _itemList[i];
            GameObject _buttonPrefab = Instantiate(InventorySlotTemplate, ButtonContainer);
            RectTransform _buttonRect = _buttonPrefab.GetComponent<RectTransform>();
            buttonStartPos.y -= _buttonRect.sizeDelta.y + buttonSpacing;
        }
    }
}
