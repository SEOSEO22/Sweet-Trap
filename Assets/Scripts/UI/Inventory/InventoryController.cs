using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UIInventoryPage inventoryUI;
    [SerializeField] private InventorySO inventoryData;

    public List<InventoryItem> initialItems = new List<InventoryItem>();

    private void Start()
    {
        PrepareUI();
        PrepareInventoryData();
    }

    private void PrepareInventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateInventoryUI;

        foreach (InventoryItem item in initialItems)
        {
            if (item.IsEmpty) continue;
            inventoryData.AddItem(item);
        }

        foreach (var item in inventoryData.GetCurrentInventoryState())
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.Icon);
        }
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnItemDrop += HandleItemUse;
        this.inventoryUI.OnStartDragging += HandleDragging;
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.Icon);
        }
    }

    private void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);

        if (inventoryItem.IsEmpty) return;
        inventoryUI.CreatedDraggedItem(inventoryItem.item.Icon);
    }

    private void HandleItemUse(int itemIndex, Vector2 screenPosition)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty) return;


        // 화면 좌표를 월드 좌표로 변환
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        // 2D Raycast로 타겟 검출
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        if (hit.collider != null)
        {
            ITarget target = (ITarget)hit.collider.GetComponent<ITarget>();
            Debug.Log(target);
            if (target == null) return;

            bool isTargetItem = false;

            foreach (string targetName in ((PotItemSO)(inventoryItem.item)).TargetItem)
            {
                if (targetName == hit.collider.name)
                {
                    isTargetItem = true;
                    break;
                }
            }

            if (isTargetItem ==true)
            {
                IItemUse itemUse = inventoryItem.item as IItemUse;
                if (itemUse != null)
                {
                    itemUse.ItemUse();
                    target.UseItemAction(inventoryItem.item.DisplayName);
                }

                IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
                if (destroyableItem != null)
                {
                    inventoryData.RemoveItem(itemIndex);
                }
            }
        }
    }
}
