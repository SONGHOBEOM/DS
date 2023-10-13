using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        PlayerData playerData = new PlayerData(player);

        string jsonData = JsonUtility.ToJson(playerData, true);

        string path = Path.Combine(Application.persistentDataPath + "/playerData.json");
        File.WriteAllText(path, jsonData);
    }

    public static void SaveInventoryItems(ItemDataListWrapper itemDB)
    { 
        string jsonData = JsonUtility.ToJson(itemDB, true);

        string path = Path.Combine(Application.persistentDataPath + "/itemDB.json");
        File.WriteAllText(path, jsonData);
    }

    public static void SaveCurrencies(CurrencyData currency)
    {
        var currencyData = new CurrencyData(currency.GetPlayerGold());

        string jsonData = JsonUtility.ToJson(currencyData, true);

        string path = Path.Combine(Application.persistentDataPath + "/currencyData.json");
        File.WriteAllText(path, jsonData);
    }

    public static PlayerData LoadPlayer()
    {
        string path = Path.Combine(Application.persistentDataPath + "/playerData.json");
        if(File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            var data = JsonUtility.FromJson<PlayerData>(jsonData);
            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }
    public static ItemDataListWrapper LoadInventoryItems()
    {
        string path = Path.Combine(Application.persistentDataPath + "/itemDB.json");
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            var data = JsonUtility.FromJson<ItemDataListWrapper>(jsonData);
            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

    public static CurrencyData LoadCurrencies()
    {
        string path = Path.Combine(Application.persistentDataPath + "/currencyData.json");
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            var data = JsonUtility.FromJson<CurrencyData>(jsonData);
            return data;
        }
        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }

}
