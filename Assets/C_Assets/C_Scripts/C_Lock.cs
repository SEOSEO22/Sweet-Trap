using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Lock : MonoBehaviour
{
    public GameObject lockPanel;  // �ڹ��� UI �г�
    public Text lockText;         // �Է� ǥ�� �ؽ�Ʈ (���Ž� UI Text)
    private string correctCode = "3721"; // ���� �ڵ�
    private string currentInput = "";    // ���� �Էµ� ����
    public GameObject background; // ���� ��� ������Ʈ

    private List<TouchTrigger> interactableObjects = new List<TouchTrigger>();

    void Start()
    {
        lockPanel.SetActive(false); // ó������ �ڹ��� UI ����
    }

    void Update()
    {
        if (lockPanel.activeSelf) // �ڹ��� UI�� Ȱ��ȭ�� ��쿡�� �Է� ó��
        {
            foreach (char c in Input.inputString)
            {
                if (char.IsDigit(c) && currentInput.Length < 4)
                {
                    currentInput += c; // ���� �Է�
                    UpdateLockText();
                }
            }

            if (Input.GetKeyDown(KeyCode.Backspace) && currentInput.Length > 0)
            {
                currentInput = currentInput.Substring(0, currentInput.Length - 1); // ������ �Է� ����
                UpdateLockText();
            }

            if (currentInput.Length == 4) // 4�ڸ� �Է��� �Ϸ�Ǿ��� �� ó��
            {
                EnableInteractableObjects();
                if (currentInput == correctCode)
                {

                    Unlock(); // �����̸� �ڹ��� ����
                }
                else
                {
                    StartCoroutine(WrongCode()); // Ʋ���� �г��� ����
                }
            }
        }
    }

    void UpdateLockText()
    {
        // �Էµ� ���� ǥ�� + ���� �ڸ��� '-'
        lockText.text = currentInput.PadRight(4, '-');
    }

    void Unlock()
    {
        lockPanel.SetActive(false);

        if (background != null)
        {
            background.SetActive(false);
        }

        // ������Ʈ�� ���� Ȱ��ȭ�� �� `SetLockOpened()` ����
        EnableInteractableObjects();

        // TouchTrigger�� �����ϴ��� Ȯ�� �� ����
        TouchTrigger cookieSister = FindObjectOfType<TouchTrigger>();
        if (cookieSister != null)
        {
            cookieSister.SetLockOpened();
        }
    }

    IEnumerator WrongCode()
    {
        lockText.text = "�̰� �ƴѰ���"; // Ʋ���� �� �޽��� ǥ��
        yield return new WaitForSeconds(1.5f); // 1.5�� �� �г� �ݱ�
        lockPanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        lockPanel.SetActive(true); // �ڹ��� Ŭ�� �� UI Ȱ��ȭ
        DisableInteractableObjects();
        currentInput = ""; // �Է� �ʱ�ȭ
        UpdateLockText();
    }

    // Interactable ������Ʈ�� ���� ������Ʈ���� ��Ȱ��ȭ�ϴ� �Լ�
    private void DisableInteractableObjects()
    {
        interactableObjects.Clear();
        foreach (TouchTrigger TouchTrigger in FindObjectsOfType<TouchTrigger>())
        {
            TouchTrigger.gameObject.SetActive(false); // ������Ʈ ��Ȱ��ȭ
            interactableObjects.Add(TouchTrigger); // ��Ȱ��ȭ�� ������Ʈ�� ����Ʈ�� ����
        }
    }

    // Interactable ������Ʈ�� ���� ������Ʈ���� �ٽ� Ȱ��ȭ�ϴ� �Լ�
    private void EnableInteractableObjects()
    {
        foreach (TouchTrigger TouchTrigger in interactableObjects)
        {
            TouchTrigger.gameObject.SetActive(true); // ������Ʈ Ȱ��ȭ
        }
        interactableObjects.Clear(); // ����Ʈ �ʱ�ȭ
    }
}