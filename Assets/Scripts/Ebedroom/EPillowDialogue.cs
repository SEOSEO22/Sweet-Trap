using UnityEngine;
using UnityEngine.UI;

public class PillowDialogue : MonoBehaviour
{
    public GameObject talk;
    public Text dialogueText;
    public string[] dialogues;
    private int clickCount = 0;
    private bool isDialogueActive = false;

    private void Start()
    {
        if (talk != null)
        {
            talk.SetActive(false);
            AddButtonToTalk();
        }
    }

    private void OnMouseDown()
    {
        if (!EventManager.Instance.IsAnyEventActive() && !isDialogueActive)
        {
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        EventManager.Instance.StartEvent("PillowDialogue");
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
        EventManager.Instance.EndEvent("PillowDialogue");
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
