using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel; // ���â (Panel)
    public Text dialogueText; // ��� ���� (���Ž� UI Text)

    private string[] dialogues = {
        "���𰡸� �����Ѱǰ� ..? ",
        "�� ... �Ӹ��� ����",
        "�¾� �� ���� �и� ������ ã���� ���ƴٴϰ��־��µ���",
        "���߿� ���࿡�� �������� ���Ҿ�",
        "�׷� ���� ������ ���ΰ���",
        "�׷� ������ ���⿡ ���� ���� �ְھ�",
        "������ ã�Ƽ� ���� �����߰ھ�"
    };

    private int index = 0;

    private List<TouchTrigger> interactableObjects = new List<TouchTrigger>(); // Interactable ��ũ��Ʈ�� ���� ������Ʈ��

    void Start()
    {
        dialoguePanel.SetActive(false); // ���� ���� �� ���â ǥ��
        dialogueText.text = dialogues[index]; // ù ��° ��� ���

        // ��ȣ�ۿ��� ������Ʈ���� ��Ȱ��ȭ
        DisableInteractableObjects();
    }

    void Update()
    {
        // ���콺 ���� Ŭ���ϸ� ���� ��� ���
        if (Input.GetMouseButtonDown(0))
        {
            NextDialogue();
        }
    }

    public void NextDialogue()
    {
        index++;

        if (index < dialogues.Length)
        {
            dialogueText.text = dialogues[index]; // ���� ��� ���
        }
        else
        {
            dialoguePanel.SetActive(false); // ��� ��簡 ������ ���â ����

            // ��簡 ���� �� ��ȣ�ۿ� ������ ������Ʈ �ٽ� Ȱ��ȭ
            EnableInteractableObjects();
        }
    }

    // Interactable ������Ʈ�� ���� ������Ʈ���� ��Ȱ��ȭ�ϴ� �Լ�
    private void DisableInteractableObjects()
    {
        interactableObjects.Clear();
        foreach (TouchTrigger TouchTrigger in FindObjectsOfType<TouchTrigger>())
        {
            TouchTrigger.gameObject.SetActive(false); // ������Ʈ ��Ȱ��ȭ
            interactableObjects.Add(TouchTrigger); // ��Ȱ��ȭ�� ������Ʈ�� ����Ʈ�� ����
        }
    }

    // Interactable ������Ʈ�� ���� ������Ʈ���� �ٽ� Ȱ��ȭ�ϴ� �Լ�
    private void EnableInteractableObjects()
    {
        foreach (TouchTrigger TouchTrigger in interactableObjects)
        {
            TouchTrigger.gameObject.SetActive(true); // ������Ʈ Ȱ��ȭ
        }
        interactableObjects.Clear(); // ����Ʈ �ʱ�ȭ
    }
}