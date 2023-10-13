using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour
{
    [SerializeField] private Text timer;

    public static Action<WaveInfo> onStart = null;
    public static Action clear = null;

    private void Start()
    {
        onStart += SetTimer;
        clear += ClearWave;
    }

    private void SetTimer(WaveInfo waveInfo)
    {
        timer.gameObject.SetActive(true);
        StartCoroutine(CheckWaveTime(waveInfo));
    }

    private IEnumerator CheckWaveTime(WaveInfo waveInfo)
    {
        var waveTime = waveInfo.waveTime;
        while (waveTime > 0)
        {
            timer.text = $"Time : {waveTime.ToString("F2")}";
            waveTime -= Time.deltaTime;
            yield return null;
        }
        if (EntityManager.Instance.player.questLevel == 3 || EntityManager.Instance.player.questLevel == 4)
            QuestManager.Instance.CompleteQuest();
        ClearWave();
    }

    private void ClearWave()
    {
        SpawnerController.isStopped?.Invoke();
        timer.gameObject.SetActive(false);
    }
}
