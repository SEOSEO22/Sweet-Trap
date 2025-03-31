using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_DialogueManager : MonoBehaviour
{
    public static C_DialogueManager Instance;
    public Text dialogueText;
    public GameObject dialoguePanel;
    public GameObject background; // ���� ��� ������Ʈ

    private Queue<string> dialogueQueue = new Queue<string>(); // ��� ���

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dialoguePanel.SetActive(false); // ó���� ��Ȱ��ȭ
    }

    public void ShowDialogue(string[] dialogues)
    {
        dialogueQueue.Clear(); // ���� ��� ����
        foreach (string dialogue in dialogues)
        {
            dialogueQueue.Enqueue(dialogue); // ���ο� ��� �߰�
        }

        dialoguePanel.SetActive(true); // �г� Ȱ��ȭ
        DisplayNextDialogue(); // ù ��° ��� ���
    }

    public void DisplayNextDialogue()
    {
        if (dialogueQueue.Count > 0)
        {
            dialogueText.text = dialogueQueue.Dequeue(); // ��� ��� ����
        }
        else
        {
            HideDialogue(); // ������ ����� ��� â �ݱ�
        }
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false); // �г� ����
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialoguePanel.activeSelf) // ���콺 Ŭ�� ����
        {
            DisplayNextDialogue();
        }
    }
}
