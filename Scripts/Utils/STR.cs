using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class STR
{
    public static string Get(string key)
    {
        var stringData = DataManager.Instance.entireDataContainer.stringDataContainer.Get();
        var str = stringData.Get(key);
        return str;
    }
}
