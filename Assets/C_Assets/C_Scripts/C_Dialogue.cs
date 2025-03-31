using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel; // 대사창 (Panel)
    public Text dialogueText; // 대사 내용 (레거시 UI Text)

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

    private List<TouchTrigger> interactableObjects = new List<TouchTrigger>(); // Interactable 스크립트를 가진 오브젝트들

    void Start()
    {
        dialoguePanel.SetActive(false); // 게임 시작 시 대사창 표시
        dialogueText.text = dialogues[index]; // 첫 번째 대사 출력

        // 상호작용할 오브젝트들을 비활성화
        DisableInteractableObjects();
    }

    void Update()
    {
        // 마우스 왼쪽 클릭하면 다음 대사 출력
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
            dialogueText.text = dialogues[index]; // 다음 대사 출력
        }
        else
        {
            dialoguePanel.SetActive(false); // 모든 대사가 끝나면 대사창 숨김

            // 대사가 끝난 후 상호작용 가능한 오브젝트 다시 활성화
            EnableInteractableObjects();
        }
    }

    // Interactable 컴포넌트를 가진 오브젝트들을 비활성화하는 함수
    private void DisableInteractableObjects()
    {
        interactableObjects.Clear();
        foreach (TouchTrigger TouchTrigger in FindObjectsOfType<TouchTrigger>())
        {
            TouchTrigger.gameObject.SetActive(false); // 오브젝트 비활성화
            interactableObjects.Add(TouchTrigger); // 비활성화한 오브젝트를 리스트에 저장
        }
    }

    // Interactable 컴포넌트를 가진 오브젝트들을 다시 활성화하는 함수
    private void EnableInteractableObjects()
    {
        foreach (TouchTrigger TouchTrigger in interactableObjects)
        {
            TouchTrigger.gameObject.SetActive(true); // 오브젝트 활성화
        }
        interactableObjects.Clear(); // 리스트 초기화
    }
}