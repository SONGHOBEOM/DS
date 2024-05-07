using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Light portalLight;
    [SerializeField] private ParticleSystem portalEffect;

    public static bool stopFlag = false;

    private WaveInfo waveInfo;
    private int randomObjNum;
    private int randomPoint;
    private int instantiateCount = 0;

    private float waitForSeconds = 3.0f;

    private Coroutine instantiate = null;

    public void Spawn(WaveInfo waveInfo)
    {
        stopFlag = false;
        this.waveInfo = waveInfo;

        if (waveInfo.waveType != Define.WaveType.Boss)
        {
            StartCoroutine(CheckFlag());
            instantiate = StartCoroutine(Instantiate());
            ChangeSpawnerColor();
            OnEffect();
        }
        else
        {
            InstantiateBossMonster();
            ChangeSpawnerColor();
            OnEffect();
        }
    }
    private IEnumerator Instantiate()
    {
        while (true)
        {
            yield return YieldCache.GetCachedTimeInterval(waitForSeconds);

            instantiateCount = GameManager.Instance.GetInstantiateCount();
            if (instantiateCount >= waveInfo.monsterCount)
                yield return null;

            if (stopFlag == false)
            {
                InstantiateNormalMonster();
                GameManager.Instance.AddInstantiateCount();
            }
        }
    }

    private IEnumerator CheckFlag()
    {
        while (true)
        {
            if (stopFlag)
            {
                StopSpawn();
                yield break;
            }
            yield return null;
        }
    }
    public void StopSpawn()
    {
        StopAllCoroutines();
        instantiateCount = 0;
        OffEffect();
    }

    private void InstantiateNormalMonster()
    {
        randomObjNum = UnityEngine.Random.Range(0, waveInfo.monsterPrefabs.Length);
        randomPoint = UnityEngine.Random.Range(0, spawnPoint.Length);
        var monsterObj = ResourceManager.Instance.Instantiate(waveInfo.monsterPrefabs[randomObjNum]);
        monsterObj.transform.position = spawnPoint[randomPoint].position;
        var monster = monsterObj.GetComponent<Monster>();
        monster.navMeshAgent.enabled = true;
    }

    private void InstantiateBossMonster()
    {
        var monsterObj = ResourceManager.Instance.Instantiate(waveInfo.monsterPrefabs[0]);
        monsterObj.transform.position = spawnPoint[0].position;
        var monster = monsterObj.GetComponent<Monster>();
        monster.navMeshAgent.enabled = true;
    }

    private void ChangeSpawnerColor()
    {
        foreach (var monster in waveInfo.monsterPrefabs)
        {
            if (waveInfo.waveType == Define.WaveType.Boss)
            {
                var changedColor = Color.black;
                foreach (var material in _renderer.materials)
                    _renderer.material.color = changedColor;
            }
            else
            {
                var changedColor = Color.red;
                foreach (var material in _renderer.materials)
                    _renderer.material.color = changedColor;
            }
        }
    }

    private void OnEffect()
    {
        portalEffect.gameObject.SetActive(true);
        ParticleSystem.MainModule main = portalEffect.main;
        main.startColor = _renderer.material.color;
        portalLight.color = _renderer.material.color;
    }

    private void OffEffect()
    {
        portalEffect.gameObject.SetActive(false);
    }
}
