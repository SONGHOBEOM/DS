using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NpcManager : Singleton<NpcManager>
{
    private NpcData npcData = null;

    public void OnNpcDialog() => DialogManager.Instance.OnDiaLog(npcData);
    public void SetNpcData(NpcData npcData) => this.npcData = npcData;
    public NpcData GetNpcData() => this.npcData;
}
