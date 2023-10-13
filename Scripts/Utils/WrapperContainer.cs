using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemDataListWrapper
{
    public List<ItemDataDB> itemList = new List<ItemDataDB>();
    public void SetItem(ItemDataDB item) => this.itemList.Add(item);

    public void SetItemList(List<ItemDataDB> itemList) => this.itemList = itemList;
    
    public List<ItemDataDB> GetItemList() => itemList;
}
