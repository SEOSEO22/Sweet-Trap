using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToBasement : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private string needItemName;

    private int needItemIndex;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (IsItemExist(needItemName) && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider && hit.collider.gameObject.name == this.name)
            {
                inventoryData.RemoveItem(needItemIndex);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
                needItemIndex = item.Key;
                isExist = true;
                break;
            }
        }

        return isExist;
    }
}
