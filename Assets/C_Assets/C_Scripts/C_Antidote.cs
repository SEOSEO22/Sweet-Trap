using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �߰�

public class C_Antidote : MonoBehaviour, ITarget
{
    [SerializeField] private GameObject antidote; // �ص��� ������Ʈ

    private string[] correctOrder = { "�ʷ�ĵ��", "�Ķ�ĵ��", "���󹰾�", "��������" }; // �ùٸ� ����
    private int currentIndex = 0; // ���� �Էµ� ���� �ε���
    private bool hasAntidote = false; // �ص��� ���� ����

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
            SceneManager.LoadScene("FailureScene"); // ���� �� �ε�
        }
    }

    private void ActivateAntidote()
    {
        antidote.SetActive(true); // �ص��� Ȱ��ȭ
        hasAntidote = true; // �ص��� ���� ���� ����
        Debug.Log("�ص����� ȹ���߽��ϴ�!");
    }

    public bool GetHasAntidote()
    {
        return hasAntidote; // �ص��� ���� ���� ��ȯ
    }
}
