using UnityEngine;
using UnityEngine.UI;

public class ScreenButtonManager : MonoBehaviour
{
    public Button leftButton;  // 왼쪽 이동 버튼
    public Button rightButton; // 오른쪽 이동 버튼

    private bool isFirstClick = true; // 오른쪽 버튼이 처음 눌렸는지 확인

    void Start()
    {
        if (leftButton != null)
            leftButton.interactable = false; // 처음에는 왼쪽 버튼 비활성화

        if (rightButton != null)
            rightButton.onClick.AddListener(EnableLeftButton); // 오른쪽 버튼 클릭 이벤트 등록
    }

    void EnableLeftButton()
    {
        if (isFirstClick && leftButton != null)
        {
            leftButton.interactable = true; // 왼쪽 버튼 활성화
            isFirstClick = false; // 이후에는 계속 활성화 상태 유지
        }
    }
}
