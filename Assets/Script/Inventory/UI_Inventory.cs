using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private GameObject InventorySlotTemplate;
    [SerializeField] private Transform ButtonContainer;

    Vector3 buttonStartPos;
    float buttonSpacing = 10f;
    public void Init()
    {
        buttonStartPos = ButtonContainer.position;
    }
    public void RefreshUI()
    {
        int x = 0;
        int y = 0;
        List<Item_SO> _itemList = new List<Item_SO>(Inventory.Instance.ItemLists);
        for (int i = 0; i < _itemList.Count; i++)
        {
            Item_SO _item = _itemList[i];
            GameObject _buttonPrefab = Instantiate(InventorySlotTemplate, ButtonContainer);
            RectTransform _buttonRect = _buttonPrefab.GetComponent<RectTransform>();

            if(x < 5)
            {
                buttonStartPos.x += _buttonRect.sizeDelta.x + buttonSpacing;
                x++;
            }
            else
            {
                buttonStartPos.y -= _buttonRect.sizeDelta.y + buttonSpacing;
                y++;
            }

            Image _buttonImage = _buttonPrefab.transform.Find("Image").GetComponent<Image>();
            _buttonImage.sprite = _item.ItemIcon;
            TextMeshProUGUI _buttonText = _buttonPrefab.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            _buttonText.text = _item.ItemName;
            TextMeshProUGUI _buttonTextAmount = _buttonPrefab.transform.Find("Amount").GetComponent<TextMeshProUGUI>();
            _buttonTextAmount.text = _item.Amount.ToString();

        }
    }
}
