using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTrigger : MonoBehaviour
{
    public C_ZoomIn cameraZoom; // 카메라 줌인 관리
    private bool isLockOpened = false; // 자물쇠 해제 여부
    private C_Antidote antidoteScript; // 해독제 스크립트 참조
    [SerializeField] private InventorySO inventoryData;

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

    public string[] UnlockedDialogues = {
        "자물쇠가 열렸어...",
        "이제 밖으로 나갈 수 있을까?",
        "하지만 몸이 이래서 ... 해독제같은게 있을텐데",
        "dddd"
    };

    public string[] FrogDialogues = {
        "......",
        "......",
    };

    public string[] SnakeDialogues = {
        "......",
        "......",
    };

    public string[] ExitDialogues = {
        "......",
        "......",
    };

    public string[] PaperDialogues = {
        "......",
        "......",
    };

    public string[] Picture2Dialogues = {
        "......",
        "......",
    };

    public string[] SolutionDialogues = {
        "......",
        "......",
    };

    public string[] LadderDialogues = {
        "......",
        "......",
    };

    void Start()
    {
        antidoteScript = FindObjectOfType<C_Antidote>(); // 해독제 스크립트 찾기
    }

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
            else if (gameObject.CompareTag("Frog"))
            {
                C_DialogueManager.Instance.ShowDialogue(FrogDialogues);
            }
            else if (gameObject.CompareTag("Snake"))
            {
                C_DialogueManager.Instance.ShowDialogue(SnakeDialogues);
            }
            else if (gameObject.CompareTag("Exit"))
            {
                C_DialogueManager.Instance.ShowDialogue(ExitDialogues);
            }
            else if (gameObject.CompareTag("Paper"))
            {
                C_DialogueManager.Instance.ShowDialogue(PaperDialogues);
            }
            else if (gameObject.CompareTag("Picture2"))
            {
                C_DialogueManager.Instance.ShowDialogue(Picture2Dialogues);
            }
            else if (gameObject.CompareTag("Solution"))
            {
                C_DialogueManager.Instance.ShowDialogue(SolutionDialogues);
            }
            else if (gameObject.CompareTag("CookieSister"))
            {
                if (isLockOpened)
                {
                    C_DialogueManager.Instance.ShowDialogue(UnlockedDialogues);
                }
                else
                {
                    C_DialogueManager.Instance.ShowDialogue(CookieSisterDialogues);
                }
            }
            else if (gameObject.CompareTag("Ladder")) // 사다리 클릭 시 해독제 보유 여부 확인
            {
                if (IsItemExist("해독제"))
                {
                    //  SceneManager.LoadScene("EndingScene"); // 엔딩 씬으로 이동
                    Debug.Log("탈출!");
                }
                else
                {
                    C_DialogueManager.Instance.ShowDialogue(LadderDialogues);
                }
            }
        }
    }

    public void SetLockOpened()
    {
        isLockOpened = true;
    }

    private bool IsItemExist(string itemName)
    {
        if (itemName == "None") return true;

        Dictionary<int, InventoryItem> inventoryItems = inventoryData.GetCurrentInventoryState();

        foreach (KeyValuePair<int, InventoryItem> item in inventoryItems)
        {
            if (item.Value.item.DisplayName == itemName)
            {
                return true;
            }
        }

        return false;
    }
}
