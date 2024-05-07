using PlayerCORE;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class DialogManager : Singleton<DialogManager>
{
    private Dictionary<string, string> dialogDatas = new Dictionary<string, string>();

    public void OnDiaLog(NpcData npcData)
    {
        PlayerStateHelper.isTalking = true;
        SoundManager.Instance.PlayClip(npcData.npcName);
        var dialog = ResourceManager.Instance.Instantiate<Dialog>("Dialog", UIManager.Instance.uiRoot);
        var rectTransform = dialog.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        dialog.npcNameText.text = STR.Get(npcData.npcName);
        Dialog(dialog, npcData);
    }
    private void Dialog(Dialog dialog, NpcData npcData)
    {
        dialog.dialogContentText.text = STR.Get($"{npcData.npcName}Dialog");
        GameObject ui;
        if (npcData.npcName == "RuneStone")
        {
            ui = dialog.runeStoneButtonUI.gameObject;
            ui.SetActive(true);
        }
        else
        {
            ui = dialog.npcButtonUI.gameObject;
            ui.SetActive(true);
        }
        
    }

    public void CloseDialog(GameObject go) => ResourceManager.Instance.Destroy(go);
}
