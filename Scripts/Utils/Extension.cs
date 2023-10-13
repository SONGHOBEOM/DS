using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static bool IsRelativeClassOf<T>(this ScriptableObject table, Type _type) => table.GetType().IsSubclassOf(_type) || table.GetType() == _type;
}
