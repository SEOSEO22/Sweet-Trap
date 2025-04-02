using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTrigger : MonoBehaviour
{
    public C_ZoomIn cameraZoom; // ī�޶� ���� ����
    private bool isLockOpened = false; // �ڹ��� ���� ����
    private C_Antidote antidoteScript; // �ص��� ��ũ��Ʈ ����
    [SerializeField] private InventorySO inventoryData;

    // �� ������Ʈ�� �ش��ϴ� ��� �迭
    public string[] TaskDialogues = {
        "�̰��� ù ��° ���̴�.",
        "���� �̻��� ����� ���� �� �ִ�...",
        "å���̴�"
    };

    public string[] MBottleDialogues = {
        "���⿡�� ������ å�� �ִ�.",
        "�� å���� ������ ��� �� ������?"
    };

    public string[] CookieSisterDialogues = {
        "......",
        "......",
    };

    public string[] UnlockedDialogues = {
        "�ڹ��谡 ���Ⱦ�...",
        "���� ������ ���� �� ������?",
        "������ ���� �̷��� ... �ص��������� �����ٵ�",
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
        antidoteScript = FindObjectOfType<C_Antidote>(); // �ص��� ��ũ��Ʈ ã��
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
            else if (gameObject.CompareTag("Ladder")) // ��ٸ� Ŭ�� �� �ص��� ���� ���� Ȯ��
            {
                if (IsItemExist("�ص���"))
                {
                    //  SceneManager.LoadScene("EndingScene"); // ���� ������ �̵�
                    Debug.Log("Ż��!");
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
