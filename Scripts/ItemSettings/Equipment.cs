using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Equipment : ItemBase
{
    public bool isEquipped = false;
    public bool IsEquipped() => isEquipped;
    public void OnEquip() => isEquipped = true;
    public void UnEquip() => isEquipped = false;
}
