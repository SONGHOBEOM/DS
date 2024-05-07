using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIPrefabInfo : Attribute
{
    public string resourceName;
    public float posX;
    public float posY;
    public UIPrefabInfo(string resourceName, float posX = 0, float posY = 0)
    {
        this.resourceName = resourceName;
        this.posX = posX;
        this.posY = posY;
    }
}