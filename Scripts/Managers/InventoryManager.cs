using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    private List<ItemBase> itemDB = new List<ItemBase>();
    private ItemDataListWrapper wrapper = new ItemDataListWrapper();
    public void AddItem(ItemBase item)
    {
        itemDB.Add(item);
        InventoryUI.addItem?.Invoke(item);
        wrapper.SetItem(new ItemDataDB(item.itemData.itemId, false));
        SaveSystem.SaveInventoryItems(wrapper);
    }

    public List<ItemBase> GetItems()
    {
        Clear();
        var newItemDB = SaveSystem.LoadInventoryItems();
        List<ItemBase> itemBases = new List<ItemBase>();
        foreach (var data in newItemDB.itemList)
        {
            var itemBase = ItemHelper.GetItem(data.itemId);
            if(itemBase is Equipment)
            {
                var equipment = (Equipment)itemBase;
                if (data.isUsed)
                    equipment.OnEquip();
            }
            itemBases.Add(itemBase);
        }
        itemDB = itemBases;

        return itemBases;
    }

    public void SaveDataBase()
    {
        List<ItemDataDB> savedItems = new List<ItemDataDB>();
        var equippedItems = GetEquippedItem();
        foreach (var item in equippedItems)
        {
            itemDB.Remove(item);
            savedItems.Add(new ItemDataDB(item.itemData.itemId, true));
        }
        foreach(var item in itemDB)
        {
            savedItems.Add(new ItemDataDB(item.itemData.itemId, false));
        }
        wrapper.SetItemList(savedItems);
        SaveSystem.SaveInventoryItems(wrapper);
    }

    public void Clear() => itemDB.Clear();
    public void EquipItem(ItemBase item)
    {
        var player = EntityManager.Instance.player;
        if (item is Equipment)
        {
            var equipmentItem = (Equipment)item;
            equipmentItem.OnEquip();
            player.Equip(item as Equipment);
            InventoryUI.useItem?.Invoke(item);
            if(item is Weapon)
            {
                var weaponData = item.itemData as WeaponData;
                var playerClass = weaponData.playerClass;
                var skillDatas = SkillHelper.GetSkillDataList(playerClass);
            }
        }
    }

    public void UnEquipItem(ItemBase item)
    {
        var player = EntityManager.Instance.player;
        if (item is Equipment)
        {
            var equipmentItem = (Equipment)item;
            equipmentItem.UnEquip();
            player.UnEquip(equipmentItem);
            InventoryUI.addItem?.Invoke(item);
        }
    }

    public List<Equipment> GetEquippedItem()
    {
        var equipments = itemDB.Cast<Equipment>().ToList();
        return equipments.FindAll(x => x.isEquipped == true);
    }
}
