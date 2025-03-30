using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetZoomItem : MonoBehaviour
{
    [SerializeField] private CameraZoom zoom;
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private string needItemName;

    private Collider2D itemCollider;
    private ItemPickUp itemPickUp;

    private void Start()
    {
        itemCollider = GetComponent<Collider2D>();
        itemCollider.enabled = false;

        itemPickUp = GetComponent<ItemPickUp>();
        itemPickUp.isConditionMet = false;
    }

    private void Update()
    {
        GetItem();
    }

    private void GetItem()
    {
        if (zoom.isZoom)
        {
            itemCollider.enabled = true;

            if (IsItemExist(needItemName))
                itemPickUp.isConditionMet = true;
            else
                itemPickUp.isConditionMet = false;
        }
        else
            itemCollider.enabled = false;
    }

    // ������ ȹ�濡 �ʿ��� �������� �κ��丮�� �ִ��� Ȯ��
    private bool IsItemExist(string itemName)
    {
        if (itemName == "None") return true;

        bool isExist = false;
        Dictionary<int, InventoryItem> inventoryItems = inventoryData.GetCurrentInventoryState();

        foreach (InventoryItem item in inventoryItems.Values)
        {
            if (item.item.DisplayName == itemName)
            {
                isExist = true;
                break;
            }
        }

        return isExist;
    }
}
