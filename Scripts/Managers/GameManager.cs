using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    private List<Spawner> spawners = new List<Spawner>(3);
    private int waveLevel = 1;
    private int instantiateCount = 0;
    private int initGold = 10000;

    private void Start()
    {
        InventoryManager.Instance.Clear();
        InitPlayerGold();
    }

    private void InitPlayerGold()
    {
        EntityManager.Instance.currencyData.SetPlayerGold(initGold);
    }

    public void AddSpawner(Spawner spawner) => spawners.Add(spawner);
    public void StartWave()
    {
        Monster.dieFlag = false;
        var waveInfo = DataManager.Instance.entireDataContainer.waveDataContainer.Get(waveLevel);
        WaveTimer.onStart?.Invoke(waveInfo);

        if (waveInfo.waveType != Define.WaveType.Boss)
        {
            foreach (var spawner in spawners)
                spawner.Spawn(waveInfo);
        }
        else
        {
            var bossSpawner = spawners[0];
            bossSpawner.Spawn(waveInfo);
        }
    }

    public void StopWave()
    {
        InitCount();
        Monster.dieFlag = true;
        Spawner.stopFlag = true;

        void InitCount() => instantiateCount = 0;
    }

    public void GameSave()
    {
        EntityManager.Instance.currencyData.SavePlayerGold();
        EntityManager.Instance.SavePlayerInfo();
        InventoryManager.Instance.SaveDataBase();
    }

    public void LoadGame()
    {
        EntityManager.Instance.currencyData.LoadPlayerGold();
        EntityManager.Instance.LoadPlayerInfo();
    }

    public int GetInstantiateCount() => instantiateCount;
    public void AddInstantiateCount() => instantiateCount++;
    public void AddWaveLevel() => waveLevel++;
    public int GetWaveLevel() => waveLevel;
}
