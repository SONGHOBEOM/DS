using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterDataContainer : ISimpleContainer
{
    public List<MonsterData> monsterDatas { get; private set; } = new();
    public void Regist(MonsterData monsterData) => monsterDatas.Add(monsterData);
    public List<MonsterData> Get() => monsterDatas.ToList();
}
