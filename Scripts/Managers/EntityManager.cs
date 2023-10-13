using PlayerCORE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
    public Player player { get; set; }

    public void SetPlayer(Player player) => this.player = player;

    public void SavePlayerInfo() => player.SavePlayer();
    public void LoadPlayerInfo() => player.LoadPlayer();

    public Camera mainCamera { get; set; }

    public CurrencyData currencyData { get; set; } = new();

}