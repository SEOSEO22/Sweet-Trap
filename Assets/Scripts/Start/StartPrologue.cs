using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartPrologue : MonoBehaviour
{
    public Image blackImage;
    public float darkDuration = 0.8f;
    public float lightDuration = 1.5f;

    public GameObject talk;
    public Text prologueText;

    private string[] dialogues = new string[]
    {
        "낯선 방이다.",
        "혹시 내 집일까?",
        "아니다. 아무것도 기억이 안 나지만…",
        "여긴 내 집이 아니다.",
        "일단 이 곳을 벗어나자."
    };

    private int currentPrologueTextIndex = 0;

    private void Start()
    {
        if (talk != null)
        {
            talk.SetActive(false);
        }

        EventManager.Instance.StartEvent("StartPrologue");
        StartCoroutine(BlinkCoroutine());
    }

    IEnumerator BlinkCoroutine()
    {
        blackImage.color = new Color(0, 0, 0, 0);
        yield return FadeImage(0f, 1f, darkDuration);
        yield return FadeImage(1f, 0f, lightDuration);
        yield return FadeImage(0f, 1f, darkDuration);
        yield return FadeImage(1f, 0f, lightDuration);

        if (talk != null)
        {
            talk.SetActive(true);
            AddClickListenerToDialogue();
            ShowNextPrologueText();
        }
    }

    IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color c = blackImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            blackImage.color = c;
            yield return null;
        }

        c.a = endAlpha;
        blackImage.color = c;
    }

    private void ShowNextPrologueText()
    {
        if (currentPrologueTextIndex < dialogues.Length)
        {
            prologueText.text = dialogues[currentPrologueTextIndex];
            currentPrologueTextIndex++;
        }

        if (currentPrologueTextIndex >= dialogues.Length)
        {
            AddEndListenerToDialogue();
        }
    }

    private void AddClickListenerToDialogue()
    {
        Button dialogueButton = talk.GetComponent<Button>();
        if (dialogueButton == null)
        {
            dialogueButton = talk.AddComponent<Button>();
        }

        dialogueButton.onClick.RemoveAllListeners();
        dialogueButton.onClick.AddListener(ShowNextPrologueText);
    }

    private void AddEndListenerToDialogue()
    {
        Button dialogueButton = talk.GetComponent<Button>();
        dialogueButton.onClick.RemoveAllListeners();
        dialogueButton.onClick.AddListener(EndPrologue);
    }

    private void EndPrologue()
    {
        talk.SetActive(false);
        EventManager.Instance.EndEvent("StartPrologue");
    }
}
