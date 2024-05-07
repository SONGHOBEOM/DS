using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private ItemIcon itemIcon;

    public ItemBase itemBase = null;
    public bool isEquipped = false;

    public void SetData(ItemBase itemBase)
    {
        if (itemBase == null)
            itemIcon.ClearItemImage();

        this.itemBase = itemBase;
        itemIcon.SetItem(this.itemBase.itemData);
    }

    public ItemBase GetData() => this.itemBase;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.itemBase == null)
            return;

        if(Util.FindChild<Tooltip>(UIManager.Instance.uiRoot.gameObject,"Tooltip",false) == null)
        {
            var tooltipObject = ResourceManager.Instance.Instantiate<Tooltip>("Tooltip", UIManager.Instance.uiRoot);
            tooltipObject.SetupTooltipInfo(this.itemBase);
            tooltipObject.gameObject.SetActive(true);
        }
        else
        {
            var tooltipObject = Util.FindChild<Tooltip>(UIManager.Instance.uiRoot.gameObject, "Tooltip", false);
            tooltipObject.SetupTooltipInfo(this.itemBase);
            tooltipObject.gameObject.SetActive(true);
        }
    } 

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.itemBase == null)
            return;

        var tooltipObject = Util.FindChild<Tooltip>(UIManager.Instance.uiRoot.gameObject, "Tooltip", false);
        tooltipObject.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.itemBase == null) return;

        SoundManager.Instance.PlayClip("UIClick");

        var itemType = this.itemBase.itemData.itemType;
        if (itemType == Define.ItemType.Weapon
                || itemType == Define.ItemType.Armor)
        {
            OnClickEquip();
        }
        Tooltip.onDisabled?.Invoke();
    }

    public void OnClickEquip()
    {
        InventoryManager.Instance.EquipItem(this.itemBase);
        ResourceManager.Instance.Destroy(gameObject);
    }
}
