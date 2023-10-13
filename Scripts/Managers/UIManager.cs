using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;


public class UIManager : Singleton<UIManager>
{
    private Dictionary<Type, UIBase> cachedUI = new Dictionary<Type, UIBase>();
    private Stack<UIBase> uiStack = new Stack<UIBase>();
    public bool isDraggingUI = false;
    public bool onDragToRotate = false;
    public GameObject UI
    {
        get
        {
            GameObject _ui = GameObject.Find("UserInterface");
            if (_ui == null)
            {
                _ui = new GameObject { name = "UserInterface" };
            }
            return _ui;
        }
    }

    public void OpenUI<T>(UIParam param = null) where T : UIBase
    {
        var type = typeof(T); 

        if (cachedUI.ContainsKey(type))
            DoOpen(type, param);
        else
            LoadOpen<T>(type, param);
    }

    public void CloseUI<T>() where T : UIBase
    {
        var type = typeof(T);
        if (cachedUI.ContainsKey(type) == false)
            return;

        DoClose(type);
    }

    private void DoOpen(Type type, UIParam param)
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

    private void LoadOpen<T>(Type type, UIParam param) where T : UIBase
    {
        var resourceInfo = type.GetCustomAttribute<ResourceInfo>();
        var name = resourceInfo.resourceName;
        var ui = ResourceManager.Instance.Instantiate(name, UI.transform);
        var rectTransform = ui.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(resourceInfo.posX, resourceInfo.posY);
        var uibase = ui.GetComponent<T>() as UIBase;
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

    public UIBase GetActiveUI<T>() where T : UIBase
    {
        var type = typeof(T);
        if (cachedUI.ContainsKey(type) == false)
            return null;

        var ui = cachedUI[type];

        return ui;
    }


    public void SetIsDraggingUI(bool isDragging) => this.isDraggingUI = isDragging;

    public void SetOnDragToRotate(bool onDragToRotate) => this.onDragToRotate = onDragToRotate;

    public void SetSkillSprite(List<SkillData> skillDatas)
    {
        CombatUI.setSkillButtons?.Invoke(skillDatas);
    }
}
