using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDButtonUI : MonoBehaviour
{
    [SerializeField] Button inventoryButton;
    [SerializeField] Button settingsButton;

    private void Awake()
    {
        inventoryButton.onClick.AddListener(OnClickOpenInventoryUI);
        settingsButton.onClick.AddListener(OnClickOpenSettingsUI);
    }

    public void OnClickOpenInventoryUI()
    {
        UIManager.Instance.OpenUI<InventoryUI>();
        if(EntityManager.Instance.player.questLevel == 1)
            QuestManager.Instance.CompleteQuest();
    }

    public void OnClickOpenSettingsUI() => UIManager.Instance.OpenUI<SettingsUI>();
}
