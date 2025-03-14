using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<InventoryItem> inventoryItems;
    [field: SerializeField] public int Size { get; private set; } = 8;
    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

    public void Initialize()
    {
        inventoryItems = new List<InventoryItem>();

        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

    public void AddItem(ItemSO item)
    {
        InventoryItem newItem = new InventoryItem
        {
            item = item,
        };

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty && !IsInventoryFull())
            {
                inventoryItems[i] = newItem;
                InformAboutChange();
                return;
            }
        }
    }

    public void AddItem(InventoryItem item)
    {
        AddItem(item.item);
    }

    private bool IsInventoryFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false;

    public void RemoveItem(int itemIndex)
    {
        if (inventoryItems.Count > itemIndex)
        {
            if (inventoryItems[itemIndex].IsEmpty) return;

            inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
        }

        InformAboutChange();
    }

    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty) continue;
            returnValue[i] = inventoryItems[i];
        }

        return returnValue;
    }

    public InventoryItem GetItemAt(int itemIndex)
    {
        return inventoryItems[itemIndex];
    }

    public void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
    }
}

[Serializable]
public struct InventoryItem
{
    public ItemSO item;
    public bool IsEmpty => item == null;

    public static InventoryItem GetEmptyItem() => new InventoryItem
    {
        item = null,
    };
}