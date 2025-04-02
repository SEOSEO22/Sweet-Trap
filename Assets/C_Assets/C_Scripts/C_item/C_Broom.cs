using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Broom : MonoBehaviour, ITarget
{
    [SerializeField] private GameObject[] removeObjects; // ���� ������Ʈ��
    [SerializeField] private GameObject gem; // ���� ������Ʈ

    [Space]
    [SerializeField] private string[] needItemNames; // �ʿ��� �����۵�

    [SerializeField]
    private bool[] isItemUsed;

    private void Start()
    {
        isItemUsed = new bool[needItemNames.Length];

        for (int i = 0; i < needItemNames.Length; i++)
        {
            isItemUsed[i] = false;
        }

        // ������ ��Ȱ��ȭ ���·� ����
        if (gem != null)
        {
            gem.SetActive(false);
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

        if (isItemAllUsed)
        {
            
            // ���� Ȱ��ȭ
            if (gem != null)
            {
                gem.SetActive(true);
            }

            foreach (GameObject obj in removeObjects)
            {
                obj.SetActive(false);
            }
        }
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
            }
        }
    }
}

