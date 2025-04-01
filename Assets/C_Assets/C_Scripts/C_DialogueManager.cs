using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_DialogueManager : MonoBehaviour
{
    public static C_DialogueManager Instance;
    public Text dialogueText;
    public GameObject dialoguePanel;
    public GameObject background; // 기존 배경 오브젝트

    private Queue<string> dialogueQueue = new Queue<string>(); // 대사 목록

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dialoguePanel.SetActive(false); // 처음엔 비활성화
    }

    public void ShowDialogue(string[] dialogues)
    {
        dialogueQueue.Clear(); // 기존 대사 제거
        foreach (string dialogue in dialogues)
        {
            dialogueQueue.Enqueue(dialogue); // 새로운 대사 추가
        }

        dialoguePanel.SetActive(true); // 패널 활성화
        DisplayNextDialogue(); // 첫 번째 대사 출력
    }

    public void DisplayNextDialogue()
    {
        if (dialogueQueue.Count > 0)
        {
            dialogueText.text = dialogueQueue.Dequeue(); // 대사 즉시 변경
        }
        else
        {
            HideDialogue(); // 마지막 대사일 경우 창 닫기
        }
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false); // 패널 숨김
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialoguePanel.activeSelf) // 마우스 클릭 감지
        {
            DisplayNextDialogue();
        }
    }
}
