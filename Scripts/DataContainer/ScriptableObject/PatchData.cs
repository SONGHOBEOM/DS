using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PatchData", menuName = "ScriptableObject/PatchData")]
public class PatchData : ScriptableObject
{
    [SerializeField] private string _assetName;
    public string assetName
    {
        get { return _assetName; }
    }
}
