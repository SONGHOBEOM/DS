using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
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
    public int GetWaveLevel() => waveLevel;
}
