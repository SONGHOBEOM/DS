using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

public static class ItemHelper
{ 
    private static Dictionary<int, ItemData> itemDatas = new Dictionary<int, ItemData>();
    private static Dictionary<int, ItemBase> itemBases = new Dictionary<int, ItemBase>();
    public static void CreateEquipmentDatas()
    {
        var equipmentDatas = DataManager.Instance.entireDataContainer.itemDataContainer.Get();
        foreach (var equipmentData in equipmentDatas)
            itemDatas.Add(equipmentData.itemId, equipmentData);
    }

    public static void CreateEquipment()
    {
        var items = DataManager.Instance.entireDataContainer.itemContainer.Get();
        foreach (var item in items)
            itemBases.Add(item.itemData.itemId, item);
    }

    public static ItemBase GetItem(int index)
    {
        if (itemBases.TryGetValue(index, out var item))
        { 
            switch (item)
            {
                case Weapon:
                    return item as Weapon;
                case Armor:
                    return item as Armor;
            }
        }
        return default;
    }
}
