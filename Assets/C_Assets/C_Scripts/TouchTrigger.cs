using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTrigger : MonoBehaviour
{
    public C_ZoomIn cameraZoom; // 카메라 줌인 관리

    // 각 오브젝트에 해당하는 대사 배열
    public string[] TaskDialogues = {
        "이곳은 첫 번째 방이다.",
        "뭔가 이상한 기운을 느낄 수 있다...",
        "책상이다"
    };

    public string[] MBottleDialogues = {
        "여기에는 오래된 책이 있다.",
        "이 책에서 무엇을 배울 수 있을까?"
    };

    public string[] CookieSisterDialogues = {
        "......",
        "......",

    };
    public string[] FrogDialogues = {
        "......",
        "......",

    };
    public string[] SnakeDialogues = {
        "......",
        "......",

    };
    public string[] PaperDialogues = {
        "......",
        "......",

    };
    public string[] ExitDialogues = {
        "......",
        "......",

    };

    public string[] LockAnswerDialogues = {
        "......",
        "......",

    };

    private bool isLockOpened = false; // 자물쇠 해제 여부

    // 터치한 오브젝트에 따라 대사 다르게 설정
    void OnMouseDown()
    {
        if (cameraZoom != null)
        {
            Vector3 targetPosition = transform.position;
            targetPosition.z = Camera.main.transform.position.z;
            cameraZoom.ZoomIn(targetPosition);

            if (gameObject.CompareTag("Task"))
            {
                C_DialogueManager.Instance.ShowDialogue(TaskDialogues);
            }
            else if (gameObject.CompareTag("MBottle"))
            {
                C_DialogueManager.Instance.ShowDialogue(MBottleDialogues);
            }
            else if (gameObject.CompareTag("CookieSister"))
            {
                if (isLockOpened)
                {
                    C_DialogueManager.Instance.ShowDialogue(new string[] {
                    "자물쇠가 열렸어...",
                    "이제 밖으로 나갈 수 있을까?",
                    "하지만 몸이 이래서 ... 해독제같은게 있을텐데",
                    "dddd"
                    });
                }
                else
                {
                    C_DialogueManager.Instance.ShowDialogue(CookieSisterDialogues);
                }
            }
            else if (gameObject.CompareTag("Frog"))
            {
                C_DialogueManager.Instance.ShowDialogue(FrogDialogues);
            }
            else if (gameObject.CompareTag("Snake"))
            {
                C_DialogueManager.Instance.ShowDialogue(SnakeDialogues);
            }
            else if (gameObject.CompareTag("Paper"))
            {
                C_DialogueManager.Instance.ShowDialogue(PaperDialogues);
            }
            else if (gameObject.CompareTag("Exit"))
            {
                C_DialogueManager.Instance.ShowDialogue(ExitDialogues);
            }
            else if (gameObject.CompareTag("LockAnswer"))
            {
                C_DialogueManager.Instance.ShowDialogue(LockAnswerDialogues);
            }
        }
    }
    public void SetLockOpened()
    {
        isLockOpened = true;
    }
}