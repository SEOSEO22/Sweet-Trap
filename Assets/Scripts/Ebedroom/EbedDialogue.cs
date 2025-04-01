using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class bedDialogue : MonoBehaviour
{
    public GameObject talk;
    public Text dialogueText;
    public string[] dialogues;
    private int clickCount = 0;
    private bool isDialogueActive = false;

    // 침대보 변경 이벤트
    public Sprite originalSprite;
    public Sprite changedSprite;
    private SpriteRenderer spriteRenderer;
    private bool isDragging = false;
    private Vector3 dragStartPosition;
    private bool hasChangedSprite = false;

    private void Start()
    {
        if (talk != null)
        {
            talk.SetActive(false);
            AddButtonToTalk();
        }

        // 스프라이트 가져오는 거
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = originalSprite;
    }

private void OnMouseDown()
    {
        if (!EventManager.Instance.IsAnyEventActive() && !isDialogueActive)
        {
            StartDialogue();
        }

        if (!hasChangedSprite)
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 dragDelta = Input.mousePosition - dragStartPosition;
            if (dragDelta.magnitude > 70f) // 드래그 거리가 50 픽셀 이상일 때
            {
                ChangeSprite();
                isDragging = false;
                hasChangedSprite = true;
            }
        }
    }
    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void ChangeSprite()
    {
        spriteRenderer.sprite = changedSprite;
    }

    private void StartDialogue()
    {
        EventManager.Instance.StartEvent("bedDialogue");
        isDialogueActive = true;
        if (talk != null)
        {
            talk.SetActive(true);
        }
        ShowNextDialogue();
    }

    public void ShowNextDialogue()
    {
        if (clickCount < dialogues.Length)
        {
            dialogueText.text = dialogues[clickCount];
            clickCount++;

            if (clickCount >= dialogues.Length)
            {
                Button dialogueButton = talk.GetComponent<Button>();
                dialogueButton.onClick.RemoveAllListeners();
                dialogueButton.onClick.AddListener(EndDialogue);
            }
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        if (talk != null)
        {
            talk.SetActive(false);
        }

        clickCount = 0;
        isDialogueActive = false;
        EventManager.Instance.EndEvent("bedDialogue");
    }


    private void AddButtonToTalk()
    {
        Button dialogueButton = talk.GetComponent<Button>();

        if (dialogueButton == null)
        {
            dialogueButton = talk.AddComponent<Button>();
        }

        dialogueButton.onClick.RemoveAllListeners();
        dialogueButton.onClick.AddListener(() => {
            if (clickCount < dialogues.Length)
            {
                ShowNextDialogue();
            }
            else
            {
                EndDialogue();
            }
        });
    }
}
