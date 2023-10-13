using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Text classText;
    [SerializeField] private Text minimumDamageText;
    [SerializeField] private Text maximumDamageText;
    [SerializeField] private Text maxHpText;
    [SerializeField] private Text defenseText;

    public static Action updatePlayerStat = null;

    private void OnEnable()
    {
        updatePlayerStat -= SetDatas;
        updatePlayerStat += SetDatas;
        SetDatas();
    }

    private void SetDatas()
    {
        levelText.text = string.Format(STR.Get("PlayerLevel"), EntityManager.Instance.player.level);

        if (EntityManager.Instance.player.GetPlayerWeapon() == default)
        {
            classText.text = string.Format(STR.Get("PlayerClass"), Define.PlayerClass.Adventurer);
            minimumDamageText.text = string.Format(STR.Get("MinimumDamage"), 0);
            maximumDamageText.text = string.Format(STR.Get("MaximumDamage"), 0);
        }
        else
        {
            classText.text = string.Format(STR.Get("PlayerClass"), EntityManager.Instance.player.GetPlayerWeapon().GetWeaponData().playerClass);
            minimumDamageText.text = string.Format(STR.Get("MinimumDamage"), EntityManager.Instance.player.GetPlayerWeapon().GetWeaponData().minDamage);
            maximumDamageText.text = string.Format(STR.Get("MaximumDamage"), EntityManager.Instance.player.GetPlayerWeapon().GetWeaponData().maxDamage);
        }
        maxHpText.text = string.Format(STR.Get("PlayerMaxHp"), EntityManager.Instance.player.maxHp);
        defenseText.text = string.Format(STR.Get("PlayerDefense"), EntityManager.Instance.player.defense);
    }

}
