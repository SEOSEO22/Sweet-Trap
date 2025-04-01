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
            UpdateText("����� �����ؿ�!\n���� �� �ð� �뷡�� �θ���, �̸����� �� �پ�ٴմϴ�.");
        }
        else if (other == colliders[1])
        {
            UpdateText("���� ���� ���ƿ�.\n�� �õ� �������� ���� �ſ���.\n��� �Ϸ翡 �� �ð��� �� �Ѵ�ϴ�.");
        }
        else if (other == colliders[2])
        {
            UpdateText("���Ǹ� ���� �� ���� ���Ƿο� �����̿���.\n��ҿ� ����ó�� �ٳ༭ �Ž����� ���� �ſ���.");
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
