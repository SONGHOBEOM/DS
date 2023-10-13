using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObject/SoundData")]
public class SoundData : ScriptableObject
{
    [SerializeField] private AudioClip _audioClip;
    public AudioClip audioClip { get { return _audioClip; } }

    [SerializeField] private string _clipName;
    public string clipName { get { return _clipName; } }

    [SerializeField] private float _volume;
    public float volume { get { return _volume; } }

    [SerializeField] private float _spatialValue;
    public float spatialValue { get { return _spatialValue; } }

    [SerializeField] private float _maxDistance;
    public float maxDistance { get {  return _maxDistance; } }
}
