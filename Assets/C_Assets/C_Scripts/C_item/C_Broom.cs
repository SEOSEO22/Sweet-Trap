using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Broom : MonoBehaviour, ITarget
{
    [SerializeField] private GameObject[] removeObjects; // 먼지 오브젝트들
    [SerializeField] private GameObject gem; // 보석 오브젝트

    [Space]
    [SerializeField] private string[] needItemNames; // 필요한 아이템들

    [SerializeField]
    private bool[] isItemUsed;

    private void Start()
    {
        isItemUsed = new bool[needItemNames.Length];

        for (int i = 0; i < needItemNames.Length; i++)
        {
            isItemUsed[i] = false;
        }

        // 보석을 비활성화 상태로 시작
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
            
            // 보석 활성화
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

