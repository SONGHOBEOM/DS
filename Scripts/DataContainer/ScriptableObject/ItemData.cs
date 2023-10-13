using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private int _itemId;
    public int itemId { get { return _itemId; } }
    [SerializeField] private string _itemName;
    public string itemName { get { return _itemName; } }

    [SerializeField] private Define.ItemType _itemType;
    public Define.ItemType itemType { get { return _itemType; } }

    [SerializeField] private Sprite _itemImage;
    public Sprite itemImage { get { return _itemImage; } }

    [SerializeField] private int _itemPrice;
    public int itemPrice { get { return _itemPrice; } }
}

[Serializable]
public class ItemDataDB
{
    public int itemId;
    public bool isUsed;

    public ItemDataDB()
    {

    }
    public ItemDataDB(int ItemID, bool isUsed)
    {
        itemId = ItemID;
        this.isUsed = isUsed;
    }
}
