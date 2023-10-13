using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestDataContainer : ISimpleContainer
{
    public List<QuestData> questDatas { get; set; } = new();
    public void Regist(QuestData questData) => questDatas.Add(questData);
    public List<QuestData> Get() => questDatas.ToList();
}
