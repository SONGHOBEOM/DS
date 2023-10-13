using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceInfo : Attribute
{
    public string resourceName;
    public float posX;
    public float posY;
    public ResourceInfo(string resourceName, float posX = 0, float posY = 0)
    {
        this.resourceName = resourceName;
        this.posX = posX;
        this.posY = posY;
    }
}