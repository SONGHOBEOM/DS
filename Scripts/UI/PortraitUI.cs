using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitUI : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider expGauge;
    [SerializeField] private Text levelText;
    [SerializeField] private Text HpText;

    private float curHp;
    private float maxHp;
    private float exp;
    private ExpValueInfo requiredExpValue;

    public static Action updateValues = null;
    public static Action updateExpValue = null;

    private void Start()
    {
        updateValues = UpdateValues;
        updateExpValue = UpdateExpValue;
    }

    private void UpdateValues()
    {
        this.curHp = EntityManager.Instance.player.curHp;
        this.maxHp = EntityManager.Instance.player.maxHp;

        hpBar.value = this.curHp / this.maxHp;
        UpdateText();
    }
    private void UpdateText() 
    { 
        HpText.text = $"{this.curHp} / {this.maxHp}";
    }

    private void UpdateExpValue()
    {
        this.exp = EntityManager.Instance.player.curExp;

        var level = EntityManager.Instance.player.level;
        this.requiredExpValue = DataManager.Instance.entireDataContainer.expValueDataContainer.Get(level);

        if(exp > requiredExpValue.maxExpValue)
        {
            this.exp -= requiredExpValue.maxExpValue;
            EntityManager.Instance.player.LevelUp();
            level = EntityManager.Instance.player.level;
            this.requiredExpValue = DataManager.Instance.entireDataContainer.expValueDataContainer.Get(level);
        }

        expGauge.value = this.exp / this.requiredExpValue.maxExpValue;
        levelText.text = $"{EntityManager.Instance.player.level}";
    }

}
