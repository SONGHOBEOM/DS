using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] ItemBase item;

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Instance.Open<PurchasingUI>(new PurchasePopupUIParameter(item));
    }

}
