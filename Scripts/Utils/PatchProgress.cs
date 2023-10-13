using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class PatchProgress : MonoBehaviour
{
    [SerializeField] private Image progressBar;

    private float maxAmount = 1.0f;
    private float stabilizationTime = 2.0f;
    public void UpdateProgress(float loadCount, float totalCount)
    {
        if(gameObject.activeSelf == false)
            gameObject.SetActive(true);

        progressBar.fillAmount = loadCount / totalCount;

        if (progressBar.fillAmount >= 1.0f)
        {
            progressBar.fillAmount = 0;
            gameObject.SetActive(false);
        }

    }

}
