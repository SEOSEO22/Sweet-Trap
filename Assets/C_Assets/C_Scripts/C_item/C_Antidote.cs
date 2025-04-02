using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 추가

public class AntidoteActivator : MonoBehaviour, ITarget
{
    [SerializeField] private GameObject antidote; // 해독제 오브젝트
   // [SerializeField] private string failureSceneName = "FailureScene"; // 실패 시 이동할 씬 이름

    private string[] correctOrder = { "초록캔디", "파랑캔디", "보라물약", "빨강물약" }; // 올바른 순서
    private int currentIndex = 0; // 현재 입력된 순서 인덱스

    private void Start()
    {
        antidote.SetActive(false); // 시작 시 해독제 비활성화
    }

    public void UseItemAction(string itemName)
    {
        Debug.Log($"입력된 아이템: {itemName}, 기대값: {correctOrder[currentIndex]}");

        // 입력된 아이템이 정해진 순서와 일치하는지 확인
        if (itemName == correctOrder[currentIndex])
        {
            currentIndex++; // 다음 순서로 이동
            Debug.Log($"올바른 아이템! 현재 인덱스: {currentIndex}");

            // 모든 아이템을 올바른 순서로 사용하면 해독제 활성화
            if (currentIndex >= correctOrder.Length)
            {
                ActivateAntidote();
            }
        }
        else
        {
            Debug.Log("순서가 틀렸습니다. 실패 씬으로 이동합니다.");
         //   SceneManager.LoadScene(failureSceneName); // 실패 씬 로드
        }
    }

    private void ActivateAntidote()
    {
        Debug.Log("해독제가 활성화되었습니다!");
        antidote.SetActive(true); // 해독제 활성화
    }
}
