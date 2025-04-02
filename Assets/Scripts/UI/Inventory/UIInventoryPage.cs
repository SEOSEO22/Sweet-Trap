using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField] private UIInventoryItem itemPrefab;
    [SerializeField] private MouseFollower mouseFollower;
    [SerializeField] private GameObject ContentPanel;

    private List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();
    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnStartDragging;
    public event Action<int, Vector2> OnItemDrop;

    private void Awake()
    {
        mouseFollower.Toggle(false);
    }

    // �κ��丮 �����ŭ �κ��丮 ������ �ʱ�ȭ �� �׼� ����
    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, ContentPanel.transform);
            listOfUIItems.Add(uiItem);

            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemEndDrag += HandleEndDrag;
        }
    }

    // ����Ʈ�� itemIndex ��ġ�� ������ ������ ����
    public void UpdateData(int itemIndex, Sprite itemImage)
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemImage);
        }
    }

    // �巡���ϴ� �������� �ε����� �˾ƿ� �ش� �������� ����ϴ� �Լ�
    // (�׼� ���Ǵ� InventoryController.cs�� ����)
    public void HandleBeginDrag(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1) return;
        currentlyDraggedItemIndex = index;

        OnStartDragging?.Invoke(index);
    }

    // �������� ������� �� ���
    public void HandleEndDrag(UIInventoryItem inventoryItemUI)
    {
        ResetDraggedItem();

        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1) return;

        Vector2 screenPosition = Input.mousePosition;
        OnItemDrop?.Invoke(index, screenPosition);
    }

    // �巡���Ϸ��� �������� �̹����� ������ ���콺�� ����ٴϵ��� �巡�� ������ ����
    public void CreatedDraggedItem(Sprite sprite)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite);
    }

    // ������ �巡�� �������� ��Ȱ��ȭ ��Ű�� ������ �ε����� -1�� �ʱ�ȭ
    public void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    // �κ��丮�� ��� �������� �����͸� �ʱ�ȭ ��Ű�� ���
    public void ResetAllItems()
    {
        foreach (var item in listOfUIItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }
}
