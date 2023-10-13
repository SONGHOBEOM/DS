using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class VirtualJoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform lever;
    [SerializeField, Range(0,150)] private float leverRange;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerTransform;

    private RectTransform rectTransform;
    private Vector2 inputDirection;
    private bool isInput;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoyStickLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoyStickLever(eventData);
    }

    public void ControlJoyStickLever(PointerEventData eventData)
    {
        var inputpos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputpos.magnitude < leverRange ? inputpos : inputpos.normalized * leverRange; // inputpos.magnitude가 leverRange 보다 작은지 확인해서 작다면 inputpos, 아니라면 inputpos.normalized * leverRange를 반환한다.
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange * 1.3f; // 플레이어의 속도를 의미
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        InputHelper.characterPosCoordinate = Vector2.zero;
    }

    private void InputControlVector()
    {
        InputHelper.characterPosCoordinate = inputDirection;
    }

    private void Update()
    {
        if(isInput)
        {
            InputControlVector();
        }
    }

}

