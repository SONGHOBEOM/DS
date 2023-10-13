using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class QuestHelper
{
    private static Dictionary<int, QuestData> entireQuests = new Dictionary<int, QuestData>();
    public static void CreateQuests()
    {
        var questDatas = DataManager.Instance.entireDataContainer.questDataContainer.Get();

        foreach (var questData in questDatas)
            entireQuests.Add(questData.questLevel, questData);
    }
    public static QuestData GetQuest(int level)
    {
        if(entireQuests.TryGetValue(level, out var questData))
            return questData;

        return default;
    }
}
