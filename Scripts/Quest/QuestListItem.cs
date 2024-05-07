using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ResourceInfo("QuestListItem", 20, -262)]
public class QuestListItem : UI
{
    [SerializeField] private Text questNameText;
    [SerializeField] private Text questTypeText;
    [SerializeField] private Text questDescriptionText;

    public void SetQuestTitle(QuestData questData)
    {
        this.questNameText.text = questData.questName;
        this.questTypeText.text = questData.questType.ToString();
        this.questDescriptionText.text = questData.questDescription;
        SetQuestTypeTextColor();
    }

    public void SetQuestTypeTextColor()
    {
        if(questTypeText.text == Define.QuestType.Normal.ToString()) { questTypeText.color = Color.gray; }
        else if(questTypeText.text == Define.QuestType.Epic.ToString()) { questTypeText.color = Color.red; }
    }

    public override void Open(UIParameter param) { }
}
