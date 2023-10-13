using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpValueDataContainer : ISimpleContainer
{
    public ExpValueData expValueData { get; set; } = new();
    public void Regist(ExpValueData expValueData) => this.expValueData = expValueData;
    public ExpValueInfo Get(int level) => expValueData.GetMaxExpInfo(level);
}
