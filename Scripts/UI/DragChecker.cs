using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragChecker : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        UIManager.Instance.SetOnDragToRotate(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIManager.Instance.SetOnDragToRotate(false);
    }
}
