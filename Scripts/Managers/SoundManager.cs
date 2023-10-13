using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private Dictionary<string, SoundData> cachedClips = new Dictionary<string, SoundData>();

    private void Start()
    {
        StartCoroutine(PlayMainMusic());
    }
    public void PlayClip(string name, Transform soundPoint = null)
    {
        if (cachedClips.TryGetValue(name, out var soundData))
        {
            StartCoroutine(Play(soundData, soundPoint));
        }
        else
        {
            var newSoundData = SoundHelper.GetSoundData(name);
            if (newSoundData == null) return;

            cachedClips.Add(name, newSoundData);

            StartCoroutine(Play(newSoundData, soundPoint));
        }
    }

    private IEnumerator Play(SoundData soundData, Transform soundPoint = null)
    {
        var soundPlayer = ResourceManager.Instance.Instantiate("SoundPlayer");

        if(soundPoint != null)
            soundPlayer.transform.position = soundPoint.position;

        var audioSource = soundPlayer.GetComponent<AudioSource>();
        audioSource.clip = soundData.audioClip;
        audioSource.volume = soundData.volume;
        audioSource.maxDistance = soundData.maxDistance;
        audioSource.spatialBlend = soundData.spatialValue;
        audioSource.Play();
        yield return YieldCache.GetCachedTimeInterval(soundData.audioClip.length);
        ResourceManager.Instance.Destroy(soundPlayer);
    }

    private IEnumerator PlayMainMusic()
    {
        while (true)
        {
            if (EntityManager.Instance.player != null)
            {
                PlayClip("Main", transform);
                yield break;
            }
            yield return null;
        }
    }
}
