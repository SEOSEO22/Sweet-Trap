using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel; // 대사창 (Panel)
    public Text dialogueText; // 대사 내용 (레거시 UI Text)

    public float triggerX; // 대사가 시작될 X좌표
    public float triggerThreshold = 0.5f; // 오차 범위

    private string[] dialogues = {
        "무언가를 실험한건가 ..? ",
        "윽 ... 머리가 아파",
        "맞아 … 나는 분명 동생을 찾으러 돌아다니고있었는데…",
        "도중에 마녀에게 붙잡히고 말았어",
        "그럼 여긴 마녀의 집인가봐",
        "그럼 동생이 여기에 있을 수도 있겠어",
        "동생을 찾아서 여길 나가야겠어"
    };

    private int index = 0;
    private bool isDialogueStarted = false; // 대사가 시작되었는지 체크
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
