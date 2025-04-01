using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMirrorEvent : MonoBehaviour
{
    [SerializeField] private GameObject let3;
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private string requiredItem = "letter1";
    [SerializeField] private Text dialogueText;

    private Collider[] colliders;
    private bool isActivated = false;

    private void Start()
    {
        if (let3 != null)
            let3.SetActive(false);

        if (let3 != null)
        {
            colliders = let3.GetComponentsInChildren<Collider>();
        }
    }

    public void andDrop(string itemName)
    {
        if (isActivated || itemName != requiredItem)
            return;

        if (IsItemExist(requiredItem))
        {
            let3?.SetActive(true);
            isActivated = true;
        }
    }

    private bool IsItemExist(string itemName)
    {
        Dictionary<int, InventoryItem> inventoryItems = inventoryData.GetCurrentInventoryState();

        foreach (KeyValuePair<int, InventoryItem> item in inventoryItems)
        {
            if (item.Value.item.DisplayName == itemName)
            {
                return true;
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated) return;

        if (other == colliders[0])
        {
            UpdateText("당신을 좋아해요!\n저는 매 시간 노래를 부르고, 이리저리 잘 뛰어다닙니다.");
        }
        else if (other == colliders[1])
        {
            UpdateText("저는 말이 많아요.\n한 시도 지루하지 않을 거예요.\n명상도 하루에 한 시간씩 꼭 한답니다.");
        }
        else if (other == colliders[2])
        {
            UpdateText("불의를 보면 못 참는 정의로운 성격이에요.\n평소엔 유령처럼 다녀서 거슬리지 않을 거예요.");
        }
    }

    private void UpdateText(string newText)
    {
        if (dialogueText != null)
        {
            dialogueText.text = newText;
        }
    }
}
