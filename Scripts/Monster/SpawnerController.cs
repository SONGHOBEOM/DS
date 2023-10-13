using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : Subject
{
    private int waveLevel = 1;
    private WaveInfo waveInfo;
    private bool _spawnState = false;
    
    public static Action isStarted = null;
    public static Action isStopped = null;

    private void Start()
    {
        isStarted = StartWave;
        isStopped = StopWave;
    }

    public WaveInfo GetWaveInfo() => this.waveInfo;
    public bool GetSpawnState() => this._spawnState;

    public void StartWave()
    {
        Monster.dieFlag = false;
        _spawnState = true;
        var waveInfo = DataManager.Instance.entireDataContainer.waveDataContainer.Get(waveLevel);
        WaveTimer.onStart?.Invoke(waveInfo);

        NotifyObservers();
    }

    public void StopWave()
    {
        Monster.dieFlag = true;
        _spawnState = false;
        this.waveLevel++;

        NotifyObservers();
    }

}
