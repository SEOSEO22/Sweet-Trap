using UnityEngine;
using UnityEngine.UI; 

public class MirrorEvent : MonoBehaviour
{
    public GameObject black;
    public GameObject Emirror;
    public GameObject xButton;
    public GameObject chocotalk; 
    public Text text;

    private bool isActivated = false;

    public void Start()
    {
        if (black != null) black.SetActive(false);
        if (Emirror != null) Emirror.SetActive(false);
        if (xButton != null) xButton.SetActive(false);
        if (chocotalk != null) chocotalk.SetActive(false); 
        if (text != null) text.gameObject.SetActive(false); 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null)
            {
                if (!isActivated && hitCollider.gameObject.CompareTag("mirror"))
                {
                    ActivateMirrorEvent();
                }
                else if (isActivated && hitCollider.gameObject == xButton)
                {
                    EndMirrorEvent();
                }
            }
        }
    }

    public void ActivateMirrorEvent()
    {
        isActivated = true;

        if (black != null) black.SetActive(true);
        if (Emirror != null) Emirror.SetActive(true);
        if (xButton != null) xButton.SetActive(true);

        if (chocotalk != null) chocotalk.SetActive(true); 
        if (text != null)
        {
            text.gameObject.SetActive(true); 
            text.text = "거울이네. 뭔가를 비춰볼 수 있겠어."; 
        }

    }

    public void EndMirrorEvent()
    {
        isActivated = false;

        if (black != null) black.SetActive(false);
        if (Emirror != null) Emirror.SetActive(false);
        if (xButton != null) xButton.SetActive(false);

        if (chocotalk != null) chocotalk.SetActive(false); 
        if (text != null) text.gameObject.SetActive(false); 

    }
}
