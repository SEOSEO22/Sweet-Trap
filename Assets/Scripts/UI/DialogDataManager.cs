using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogDataManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;
    
    private GameObject scanObject; // 상호작용할 아이템 오브젝트
    private Dictionary<int, string[]> dialogData;   // 아이템별 상호작용 대사를 저장하는 Dictionary

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

    // 대사창 UI에 대사 설정
    public void SetDialog(GameObject scanObj)
    {
        scanObject = scanObj;

        DialogData dialogData = scanObj.GetComponent<DialogData>();
        dialogText.text = GetDialogData(dialogData.id, dialogData.currentDialogIdx);
    }

    // Dictionary에 대사 데이터 추가
    public void AddDialogData(int id, string[] dialogText)
    {
        dialogData.Add(id, dialogText);
    }

    public string GetDialogData(int id, int dialogIndex)
    {
        return dialogData[id][dialogIndex];
    }
}
