using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ResourceInfo("PurchasingUI")]
public class PurchasingUI : UIPopup
{
    [SerializeField] Button confirmButton;
    [SerializeField] Button cancelButton;
    [SerializeField] Button reduceButton;
    [SerializeField] Button addButton;
    [SerializeField] Text totalPriceText;
    [SerializeField] Text itemNumberText;
    [SerializeField] GameObject warnMessage;
    [SerializeField] Text warnMessageText;

    private ItemBase item = null;
    private int itemNumber;
    private int defaultitemNumber = 1;

    private void Awake()
    {
        InitListener();
    }

    private void InitListener()
    {
        addButton.onClick.AddListener(AddItemNumber);
        reduceButton.onClick.AddListener(ReduceItemNumber);
        confirmButton.onClick.AddListener(Purchase);
        cancelButton.onClick.AddListener(Close);
    }

    public override void Open(UIParam param)
    {
        base.Open(param);

        if (!(param is PurchasePopupUIParam))
        {
            Close();
            return;
        }
        var purchasingPopup = param as PurchasePopupUIParam;
        item = purchasingPopup.itemBase;
        
        ShowTotalPrice();
    }

    public override void Close()
    {
        base.Close();
        itemNumber = defaultitemNumber;
        itemNumberText.text = itemNumber.ToString();
        gameObject.SetActive(false);
    }

    void Purchase()
    {
        var gold = EntityManager.Instance.currencyData.GetPlayerGold();
        var price = item.itemData.itemPrice;
        if (gold >= price * itemNumber)
        {
            for(int i = 0; i < itemNumber; i++)
            {
                InventoryManager.Instance.AddItem(item);
            }
            EntityManager.Instance.currencyData.AddGold(-(price * itemNumber));
            ShopUI.refreshGold?.Invoke();
            SoundManager.Instance.PlayClip("Trade", EntityManager.Instance.player.transform);

            if (EntityManager.Instance.player.questLevel == 2)
                QuestManager.Instance.CompleteQuest();

            gameObject.SetActive(false);
        }
        else
        {
            warnMessage.SetActive(true);
            warnMessageText.text = STR.Get("WarningMessageGold");
        }

    }
    void AddItemNumber()
    {
        itemNumber++;
        itemNumberText.text = itemNumber.ToString();
        ShowTotalPrice();
    }
    void ReduceItemNumber()
    {
        if (itemNumber == 1)
            return;
        itemNumber--;
        itemNumberText.text = itemNumber.ToString();
        ShowTotalPrice();
    }
    void ShowTotalPrice()
    {
        if(itemNumber == 0)
            itemNumber = defaultitemNumber;

        var price = item.itemData.itemPrice;
        totalPriceText.text = string.Format(STR.Get("TotalPrice"), price);
    }
}
