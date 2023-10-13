using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcDataContainer : ISimpleContainer
{
    public List<NpcData> npcDatas { get; set; } = new();
    public void Regist(NpcData npcData) => npcDatas.Add(npcData);
    public List<NpcData> Get() => npcDatas.ToList();
}
