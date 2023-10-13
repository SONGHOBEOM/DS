using JetBrains.Annotations;
using PlayerCORE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class EffectSystem : Singleton<EffectSystem>
{
    public void ShowActiveEffect(int weaponIndex, string skillName)
    {
        var effectList = DataManager.Instance.entireDataContainer.effectDataContainer.Get();
        var effect = effectList.FirstOrDefault(x => x.weaponIndex == weaponIndex && x.skillName == skillName);

        StartCoroutine(Play(effect));
    }

    public void ShowEffect(string skillName)
    {
        var effectList = DataManager.Instance.entireDataContainer.effectDataContainer.Get();
        var effect = effectList.FirstOrDefault(x => x.skillName == skillName);

        StartCoroutine(Play(effect));
    }

    private IEnumerator Play(EffectData effectData)
    {
        var player = EntityManager.Instance.player;
        SoundManager.Instance.PlayClip(effectData.skillName);
        yield return YieldCache.GetCachedTimeInterval(effectData.playDelay);
       
        GameObject effect = ResourceManager.Instance.Instantiate(effectData.effect, player.transform);
        effect.transform.localPosition = Vector3.zero + effectData.effectRange;
        effect.transform.localRotation = effectData.effectRotation;

        effect.transform.SetParent(null, true);
        
        StartCoroutine(Hide(effect, effectData.playDuration));
    }

    private IEnumerator Hide(GameObject effect, float playDuration)
    {
        yield return YieldCache.GetCachedTimeInterval(playDuration);
        ResourceManager.Instance.Destroy(effect);
        PlayerStateHelper.isAttacking = false;
    }
}
