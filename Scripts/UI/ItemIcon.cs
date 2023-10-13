using UnityEngine;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    
    public void SetItem(ItemData itemData)
    {
        SetItemImage(itemData);
    }
    private void SetItemImage(ItemData itemData)
    {
        itemImage.sprite = itemData.itemImage;
    }
    public void ClearItemImage()
    {
        itemImage.sprite = null;
    }
}
