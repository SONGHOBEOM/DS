using PlayerCORE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MonsterAttackChecker : MonoBehaviour
{
    private MonsterData monsterData;
    private Define.ObjectType monsterType;
    private bool onHit = false;

    private void OnEnable()
    {
        onHit = false;
    }
    public void SetMonsterData(MonsterData monsterData) => this.monsterData = monsterData;
    public void SetMonsterType(Define.ObjectType monsterType) => this.monsterType = monsterType;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (PlayerStateHelper.isDodging || PlayerStateHelper.isDamaged)
                return;
            if(onHit == false)
            {
                onHit = true;
                var damage = Calculator.CalculateMonsterDamage(monsterData.monsterDamage);
                EntityManager.Instance.player.GetDamaged(damage);

                if (monsterType == Define.ObjectType.Boss)
                    PlayerStateHelper.isKnockedDown = true;
            }
        }
    }

}
