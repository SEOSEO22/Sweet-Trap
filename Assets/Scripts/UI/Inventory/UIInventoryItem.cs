using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image itemImage;       // 아이템 이미지
    [SerializeField] private Image borderImage;     // 인벤토리 슬롯 이미지
    [SerializeField] private Color initSlotColor;   // 초기 인벤토리 슬롯 색상
    [SerializeField] private bool isEmpty;          // 아이템이 존재하는지 확인하는 bool 변수

    public event Action<UIInventoryItem> OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag;

    private void Awake()
    {
        initSlotColor = borderImage.color;

        ResetData();
        Deselect();
    }

    // 아이템 데이터 리셋 기능 (Empty 상태로 설정)
    public void ResetData()
    {
        if (itemImage != null)
        {
            itemImage.gameObject.SetActive(false);
            isEmpty = true;
        }
    }

    // 매개변수로 받은 sprite로 아이템 이미지 지정 및 데이터 셋 기능
    public void SetData(Sprite sprite)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        isEmpty = false;
    }

    // 아이템이 선택(MouseEnter)됐을 경우 슬롯 색상 변화
    public void Select()
    {
        borderImage.color = initSlotColor * .5f;
    }

    // 아이템에서 MouseExit인 경우 슬롯 색상을 원래 상태로 초기화
    public void Deselect()
    {
        borderImage.color = initSlotColor;
    }

    // 아이템 드래그를 시작했을 때의 기능 (액션 정의는 UIInventoryPage.cs에 존재)
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isEmpty) return;

        GetComponent<Image>().raycastTarget = false;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    // 아이템을 사용했을 때의 기능 (액션 정의는 UIInventoryPage.cs에 존재)
    // 아이템 드래그를 끝냈을 때의 기능 (액션 정의는 UIInventoryPage.cs에 존재)
    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        OnItemEndDrag?.Invoke(this);
    }

    // 아이템에 마우스가 올라갔을 때 Select 함수 실행
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData != null)
        {
            Select();
        }
    }

    // 아이템에서 마우스가 내려왔을 때 Deselect 함수 실행
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData != null)
        {
            Deselect();
        }
    }
}
