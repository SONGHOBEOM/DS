using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CurrencyData
{
    [SerializeField] private int playerGold;
    private int loadedPlayerGold;

    public CurrencyData(int playerGold = 0) => this.playerGold = playerGold;

    public void SavePlayerGold() => SaveSystem.SaveCurrencies(this);
    public void LoadPlayerGold()
    {
        var currencyData = SaveSystem.LoadCurrencies();
        loadedPlayerGold = currencyData.playerGold;
        playerGold = loadedPlayerGold;
    }
    public int GetPlayerGold() => playerGold;

    
    public void SetPlayerGold(int gold) => playerGold = gold;

    public void AddGold(int gold)
    {
        playerGold += gold;
        SaveSystem.SaveCurrencies(this);
    }
}
