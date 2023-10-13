using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public DataContainer entireDataContainer { get; private set; }

    private void Start()
    {
        entireDataContainer = new DataContainer();
        entireDataContainer.patchDataContainer.Regist();
        PatchManager.Instance.Load();
    }

    public void CreateDatas()
    {
        ItemHelper.CreateEquipmentDatas();
        QuestHelper.CreateQuests();
        SkillHelper.CreateSkillDatas();
        SoundHelper.CreateSoundData();
        ObjectHelper.CreateObejctData();
    }
}
