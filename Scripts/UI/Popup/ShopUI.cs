using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ResourceInfo("ShopUI")]
public class ShopUI : Popup
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
        UI.AddUIEvent(gameObject, DraggingUI, Define.UIEvent.Drag);
        UI.AddUIEvent(gameObject, SetIsDraggingUI, Define.UIEvent.ClickDown);
        UI.AddUIEvent(gameObject, SetIsNotDraggingUI, Define.UIEvent.ClickUp);

        void DraggingUI(PointerEventData data) => transform.position += (Vector3)data.delta;
        void SetIsDraggingUI(PointerEventData data)
        {
            UIManager.Instance.SetIsDraggingUI(data.button == PointerEventData.InputButton.Left);
            gameObject.transform.SetAsLastSibling();
        }
        void SetIsNotDraggingUI(PointerEventData data) => UIManager.Instance.SetIsDraggingUI(false);
    }
    public override void Open(UIParameter param)
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
