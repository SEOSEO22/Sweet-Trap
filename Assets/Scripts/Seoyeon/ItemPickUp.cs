using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [field: SerializeField] public ItemSO InventoryItem { get; private set; }
    [field: SerializeField] public InventorySO InventoryData { get; private set; }

    // 아이템을 인벤토리에 저장하기 위한 조건이 충족되었는지 판단
    public bool isConditionMet = true;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.Icon;
    }

    private void OnMouseDown()
    {
        if (isConditionMet)
        {
            InventoryData.AddItem(InventoryItem);
            Destroy(gameObject);
        }
    }
}
