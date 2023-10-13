using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    protected override void OnEnable()
    {
        this.monsterType = Define.ObjectType.Skeleton;
        if(monsterData == null)
            this.monsterData = ObjectHelper.GetData(this.monsterType) as MonsterData;
        InitInfo();
        base.OnEnable();
    }
}
