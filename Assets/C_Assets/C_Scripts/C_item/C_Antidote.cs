using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �߰�

public class AntidoteActivator : MonoBehaviour, ITarget
{
    [SerializeField] private GameObject antidote; // �ص��� ������Ʈ
   // [SerializeField] private string failureSceneName = "FailureScene"; // ���� �� �̵��� �� �̸�

    private string[] correctOrder = { "�ʷ�ĵ��", "�Ķ�ĵ��", "���󹰾�", "��������" }; // �ùٸ� ����
    private int currentIndex = 0; // ���� �Էµ� ���� �ε���

    private void Start()
    {
        antidote.SetActive(false); // ���� �� �ص��� ��Ȱ��ȭ
    }

    public void UseItemAction(string itemName)
    {
        Debug.Log($"�Էµ� ������: {itemName}, ��밪: {correctOrder[currentIndex]}");

        // �Էµ� �������� ������ ������ ��ġ�ϴ��� Ȯ��
        if (itemName == correctOrder[currentIndex])
        {
            currentIndex++; // ���� ������ �̵�
            Debug.Log($"�ùٸ� ������! ���� �ε���: {currentIndex}");

            // ��� �������� �ùٸ� ������ ����ϸ� �ص��� Ȱ��ȭ
            if (currentIndex >= correctOrder.Length)
            {
                ActivateAntidote();
            }
        }
        else
        {
            Debug.Log("������ Ʋ�Ƚ��ϴ�. ���� ������ �̵��մϴ�.");
         //   SceneManager.LoadScene(failureSceneName); // ���� �� �ε�
        }
    }

    private void ActivateAntidote()
    {
        Debug.Log("�ص����� Ȱ��ȭ�Ǿ����ϴ�!");
        antidote.SetActive(true); // �ص��� Ȱ��ȭ
    }
}
