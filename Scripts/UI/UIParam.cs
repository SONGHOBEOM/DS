using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UIParam
{

}

public class QuestListUIParam : UIParam
{
    public QuestListItem quest;
    public QuestData questData;

    public QuestListUIParam(QuestListItem quest, QuestData questData)
    {
        this.quest = quest;
        this.questData = questData;
    }
}

public class PurchasePopupUIParam : UIParam
{
    public ItemBase itemBase;

    public PurchasePopupUIParam(ItemBase itemBase)
    {
        this.itemBase = itemBase;
    }
}
