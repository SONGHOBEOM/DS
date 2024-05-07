using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UIParameter
{

}

public class QuestListUIParameter : UIParameter
{
    public QuestListItem quest;
    public QuestData questData;

    public QuestListUIParameter(QuestListItem quest, QuestData questData)
    {
        this.quest = quest;
        this.questData = questData;
    }
}

public class PurchasePopupUIParameter : UIParameter
{
    public ItemBase itemBase;

    public PurchasePopupUIParameter(ItemBase itemBase)
    {
        this.itemBase = itemBase;
    }
}
