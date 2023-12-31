using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;
public class UI_EquipSystem : MonoBehaviour
{
    [SerializeField] private GameObject equipmentUITemplate;
    [SerializeField] private Transform Container;
    public List<EquipmentIcon> EquipmentIconList = new List<EquipmentIcon>();
    public Action OnUpdatingEquipment;

    private Vector3 position;
    private float buttonSpacing;
    private void Awake()
    {
        OnUpdatingEquipment += UpdateEquipmentUI;
    }

    private void UpdateEquipmentUI()
    {
        List<Item_SO> _itemList = new List<Item_SO>(Inventory.Instance.EquipSystem.ItemHolder);
        EquipmentIconList = new List<EquipmentIcon>();
        for (int i = 0; i < _itemList.Count; i++)
        {
            GameObject _equipmentPrefab = Instantiate(equipmentUITemplate, Container);
            RectTransform _buttonRect = _equipmentPrefab.GetComponent<RectTransform>();
            EquipmentIcon _equipmentIcon = _equipmentPrefab.GetComponent<EquipmentIcon>();
            EquipmentIconList.Add(_equipmentIcon);
            Image _buttonImage = _equipmentPrefab.transform.Find("Image").GetComponent<Image>();
            _buttonImage.sprite = _itemList[i].ItemIcon;
            position.x -= _buttonRect.sizeDelta.x + buttonSpacing;
        }
    }

    public void UseEquipedItem(int _index)
    {
        var _equipmentIcon = EquipmentIconList[_index];
        if (_equipmentIcon != null)
            _equipmentIcon.UseItem();
    }

    public void SetUIItem(Item_SO _item)
    {
        EquipmentIcon _equipmentIcon = EquipmentIconList.FirstOrDefault(_ui => _ui.Item = null);
        if (_equipmentIcon != null)
            _equipmentIcon.SetItem(_item);
    }

    public void UnEquipItem(Item_SO _item)
    {
        EquipmentIcon _equipmentIcon = EquipmentIconList.FirstOrDefault(_ui => _ui.Item = _item);
        if (_equipmentIcon != null)
        {
            _equipmentIcon.ClearItem();
            EquipmentIconList.Remove(_equipmentIcon);
        }

        Inventory.Instance.EquipSystem.UnEquip(_item);
    }
}
