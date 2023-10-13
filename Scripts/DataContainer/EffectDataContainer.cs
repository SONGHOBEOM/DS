using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectDataContainter : ISimpleContainer
{
    public List<EffectData> effectDatas { get; set; } = new();
    public void Regist(EffectData effectData) => effectDatas.Add(effectData);
    public List<EffectData> Get() => effectDatas.ToList();
}
