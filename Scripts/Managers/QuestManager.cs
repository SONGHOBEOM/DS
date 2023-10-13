using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    public static Action<int> updateQuestListItem = null;
    private void Start()
    {
        updateQuestListItem = UpdateQuestlistItem;
    }

    public void UpdateQuestlistItem(int level)
    {
        var questData = QuestHelper.GetQuest(level);
        UIManager.Instance.OpenUI<QuestListItem>();
        var questListItem = Util.FindChild<QuestListItem>(UIManager.Instance.UI);
        questListItem.SetQuestTitle(questData);
    }

    public void CompleteQuest()
    {
        EntityManager.Instance.player.QuestLevelUp();
        UpdateQuestlistItem(EntityManager.Instance.player.questLevel);
    }
}
