using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorLetter : MonoBehaviour, ITarget
{
    [SerializeField] private GameObject[] removeObjects;
    [SerializeField] public GameObject let3;

    [Space]
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private string[] needItemNames;

    [SerializeField]
    private bool[] isItemUsed;

    private void Start()
    {
        let3.SetActive(false);
        isItemUsed = new bool[needItemNames.Length];

        for (int i = 0; i < needItemNames.Length; i++)
        {
            isItemUsed[i] = false;
        }
    }

    private void Update()
    {
        bool isItemAllUsed = false;

        for (int i = 0; i < needItemNames.Length; i++)
        {
            if (isItemUsed[i] == false)
            {
                return;
            }

            isItemAllUsed = true;
        }

        if (isItemAllUsed == true)
        {

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

    public void UseItemAction(string itemName)
    {
        for (int i = 0; i < needItemNames.Length; i++)
        {
            Debug.Log(itemName);
            Debug.Log(needItemNames[i]);
            if (itemName == needItemNames[i])
            {
                isItemUsed[i] = true;
                let3.SetActive(true);
            }
           
        }
    }
}
