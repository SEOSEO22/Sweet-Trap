using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    [SerializeField] private GameObject[] removeObjects;
    [SerializeField] private GameObject basementDoor;

    [Space]
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private string[] needItemNames;

    private bool[] isItemOnceInInventory;
    private bool[] isItemUsed;

    private void Start()
    {
        isItemOnceInInventory = new bool[needItemNames.Length];
        isItemUsed = new bool[needItemNames.Length];

        for (int i = 0; i < needItemNames.Length; i++)
        {
            isItemOnceInInventory[i] = false;
            isItemUsed[i] = false;
        }
    }

    private void Update()
    {
        for (int i = 0; i < needItemNames.Length; i++)
        {
            if (IsItemExist(needItemNames[i]) == false)
            {
                break;
            }

            isItemOnceInInventory[i] = true;
        }

        for (int i = 0; i < needItemNames.Length; i++)
        {
            if (isItemOnceInInventory[i] == true)
            {
                if (IsItemExist(needItemNames[i]) == true)
                {
                    break;
                }

                isItemUsed[i] = true;
            }
        }

        bool isItemAllUsed = false;

        for (int i = 0; i < needItemNames.Length; i++)
        {
            if (isItemUsed[i] == false)
            {
                break;
            }

            isItemAllUsed = true;
        }

        if (isItemAllUsed == true)
        {
            basementDoor.SetActive(true);

            foreach (GameObject obj in removeObjects)
            {
                Destroy(obj.gameObject);
            }
        }
    }

    // 필요한 아이템이 인벤토리에 있는지 확인
    private bool IsItemExist(string itemName)
    {
        if (itemName == "None") return true;

        bool isExist = false;
        Dictionary<int, InventoryItem> inventoryItems = inventoryData.GetCurrentInventoryState();

        foreach (KeyValuePair<int, InventoryItem> item in inventoryItems)
        {
            if (item.Value.item.DisplayName == itemName)
            {
                isExist = true;
                break;
            }
        }

        return isExist;
    }
}
