using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ResourceInfo("ShopUI")]
public class ShopUI : UIPopup
{
    [SerializeField] private Text goldText;

    public static Action refreshGold = null;
    public override void Init()
    {
        base.Init();
        AddUIEvent();
        refreshGold += RefreshGold;
    }
    void AddUIEvent()
    {
        UIBase.AddUIEvent(gameObject, DraggingUI, Define.UIEvent.Drag);
        UIBase.AddUIEvent(gameObject, SetIsDraggingUI, Define.UIEvent.ClickDown);
        UIBase.AddUIEvent(gameObject, SetIsNotDraggingUI, Define.UIEvent.ClickUp);

        void DraggingUI(PointerEventData data) => transform.position += (Vector3)data.delta;
        void SetIsDraggingUI(PointerEventData data)
        {
            UIManager.Instance.SetIsDraggingUI(data.button == PointerEventData.InputButton.Left);
            gameObject.transform.SetAsLastSibling();
        }
        void SetIsNotDraggingUI(PointerEventData data) => UIManager.Instance.SetIsDraggingUI(false);
    }
    public override void Open(UIParam param)
    {
        base.Open(param);

        RefreshGold();
    }


    public override void Close()
    {
        SoundManager.Instance.PlayClip("UIClick");
        base.Close();
    }

    private void RefreshGold()
    {
        var gold = EntityManager.Instance.currencyData.GetPlayerGold();
        goldText.text = string.Format(STR.Get("OwnGold"), gold);
    }
}
