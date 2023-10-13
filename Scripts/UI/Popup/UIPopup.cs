using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPopup : UIBase
{
    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
    }

    public virtual void OnClose()
    {
        Close();
        gameObject.SetActive(false);
    }

    public override void Open(UIParam param)
    {
        gameObject.transform.SetAsLastSibling();
        SoundManager.Instance.PlayClip("OpenUI", EntityManager.Instance.player.transform);
    }
}
