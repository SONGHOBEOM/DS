using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public static class Calculator
{
    public static float CalculateWeaponDamage(WeaponData weaponData)
    {
        var damage = Random.Range(weaponData.minDamage, weaponData.maxDamage);

        return damage;
    }

    public static float CalculateSkillDamage(SkillData skillData)
    {
        var weapon = EntityManager.Instance.player.GetPlayerWeapon();
        var weaponData = weapon.itemData as WeaponData;
        var weaponDamage = Random.Range(weaponData.minDamage, weaponData.maxDamage);

        var totalDamage = (weaponDamage * skillData.coefficient) + skillData.skillDamage;

        return totalDamage;
    }

    public static float CalculateMonsterDamage(float damage)
    {
        var defense = EntityManager.Instance.player.defense;
        var totalDamage = damage - defense;

        return totalDamage;
    }
}
