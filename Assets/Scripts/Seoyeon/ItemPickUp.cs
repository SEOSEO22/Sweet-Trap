using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [field: SerializeField] public ItemSO InventoryItem { get; private set; }
    [field: SerializeField] public InventorySO InventoryData { get; private set; }

    // �������� �κ��丮�� �����ϱ� ���� ������ �����Ǿ����� �Ǵ�
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
