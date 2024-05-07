using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Popup : UI
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

    public override void Open(UIParameter param)
    {
        gameObject.transform.SetAsLastSibling();
        SoundManager.Instance.PlayClip("OpenUI", EntityManager.Instance.player.transform);
    }
}
