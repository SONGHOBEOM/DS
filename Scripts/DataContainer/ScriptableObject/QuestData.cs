using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "ScriptableObject/QuestData")]
public class QuestData : ScriptableObject
{
    [SerializeField] private int _questLevel;
    public int questLevel { get { return _questLevel; } }

    [SerializeField] private string _questName;
    public string questName { get { return _questName; } }

    [SerializeField] private Define.QuestType _questType;
    public Define.QuestType questType { get {  return _questType; } }

    [SerializeField] private string _questDescription;
    public string questDescription { get { return _questDescription; } }
}