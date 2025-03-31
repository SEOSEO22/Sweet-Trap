using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTrigger : MonoBehaviour
{
    public C_ZoomIn cameraZoom; // ī�޶� ���� ����

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

    private bool isLockOpened = false; // �ڹ��� ���� ����

    // ��ġ�� ������Ʈ�� ���� ��� �ٸ��� ����
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
                    "�ڹ��谡 ���Ⱦ�...",
                    "���� ������ ���� �� ������?",
                    "������ ���� �̷��� ... �ص��������� �����ٵ�",
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