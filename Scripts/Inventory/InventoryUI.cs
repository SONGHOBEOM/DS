using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

[ResourceInfo("InventoryUI", 511f)]
public class InventoryUI : Popup
{
    [SerializeField] private Transform content;
    [SerializeField] private List<ItemBase> myItems = new List<ItemBase>();
    [SerializeField] private Text playergold;
    [SerializeField] private Transform armorSlot;
    [SerializeField] private Transform weaponSlot;
    [SerializeField] private GameObject inventorySlot;
    [SerializeField] private GameObject equipmentSlot;

    public static Action<ItemBase> addItem = null;
    public static Action<ItemBase> useItem = null;
    public static Action refreshList = null;
    public override void Init()
    {
        base.Init();
        AddUIEvent();
        SetActionFunc();
    }
    void AddUIEvent()
    {
        GameObject go = GetComponent<InventoryUI>().gameObject;
        AddUIEvent(go, DraggingUI, Define.UIEvent.Drag);
        AddUIEvent(go, SetIsDraggingUI, Define.UIEvent.ClickDown);
        AddUIEvent(go, SetIsNotDraggingUI, Define.UIEvent.ClickUp);

        void DraggingUI(PointerEventData data) => transform.position += (Vector3)data.delta;
        void SetIsDraggingUI(PointerEventData data)
        {
            UIManager.Instance.SetIsDraggingUI(data.button == PointerEventData.InputButton.Left);
            go.transform.SetAsLastSibling();
        }
        void SetIsNotDraggingUI(PointerEventData data) => UIManager.Instance.SetIsDraggingUI(false);
    }

    private void SetActionFunc()
    {
        addItem = AddItemsToSlot;
        useItem = UseItem;
        refreshList = RefreshInventory;
    }


    public override void Open(UIParameter param)
    {
        base.Open(param);

        RefreshGoldText();
    }

    public override void Close()
    {
        base.Close();
        SoundManager.Instance.PlayClip("UIClick");
    }

    public void AddItemsToSlot(ItemBase itemBase)
    {
        if (itemBase == null) return;
        var slot = ResourceManager.Instance.Instantiate<InventorySlot>(inventorySlot, content);
        slot.SetData(itemBase);
    }

    private void RefreshInventory()
    {
        myItems = InventoryManager.Instance.GetItems();
        foreach(var item in myItems)
        {
            var itemType = item.itemData.itemType;
            if(itemType == Define.ItemType.Weapon || itemType == Define.ItemType.Armor)
            {
                var equipment = item as Equipment;
                if (equipment.IsEquipped())
                    InventoryManager.Instance.EquipItem(item);
                else
                    AddItemsToSlot(item);
            }
        }
    }

    public void RefreshGoldText()
    {
        var gold = EntityManager.Instance.currencyData.GetPlayerGold();
        playergold.text = string.Format(STR.Get("OwnGold"), gold);
    }


    public void UseItem(ItemBase item) => UseEquipment(item);
    public void UseEquipment(ItemBase itemBase)
    {
        var itemType = itemBase.itemData.itemType;
        if (itemType == Define.ItemType.Weapon)
        {
            var slot = ResourceManager.Instance.Instantiate<EquipmentSlot>(equipmentSlot, weaponSlot);
            var rectTransform = slot.GetComponent<RectTransform>();
            rectTransform.localPosition = Vector3.zero;
            slot.SetData(itemBase);
        }
        else if (itemType == Define.ItemType.Armor)
        {
            var slot = ResourceManager.Instance.Instantiate<EquipmentSlot>(equipmentSlot, armorSlot);
            var rectTransform = slot.GetComponent<RectTransform>();
            rectTransform.localPosition = Vector3.zero;
            slot.SetData(itemBase);
        }
    }
}
