using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSkill : MonoBehaviour
{
    [SerializeField] private GameObject[] effects;
    [SerializeField] private Image warningMark;

    private PatternInfo patternInfo;
    private void OnEnable()
    {
        StartCoroutine(PlayPattern());
    }

    private IEnumerator PlayPattern()
    {
        if(warningMark != null)
        {
            warningMark.gameObject.SetActive(true);
            yield return YieldCache.GetCachedTimeInterval(patternInfo.markingTime);
            warningMark.gameObject.SetActive(false);
        }

        if (gameObject.name == "Roar")
            Summon();
        else if (gameObject.name == "EarthQuake")
            StartCoroutine(EarthQuake());
        else
            StartCoroutine(CuttingOff());

        void Summon()
        {
            SoundManager.Instance.PlayClip("SkeletonSummon", transform);
            var waveLevel = GameManager.Instance.GetWaveLevel();
            var waveInfo = DataManager.Instance.entireDataContainer.waveDataContainer.Get(waveLevel);
            foreach (var effect in effects)
            {
                effect.SetActive(true);
                foreach(var monsterPrefab in waveInfo.monsterPrefabs)
                {
                    if (monsterPrefab.GetComponent<Boss>() != null)
                        continue;

                    GameObject monster = ResourceManager.Instance.Instantiate(monsterPrefab);
                    monster.transform.position = effect.transform.position;

                    var monsterObj = monster.GetComponent<Monster>();
                    monsterObj.navMeshAgent.enabled = true;

                }
            }
        }

        IEnumerator EarthQuake()
        {
            SoundManager.Instance.PlayClip("OrkEarthQuake", transform);
            foreach (var effect in effects)
            {
                effect.SetActive(true);
                yield return YieldCache.GetCachedTimeInterval(patternInfo.playDelay);
            }
            SoundManager.Instance.PlayClip("EarthQuake", transform);
        }

        IEnumerator CuttingOff()
        {
            SoundManager.Instance.PlayClip("OrkCuttingOff", transform);
            foreach (var effect in effects)
                effect.SetActive(true);
            SoundManager.Instance.PlayClip("CuttingOff", transform);
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        if(warningMark != null)
            warningMark.gameObject.SetActive(false);
        foreach(var effect in effects)
            effect.SetActive(false);
    }

    public void SetPatternInfo(PatternInfo patternInfo) => this.patternInfo = patternInfo;
}
