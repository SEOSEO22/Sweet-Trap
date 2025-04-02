using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour, ITarget
{
    [SerializeField] private GameObject[] removeObjects;
    [SerializeField] private GameObject basementDoor;

    [Space]
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private string[] needItemNames;

    [SerializeField]
    private bool[] isItemUsed;

    private void Start()
    {
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
            basementDoor.SetActive(true);

            foreach (GameObject obj in removeObjects)
            {
                StartCoroutine(FadeOutAndDestroy(obj));
            }
        }
    }

    private IEnumerator FadeOutAndDestroy(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        Color startColor = sr.color;
        float duration = 1.0f; // 1초 동안 서서히 투명해짐
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            sr.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null; // 한 프레임 대기
        }

        Destroy(obj);
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
            if (itemName == needItemNames[i])
            {
                isItemUsed[i] = true;
            }
        }
    }
}
