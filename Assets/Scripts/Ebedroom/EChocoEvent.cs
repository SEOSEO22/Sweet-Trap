using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EChocoEvent : MonoBehaviour
{
    public GameObject chocotalk;
    public GameObject black;
    public GameObject xButton;
    public GameObject milk;
    public GameObject banana;
    public GameObject straw;
    public GameObject bone;
    public GameObject xb;
    public GameObject vb;
    public Text talkText;
    public GameObject retryObject;
    public GameObject end;

    private bool isActivated = false;
    private bool isBananaClicked = false;
    private bool isStrawClicked = false;
    private bool isMilkClicked = false;

    public void Start()
    {
        if (chocotalk != null) chocotalk.SetActive(false);
        if (black != null) black.SetActive(false);
        if (xButton != null) xButton.SetActive(false);
        if (milk != null) milk.SetActive(false);
        if (banana != null) banana.SetActive(false);
        if (straw != null) straw.SetActive(false);
        if (xb != null) xb.SetActive(false);
        if (vb != null) vb.SetActive(false);
        if (bone != null) bone.SetActive(false);

        if (retryObject != null)
            retryObject.SetActive(false);

        if (talkText != null)
            talkText.text = "";
    }

    private void OnMouseDown()
    {
        if (!isActivated && gameObject.CompareTag("choco"))
        {
            ActivateChocoEvent();
        }
    }

    public void ActivateChocoEvent()
    {
        if (!isActivated)
        {
            isActivated = true;

            if (chocotalk != null) chocotalk.SetActive(true);
            talkText.gameObject.SetActive(true);
            if (black != null) black.SetActive(true);
            if (xButton != null)
                xButton.SetActive(true);
            if (milk != null)
                milk.SetActive(true);
            if (banana != null)
                banana.SetActive(true);
            if (straw != null)
                straw.SetActive(true);

            if (talkText != null)
                talkText.text = "우유 초콜릿, 바나나 초콜릿, 딸기 초콜릿 3조각이 있어.";

            ResetState();
        }
    }

    public void EndChocoEvent()
    {
        isActivated = false;

        if (chocotalk != null) chocotalk.SetActive(false);
        if (black != null) black.SetActive(false);
        if (xButton != null) xButton.SetActive(false);
        if (milk != null) milk.SetActive(false);
        if (banana != null) banana.SetActive(false);
        if (straw != null) straw.SetActive(false);

        if (xb != null) xb.SetActive(false);
        if (vb != null) vb.SetActive(false);

        if (retryObject != null)
            retryObject.SetActive(false);

        if (talkText != null)
            talkText.text = "";

        ResetState();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null)
            {

                if (hitCollider.gameObject == xButton)
                {
                    EndChocoEvent();
                    return;
                }

                if (hitCollider.gameObject == retryObject)
                {
                    EndChocoEvent();
                    return;
                }

                if (
                    hitCollider.gameObject == milk ||
                    hitCollider.gameObject == banana ||
                    hitCollider.gameObject == straw)
                {
                    HandleChocolatePieceClick(hitCollider.gameObject);
                    return;
                }

                if (hitCollider.gameObject == xb)
                {
                    HandleXbClick();
                    return;
                }

                if (hitCollider.gameObject == vb)
                {
                    HandleVbClick();
                    return;
                }
            }
        }
    }

    private IEnumerator WaitAndEndEvent()
    {
        yield return new WaitForSeconds(1f);

        chocotalk?.SetActive(false);
        black?.SetActive(false);
        xButton?.SetActive(false);
        milk?.SetActive(false);
        banana?.SetActive(false);
        straw?.SetActive(false);
        xb?.SetActive(false);
        vb?.SetActive(false);
        retryObject?.SetActive(false);

        if (end != null)
            end.SetActive(false); 

        isActivated = false;

        if (talkText != null)
            talkText.text = "";
    }

    public void HandleXbClick()
    {
        if (xb != null) xb.SetActive(false);
        if (vb != null) vb.SetActive(false);

        if (talkText != null)
            talkText.text = "안 먹기로 했다.";

        ResetState();
    }

    public void HandleVbClick()
    {
        if (!isActivated)
            return;

        if (isBananaClicked || isStrawClicked)
        {
            if (talkText != null)
                talkText.text = "독이 들어있다...\n기절하여 마녀에게 붙잡히고 말았다.";

            milk?.SetActive(false);
            banana?.SetActive(false);
            straw?.SetActive(false);
            retryObject?.SetActive(true);
            SceneManager.LoadScene("EscapeFailScene");

            return;
        }
        if (isMilkClicked)
        {
            if (talkText != null)
                talkText.text = "맛없다\n초콜릿 안에 뭐가 들어있다.";

            vb?.SetActive(false);
            xb?.SetActive(false);
            milk?.SetActive(false);
            straw?.SetActive(false);
            banana?.SetActive(false);

            bone?.SetActive(true);

            StartCoroutine(WaitAndEndEvent()); 

            return;
        }
    }

    public void HandleChocolatePieceClick(GameObject piece)
    {

        if (!isActivated)
            return;

        if (piece == banana)
            isBananaClicked = true;

        if (piece == straw)
            isStrawClicked = true;

        if (piece == milk)
            isMilkClicked = true;

        xb?.SetActive(true);
        vb?.SetActive(true);

        talkText.text = "먹을까?";
    }

    private void ResetState()
    {
        isBananaClicked = false;
        isStrawClicked = false;
        isMilkClicked = false;
    }
}
