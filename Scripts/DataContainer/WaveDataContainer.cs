using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDataContainer : ISimpleContainer
{
    public WaveData waveData { get; set; } = new();
    public void Regist(WaveData waveData) => this.waveData = waveData;
    public WaveInfo Get(int level) => waveData.GetWaveInfo(level);
}

