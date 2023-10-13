using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : ISimpleContainer
{
    public List<ItemBase> items = new List<ItemBase>();
    public void Regist(ItemBase item) => items.Add(item);
    public List<ItemBase> Get() => items;
}
