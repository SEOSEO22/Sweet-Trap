using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogDataManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;
    
    private GameObject scanObject; // 상호작용할 아이템 오브젝트
    private Dictionary<int, string[]> dialogData;   // 아이템별 상호작용 대사를 저장하는 Dictionary
    public bool isDialogPanelActive { get; private set; } = false;   // 대사 패널의 활성화 여부 확인

    private void Awake()
    {
        dialogData = new Dictionary<int, string[]>();
        InitDialog(); 
    }

    private void Update()
    {
        UpdateDialog();
    }

    // 대사창 게임 오브젝트 비활성화 및 대사 초기화
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

            // 클릭한 오브젝트가 상호작용이 가능할 경우
            if (!isDialogPanelActive && hit.collider && hit.collider.gameObject.GetComponent<DialogData>())
            {
                InitDialog();   // 대사창 초기화
                SetDialog(hit.collider.gameObject); // 재생할 대사 설정
            }
            else if (isDialogPanelActive) // 대사가 남아있을 경우 대사 재생
            {
                SetDialog(scanObject);
            }
        }
    }

    // 대사창 UI에 대사 설정
    public void SetDialog(GameObject scanObj)
    {
        scanObject = scanObj;

        DialogData dialogData = scanObject.GetComponent<DialogData>();
        int dialogIndex = dialogData.GetCurrentDialogIdx();

        // 대사가 없을 경우 대사창 비활성화
        if (dialogIndex == -1)
        {
            InitDialog();
            return;
        }

        dialogText.text = GetDialogData(dialogData.id, dialogIndex);
        dialogPanel.SetActive(true);
        isDialogPanelActive = true;
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
