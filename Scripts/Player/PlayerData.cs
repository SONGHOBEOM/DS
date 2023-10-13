using PlayerCORE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int questLevel;
    public float maxHp;
    public float curHp;
    public float curExp;
    public float[] position = new float[3];
    public float[] rotation = new float[4];
    public float[] cameraRotation = new float[4];

    public PlayerData(Player player)
    {
        this.level = player.level;
        this.questLevel = player.questLevel;
        this.maxHp = player.maxHp;
        this.curHp = player.curHp;
        this.curExp = player.curExp;

        for(int i = 0; i < position.Length; i++)
        {
            position[i] = player.transform.position[i];
        }

        for(int i = 0; i < rotation.Length; i++)
        {
            rotation[i] = player.transform.rotation[i];
            cameraRotation[i] = player.GetCameraRotation()[i];
        }
    }

    public PlayerData GetPlayerData() => this;
    public int GetPlayerLevel() => this.level; 
    public int GetPlayerQuestLevel() => this.questLevel;
    public float GetPlayerMaxHp() => this.maxHp;
    public float GetPlayerCurHp() => this.curHp;
    public float GetPlayerCurExp() => this.curExp;
    public float[] GetPlayerPosition() => this.position;
    public float[] GetPlayerRotation() => this.rotation;
    public float[] GetCameraRotation() => this.rotation;
}
