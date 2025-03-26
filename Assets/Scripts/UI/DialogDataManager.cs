using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogDataManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;
    
    private GameObject scanObject; // ��ȣ�ۿ��� ������ ������Ʈ
    private Dictionary<int, string[]> dialogData;   // �����ۺ� ��ȣ�ۿ� ��縦 �����ϴ� Dictionary

    private void Awake()
    {
        dialogData = new Dictionary<int, string[]>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider.gameObject.GetComponent<DialogData>())
            {
                SetDialog(hit.collider.gameObject);
            }
        }
    }

    // ���â UI�� ��� ����
    public void SetDialog(GameObject scanObj)
    {
        scanObject = scanObj;

        DialogData dialogData = scanObj.GetComponent<DialogData>();
        dialogText.text = GetDialogData(dialogData.id, dialogData.currentDialogIdx);
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
