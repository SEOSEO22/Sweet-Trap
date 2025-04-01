using UnityEngine;
using UnityEngine.UI;

public class CardEvent : MonoBehaviour
{
    public GameObject black;
    public GameObject xButton;
    public GameObject card;
    public GameObject cardDish;
    public GameObject bottle;

    public GameObject inputFieldObject; 
    public GameObject confirmButton;   

    private bool isActivated = false;

    void Start()
    {
        if (black != null) black.SetActive(false);
        if (xButton != null) xButton.SetActive(false);
        if (card != null) card.SetActive(false);
        if (inputFieldObject != null) inputFieldObject.SetActive(false);
        if (confirmButton != null) confirmButton.SetActive(false);

        if (bottle != null) bottle.SetActive(false);
        if (cardDish != null) cardDish.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (!isActivated && gameObject.CompareTag("card"))
        {
            ActivateObjects();
        }
    }

    public void ActivateObjects()
    {
        if (!isActivated)
        {
            isActivated = true;

            if (black != null) black.SetActive(true);
            if (xButton != null) xButton.SetActive(true);
            if (card != null) card.SetActive(true);

            if (inputFieldObject != null) inputFieldObject.SetActive(true);
            if (confirmButton != null) confirmButton.SetActive(true);
        }
    }

    public void DeactivateObjects()
    {
        if (isActivated)
        {
            isActivated = false;

            if (black != null) black.SetActive(false);
            if (xButton != null) xButton.SetActive(false);
            if (card != null) card.SetActive(false);

            if (inputFieldObject != null) inputFieldObject.SetActive(false);
            if (confirmButton != null) confirmButton.SetActive(false);
        }
    }

    public void ConfirmInput()
    {
        InputField inputField = inputFieldObject.GetComponent<InputField>();

        if (inputField != null)
        {
            string inputText = inputField.text;

            if (inputText == "38")
            {
                DeactivateObjects();

                if (cardDish != null) cardDish.SetActive(false);
                if (bottle != null) bottle.SetActive(true);
            }
        }
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
                    DeactivateObjects();
                    return;
                }
            }
        }
    }
}
