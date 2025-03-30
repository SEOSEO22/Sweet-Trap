using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogDataManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;
    
    private GameObject scanObject; // ��ȣ�ۿ��� ������ ������Ʈ
    private Dictionary<int, string[]> dialogData;   // �����ۺ� ��ȣ�ۿ� ��縦 �����ϴ� Dictionary
    public bool isDialogPanelActive { get; private set; } = false;   // ��� �г��� Ȱ��ȭ ���� Ȯ��

    private void Awake()
    {
        dialogData = new Dictionary<int, string[]>();
        InitDialog(); 
    }

    private void Update()
    {
        UpdateDialog();
    }

    // ���â ���� ������Ʈ ��Ȱ��ȭ �� ��� �ʱ�ȭ
    private void InitDialog()
    {
        dialogPanel.SetActive(false);
        isDialogPanelActive = false;
        dialogText.text = "";
    }

    public void UpdateDialog()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // Ŭ���� ������Ʈ�� ��ȣ�ۿ��� ������ ���
            if (!isDialogPanelActive && hit.collider && hit.collider.gameObject.GetComponent<DialogData>())
            {
                InitDialog();   // ���â �ʱ�ȭ
                SetDialog(hit.collider.gameObject); // ����� ��� ����
            }
            else if (isDialogPanelActive) // ��簡 �������� ��� ��� ���
            {
                SetDialog(scanObject);
            }
        }
    }

    // ���â UI�� ��� ����
    public void SetDialog(GameObject scanObj)
    {
        scanObject = scanObj;

        DialogData dialogData = scanObject.GetComponent<DialogData>();
        int dialogIndex = dialogData.GetCurrentDialogIdx();

        // ��簡 ���� ��� ���â ��Ȱ��ȭ
        if (dialogIndex == -1)
        {
            InitDialog();
            return;
        }

        dialogText.text = GetDialogData(dialogData.id, dialogIndex);
        dialogPanel.SetActive(true);
        isDialogPanelActive = true;
    }

    // Dictionary�� ��� ������ �߰�
    public void AddDialogData(int id, string[] dialogText)
    {
        dialogData.Add(id, dialogText);
    }

    public string GetDialogData(int id, int dialogIndex)
    {
        return dialogData[id][dialogIndex];
    }
}
