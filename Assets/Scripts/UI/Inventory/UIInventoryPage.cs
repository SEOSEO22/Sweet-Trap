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

    // 인벤토리 사이즈만큼 인벤토리 아이템 초기화 및 액션 연결
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

    // 리스트의 itemIndex 위치의 아이템 데이터 세팅
    public void UpdateData(int itemIndex, Sprite itemImage)
    {
        if (listOfUIItems.Count > itemIndex)
        {
            listOfUIItems[itemIndex].SetData(itemImage);
        }
    }

    // 드래그하는 아이템의 인덱스를 알아와 해당 아이템을 사용하는 함수
    // (액션 정의는 InventoryController.cs에 존재)
    public void HandleBeginDrag(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1) return;
        currentlyDraggedItemIndex = index;

        OnStartDragging?.Invoke(index);
    }

    // 아이템을 사용했을 때 기능
    public void HandleEndDrag(UIInventoryItem inventoryItemUI)
    {
        ResetDraggedItem();

        int index = listOfUIItems.IndexOf(inventoryItemUI);
        if (index == -1) return;

        Vector2 screenPosition = Input.mousePosition;
        OnItemDrop?.Invoke(index, screenPosition);
    }

    // 드래그하려는 아이템의 이미지를 가져와 마우스를 따라다니도록 드래그 아이템 생성
    public void CreatedDraggedItem(Sprite sprite)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite);
    }

    // 생성된 드래그 아이템을 비활성화 시키고 아이템 인덱스를 -1로 초기화
    public void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    // 인벤토리의 모든 아이템의 데이터를 초기화 시키는 기능
    public void ResetAllItems()
    {
        foreach (var item in listOfUIItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }
}
