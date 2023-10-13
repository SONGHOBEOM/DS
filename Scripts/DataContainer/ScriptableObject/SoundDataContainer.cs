using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundDataContainer : ISimpleContainer
{
    public List<SoundData> soundDatas { get; set; } = new();
    public void Regist(SoundData soundData) => soundDatas.Add(soundData);
    public List<SoundData> Get() => soundDatas.ToList();
}
