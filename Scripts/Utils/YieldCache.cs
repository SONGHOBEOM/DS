using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public static class YieldCache
{
    public static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    public static readonly WaitForSecondsRealtime waifForSecond = new WaitForSecondsRealtime(1);

    private static Dictionary<float, WaitForSeconds> _timeInterval = new Dictionary<float, WaitForSeconds>(new FloatComparer());

    public static WaitForSeconds GetCachedTimeInterval(float timeInterval)
    {
        if(_timeInterval.TryGetValue(timeInterval, out var waitForSeconds)) return waitForSeconds;
        else
        {
            var createdInterval = new WaitForSeconds(timeInterval);
            _timeInterval.Add(timeInterval, createdInterval);
            return createdInterval;
        }

    }
}
