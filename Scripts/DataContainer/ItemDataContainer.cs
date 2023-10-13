using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDataContainer : ISimpleContainer
{
    public List<ItemData> itemDatas{ get; set; } = new();

    public void Regist(ItemData itemData) => itemDatas.Add(itemData);
    public List<ItemData> Get() => itemDatas.ToList();
}
