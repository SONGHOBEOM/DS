using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatchDataContainer : MonoBehaviour
{
    public List<PatchData> patchDatas { get; private set; } = new();
    public void Regist() 
    {
        Object[] dataArray = Resources.LoadAll("PatchDatas");
        foreach(var data in dataArray)
        {
            var patchData = data as PatchData;
            patchDatas.Add(patchData);
        }
    }

    public List<PatchData> Get() => patchDatas;
}
