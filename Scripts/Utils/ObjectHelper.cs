using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectHelper
{
    private static Dictionary<Define.ObjectType, CharacterBasicData> basicDatas = new Dictionary<Define.ObjectType, CharacterBasicData>();

    public static void CreateObejctData()
    {
        var objectDatas = DataManager.Instance.entireDataContainer.characterBasicDataContainer.Get();
        foreach(var objectData in objectDatas)
        {
            basicDatas.Add(objectData.objectType, objectData);
        }
    }

    public static CharacterBasicData GetData(Define.ObjectType type)
    {
        if(basicDatas.TryGetValue(type, out CharacterBasicData basicData))
            return basicData;

        return default;
    }
}
