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
    public GameObject gem2; 

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
                    EnableInteractableObjects();
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
            EnableInteractableObjects();
            background.SetActive(false);
            if (background != null)
            {
                EnableInteractableObjects();
                gem2.SetActive(true);

            }
        }

        // ������Ʈ�� ���� Ȱ��ȭ�� �� `SetLockOpened()` ����
        EnableInteractableObjects();

        // ��� TouchTrigger ������Ʈ ã�Ƽ� `SetLockOpened()` ȣ��
        foreach (TouchTrigger touchTrigger in FindObjectsOfType<TouchTrigger>())
        {
            touchTrigger.SetLockOpened();
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
