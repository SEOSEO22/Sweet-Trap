using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image itemImage;       // ������ �̹���
    [SerializeField] private Image borderImage;     // �κ��丮 ���� �̹���
    [SerializeField] private Color initSlotColor;   // �ʱ� �κ��丮 ���� ����
    [SerializeField] private bool isEmpty;          // �������� �����ϴ��� Ȯ���ϴ� bool ����

    public event Action<UIInventoryItem> OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag;

    private void Awake()
    {
        initSlotColor = borderImage.color;

        ResetData();
        Deselect();
    }

    // ������ ������ ���� ��� (Empty ���·� ����)
    public void ResetData()
    {
        if (itemImage != null)
        {
            itemImage.gameObject.SetActive(false);
            isEmpty = true;
        }
    }

    // �Ű������� ���� sprite�� ������ �̹��� ���� �� ������ �� ���
    public void SetData(Sprite sprite)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        isEmpty = false;
    }

    // �������� ����(MouseEnter)���� ��� ���� ���� ��ȭ
    public void Select()
    {
        borderImage.color = initSlotColor * .5f;
    }

    // �����ۿ��� MouseExit�� ��� ���� ������ ���� ���·� �ʱ�ȭ
    public void Deselect()
    {
        borderImage.color = initSlotColor;
    }

    // ������ �巡�׸� �������� ���� ��� (�׼� ���Ǵ� UIInventoryPage.cs�� ����)
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

    // �������� ������� ���� ��� (�׼� ���Ǵ� UIInventoryPage.cs�� ����)
    // ������ �巡�׸� ������ ���� ��� (�׼� ���Ǵ� UIInventoryPage.cs�� ����)
    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        OnItemEndDrag?.Invoke(this);
    }

    // �����ۿ� ���콺�� �ö��� �� Select �Լ� ����
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData != null)
        {
            Select();
        }
    }

    // �����ۿ��� ���콺�� �������� �� Deselect �Լ� ����
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData != null)
        {
            Deselect();
        }
    }
}
