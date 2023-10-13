using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DialogButtonUI : IObserver
{
    [SerializeField] private Button shopButton;
    [SerializeField] private Text shopButtonText;
    [SerializeField] private Button summonButton;
    [SerializeField] private Text summonButtonText;
    [SerializeField] private Button finishButton;
    [SerializeField] private Text finishButtonText;

    private NpcData npcData = null;
    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        SubButtonFunc();
    }

    private void Init()
    {
        this.npcData = NpcManager.Instance.GetNpcData();
        AddButtonFunc();

        shopButtonText.text = STR.Get("ShopButton");
        summonButtonText.text = STR.Get("SummonButton");
        finishButtonText.text = STR.Get("FinishButton");
    }

    void AddButtonFunc()
    {
        shopButton.onClick.AddListener(OpenShop);
        finishButton.onClick.AddListener(CloseDialog);
        summonButton.onClick.AddListener(StartWave);
    }

    void SubButtonFunc()
    {
        shopButton.onClick.RemoveAllListeners();
        finishButton.onClick.RemoveAllListeners();
        summonButton.onClick.RemoveAllListeners();
    }

    void OpenShop()
    {
        SoundManager.Instance.PlayClip("UIClick");
        UIManager.Instance.OpenUI<ShopUI>();
    }

    void StartWave()
    {
        SoundManager.Instance.PlayClip("UIClick");
        SpawnerController.isStarted?.Invoke();
        CloseDialog();
    }

    void CloseDialog()
    {
        SoundManager.Instance.PlayClip("UIClick");
        DialogManager.Instance.CloseDialog(transform.parent.gameObject); 
    }

}


