using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternDataContainer : ISimpleContainer
{
    public PatternData patternData { get; set; } = new();
    public void Regist(PatternData patternData) => this.patternData = patternData;
    public PatternInfo Get(string name) => patternData.GetPatternInfo(name);
}
