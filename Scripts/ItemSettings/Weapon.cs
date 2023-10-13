using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Weapon : Equipment
{
    [SerializeField] private WeaponAttackChecker weaponAttackChecker;

    public WeaponData GetWeaponData()
    {
        if (itemData is WeaponData) 
            return itemData as WeaponData;
        return default;
    }

    public void ActivateChecker() => weaponAttackChecker.gameObject.SetActive(true);
    public void UnActivateChecker() => weaponAttackChecker.gameObject.SetActive(false);
}
