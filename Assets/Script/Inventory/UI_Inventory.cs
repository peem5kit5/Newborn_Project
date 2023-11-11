using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class UI_Inventory : MonoBehaviour
{
    public static UI_Inventory Instance { get; private set; }

    public GameObject InventorySlotTemplate;
    public Transform ButtonContainer;

    Vector3 buttonStartPos;
    float buttonSpacing = 10f;

    public void Awake()
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
        List<Item_SO> _itemList = new List<Item_SO>(Inventory.Instance.ItemLists);
        for (int i = 0; i < _itemList.Count; i++)
        {
            Item_SO _item = _itemList[i];
            GameObject _buttonPrefab = Instantiate(InventorySlotTemplate, ButtonContainer);
            RectTransform _buttonRect = _buttonPrefab.GetComponent<RectTransform>();
            buttonStartPos.y -= _buttonRect.sizeDelta.y + buttonSpacing;
            Image _buttonImage = _buttonPrefab.transform.Find("Image").GetComponent<Image>();
            _buttonImage.sprite = _item.ItemIcon;
            TextMeshProUGUI _buttonText = _buttonPrefab.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            _buttonText.text = _item.ItemName;
            TextMeshProUGUI _buttonTextAmount = _buttonPrefab.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            _buttonTextAmount.text = _item.Amount.ToString();

        }
    }
}
