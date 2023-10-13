using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemBase : MonoBehaviour 
{
    [SerializeField]
    public ItemData itemData;

    public ItemData GetItemData() => this.itemData;
}
