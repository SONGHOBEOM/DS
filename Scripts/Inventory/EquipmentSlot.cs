using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private ItemIcon itemIcon;
    public ItemBase itemBase = null;

    public void SetData(ItemBase itemBase)
    {
        if (itemBase == null)
            itemIcon.ClearItemImage();

        this.itemBase = itemBase;
        itemIcon.SetItem(this.itemBase.itemData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlayClip("UIClick");
        InventoryManager.Instance.UnEquipItem(itemBase);
        ResourceManager.Instance.Destroy(gameObject);
        Tooltip.onDisabled?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.itemBase == null)
            return;

        if (Util.FindChild<Tooltip>(UIManager.Instance.UI, "Tooltip", false) == null)
        {
            var tooltipObject = ResourceManager.Instance.Instantiate<Tooltip>("Tooltip", UIManager.Instance.UI.transform);
            tooltipObject.SetupTooltipInfo(this.itemBase);
            tooltipObject.gameObject.SetActive(true);
        }
        else
        {
            var tooltipObject = Util.FindChild<Tooltip>(UIManager.Instance.UI, "Tooltip", false);
            tooltipObject.SetupTooltipInfo(this.itemBase);
            tooltipObject.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.itemBase == null)
            return;

        var tooltipObject = Util.FindChild<Tooltip>(UIManager.Instance.UI, "Tooltip", false);
        tooltipObject.gameObject.SetActive(false);
    }
}
