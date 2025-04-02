using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Lock : MonoBehaviour
{
    public GameObject lockPanel;  // 자물쇠 UI 패널
    public Text lockText;         // 입력 표시 텍스트 (레거시 UI Text)
    private string correctCode = "3721"; // 정답 코드
    private string currentInput = "";    // 현재 입력된 숫자
    public GameObject background; // 기존 배경 오브젝트
    public GameObject gem2; 

    private List<TouchTrigger> interactableObjects = new List<TouchTrigger>();

    void Start()
    {
        lockPanel.SetActive(false); // 처음에는 자물쇠 UI 숨김
    }

    void Update()
    {
        if (lockPanel.activeSelf) // 자물쇠 UI가 활성화된 경우에만 입력 처리
        {
            foreach (char c in Input.inputString)
            {
                if (char.IsDigit(c) && currentInput.Length < 4)
                {
                    currentInput += c; // 숫자 입력
                    UpdateLockText();
                }
            }

            if (Input.GetKeyDown(KeyCode.Backspace) && currentInput.Length > 0)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1); // 마지막 입력 삭제
                UpdateLockText();
            }

            if (currentInput.Length == 4) // 4자리 입력이 완료되었을 때 처리
            {
                EnableInteractableObjects();

                if (currentInput == correctCode)
                {
                    Unlock(); // 정답이면 자물쇠 해제
                    EnableInteractableObjects();
                }
                else
                {
                    StartCoroutine(WrongCode()); // 틀리면 패널을 닫음
                }
            }
        }
    }

    void UpdateLockText()
    {
        // 입력된 숫자 표시 + 남은 자리는 '-'
        lockText.text = currentInput.PadRight(4, '-');
    }

    void Unlock()
    {
        lockPanel.SetActive(false);

        if (background != null)
        {
            EnableInteractableObjects();
            background.SetActive(false);
            if (background != null)
            {
                EnableInteractableObjects();
                gem2.SetActive(true);

            }
        }

        // 오브젝트를 먼저 활성화한 후 `SetLockOpened()` 실행
        EnableInteractableObjects();

        // 모든 TouchTrigger 오브젝트 찾아서 `SetLockOpened()` 호출
        foreach (TouchTrigger touchTrigger in FindObjectsOfType<TouchTrigger>())
        {
            touchTrigger.SetLockOpened();
        }
    }

    IEnumerator WrongCode()
    {
        lockText.text = "이게 아닌가봐"; // 틀렸을 때 메시지 표시
        yield return new WaitForSeconds(1.5f); // 1.5초 후 패널 닫기
        lockPanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        lockPanel.SetActive(true); // 자물쇠 클릭 시 UI 활성화
        DisableInteractableObjects();
        currentInput = ""; // 입력 초기화
        UpdateLockText();
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
