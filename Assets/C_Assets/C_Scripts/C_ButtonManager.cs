using UnityEngine;
using UnityEngine.UI;

public class ScreenButtonManager : MonoBehaviour
{
    public Button leftButton;  // ���� �̵� ��ư
    public Button rightButton; // ������ �̵� ��ư

    private bool isFirstClick = true; // ������ ��ư�� ó�� ���ȴ��� Ȯ��

    void Start()
    {
        if (leftButton != null)
            leftButton.interactable = false; // ó������ ���� ��ư ��Ȱ��ȭ

        if (rightButton != null)
            rightButton.onClick.AddListener(EnableLeftButton); // ������ ��ư Ŭ�� �̺�Ʈ ���
    }

    void EnableLeftButton()
    {
        if (isFirstClick && leftButton != null)
        {
            leftButton.interactable = true; // ���� ��ư Ȱ��ȭ
            isFirstClick = false; // ���Ŀ��� ��� Ȱ��ȭ ���� ����
        }
    }
}
