using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Armor : Equipment
{
    public int GetIndex()
    {
        return itemData.itemId;
    }

    public ArmorData GetArmorData()
    {
        if (itemData is ArmorData)
            return itemData as ArmorData;
        else
            return null;
    }
}   
