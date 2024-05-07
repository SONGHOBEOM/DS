using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;


public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Transform _uiRoot;
    public Transform uiRoot => _uiRoot;

    private Dictionary<Type, UI> cachedUI = new Dictionary<Type, UI>();
    private Stack<UI> uiStack = new Stack<UI>();
    public bool isDraggingUI = false;
    public bool onDragToRotate = false;

    public void Open<T>(UIParameter param = null) where T : UI
    {
        var type = typeof(T); 

        if (cachedUI.ContainsKey(type))
            DoOpen(type, param);
        else
            LoadOpen<T>(type, param);
    }

    public void Close<T>() where T : UI
    {
        var type = typeof(T);
        if (cachedUI.ContainsKey(type) == false)
            return;

        DoClose(type);
    }

    private void DoOpen(Type type, UIParameter param)
    {
        if (cachedUI.TryGetValue(type, out var ui))
        {
            ui.gameObject.SetActive(true);
            ui.Open(param);
            uiStack.Push(ui);
        }
        else
            return;
    }

    private void LoadOpen<T>(Type type, UIParameter param) where T : UI
    {
        var uiPrefabInfo = type.GetCustomAttribute<UIPrefabInfo>();
        var name = uiPrefabInfo.resourceName;
        var ui = ResourceManager.Instance.Instantiate(name, uiRoot);
        var rectTransform = ui.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(resourceInfo.posX, resourceInfo.posY);
        var uibase = ui.GetComponent<T>() as UI;
        cachedUI[type] = uibase;

        uibase.Open(param);
    }
    private void DoClose(Type type)
    {
        if (cachedUI.TryGetValue(type, out var ui))
        {
            ui.gameObject.SetActive(false);
            ui.Close();
            uiStack.Pop();
        }
        else
            return;
    }

    public UI GetActive<T>() where T : UI
    {
        var type = typeof(T);
        if (cachedUI.ContainsKey(type) == false)
            return null;

        var ui = cachedUI[type];

        return ui;
    }


    public void SetIsDraggingUI(bool isDragging) => this.isDraggingUI = isDragging;

    public void SetOnDragToRotate(bool onDragToRotate) => this.onDragToRotate = onDragToRotate;
}
