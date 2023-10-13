using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StringData", menuName = "ScriptableObject/StringData")]
public class StringData : ScriptableObject
{
    [SerializeField]
    private SerializableDictionary<string, string> serializableDictionary = new SerializableDictionary<string, string>();

    public string Get(string key)
    {
        if (serializableDictionary.TryGetValue(key, out var value)) 
        {
            return value;
        }

        return "";
    }
}
