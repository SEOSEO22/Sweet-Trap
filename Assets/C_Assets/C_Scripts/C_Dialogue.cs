using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel; // ���â (Panel)
    public Text dialogueText; // ��� ���� (���Ž� UI Text)

    public float triggerX; // ��簡 ���۵� X��ǥ
    public float triggerThreshold = 0.5f; // ���� ����

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
    private bool isDialogueStarted = false; // ��簡 ���۵Ǿ����� üũ
    private List<TouchTrigger> interactableObjects = new List<TouchTrigger>();

    void Start()
    {
        dialoguePanel.SetActive(false);
        DisableInteractableObjects();
    }

    void Update()
    {
        if (!isDialogueStarted && Mathf.Abs(Camera.main.transform.position.x - triggerX) < triggerThreshold)
        {
            StartDialogue();
        }

        if (isDialogueStarted && Input.GetMouseButtonDown(0))
        {
            NextDialogue();
        }
    }

    public void StartDialogue()
    {
        if (isDialogueStarted) return;

        isDialogueStarted = true;
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogues[index];
    }

    public void NextDialogue()
    {
        index++;

        if (index < dialogues.Length)
        {
            dialogueText.text = dialogues[index];
        }
        else
        {
            dialoguePanel.SetActive(false);
            EnableInteractableObjects();
        }
    }

    private void DisableInteractableObjects()
    {
        interactableObjects.Clear();
        foreach (TouchTrigger touchTrigger in FindObjectsOfType<TouchTrigger>())
        {
            touchTrigger.gameObject.SetActive(false);
            interactableObjects.Add(touchTrigger);
        }
    }

    private void EnableInteractableObjects()
    {
        foreach (TouchTrigger touchTrigger in interactableObjects)
        {
            touchTrigger.gameObject.SetActive(true);
        }
        interactableObjects.Clear();
    }
}
