using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "ScriptableObject/BossData")]
public class BossData : MonsterData
{
    [SerializeField] private float _controlTime;
    public float controlTime { get { return _controlTime; } }
}
