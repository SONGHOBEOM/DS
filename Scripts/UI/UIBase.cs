using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIBase : MonoBehaviour
{
    public static void AddUIEvent(GameObject ui, Action<PointerEventData> action, Define.UIEvent type)
    {
        UIEventHandler evt = Util.GetOrAddComponent<UIEventHandler>(ui);
        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case Define.UIEvent.ClickDown:
                evt.OnClickDownHandler -= action;
                evt.OnClickDownHandler += action;
                break;
            case Define.UIEvent.ClickUp:
                evt.OnClickUpHandler -= action;
                evt.OnClickUpHandler += action;
                break;
        }
    }

    public abstract void Open(UIParam param);
    public virtual void Close() { }
    protected virtual void Visible() { }
    protected virtual void InVisible() { }

    protected virtual void Refresh() { }
}
