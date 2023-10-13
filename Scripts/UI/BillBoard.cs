using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Transform cameraPosition;
    private float updateTime = 0.5f;

    private void OnEnable()
    {
        if(cameraPosition == null)
            cameraPosition = EntityManager.Instance.mainCamera.transform;
        StartCoroutine(LookCamera());
    }

    private void OnDisable()
    {
        StopCoroutine(LookCamera());
    }

    private IEnumerator LookCamera()
    {
        while (true)
        {
            gameObject.transform.LookAt(cameraPosition);
            yield return YieldCache.GetCachedTimeInterval(updateTime);
        }
    }
}
