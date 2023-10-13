using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringDataContainer : ISimpleContainer
{
    public StringData stringData { get; set; } = new();
    public void Regist(StringData stringData) => this.stringData = stringData;
    public StringData Get() => stringData;
}
