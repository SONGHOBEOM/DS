using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundHelper
{
    private static Dictionary<string, SoundData> entireSoundDatas = new Dictionary<string, SoundData>();

    public static void CreateSoundData()
    {
        var soundDatas = DataManager.Instance.entireDataContainer.soundDataContainer.Get();
        foreach(var soundData in soundDatas)
        {
            entireSoundDatas.Add(soundData.clipName, soundData);
        }
    }

    public static SoundData GetSoundData(string clipName)
    {
        if(entireSoundDatas.TryGetValue(clipName, out var soundData))
            return soundData;

        return default;
    }
}
