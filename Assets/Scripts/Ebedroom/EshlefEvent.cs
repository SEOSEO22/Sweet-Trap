using UnityEngine;
using System.Collections;

public class EshelfEvent : MonoBehaviour
{
    public GameObject EX_Shelf;
    public GameObject black;
    public GameObject xButton;
    public GameObject Bead;
    public GameObject arrow;
    public GameObject arrow2;
    private bool isActivated = false;
    private bool isEventCompleted = false;

    private int[] bookStates = new int[8];
    private Vector3[] originalPositions = new Vector3[8];
    private Quaternion[] originalRotations = new Quaternion[8];

    private const int INTERACTIVE_LAYER = 9;

    public void Start()
    {
        if (EX_Shelf != null) EX_Shelf.SetActive(false);
        if (Bead != null) Bead.SetActive(false);
        if (black != null) black.SetActive(false);
        if (xButton != null) xButton.SetActive(false);
        if (arrow != null) arrow.SetActive(true);
        if (arrow2 != null) arrow2.SetActive(true);
    }

    public void OnMouseDown()
    {
        if (isEventCompleted)
            return;

        if (!isActivated)
        {
            ActivateShelfEvent();
        }
    }

    public void ActivateShelfEvent()
    {
        if (!EventManager.Instance.IsAnyEventActive())
        {
            EventManager.Instance.StartEvent("ShelfActivation");
            isActivated = true;

            if (EX_Shelf != null)
            {
                EX_Shelf.SetActive(true);
                SetupInteractiveObjects(EX_Shelf);
            }
            if (black != null) black.SetActive(true);
            if (xButton != null)
            {
                xButton.SetActive(true);
                SetLayerRecursively(xButton, INTERACTIVE_LAYER);
            }
            if (arrow != null) arrow.SetActive(false);
            if (arrow2 != null) arrow2.SetActive(false);
        }
    }

    public void SetupInteractiveObjects(GameObject parent)
    {
        for (int i = 1; i <= 8; i++)
        {
            GameObject book = parent.transform.Find($"book{i}")?.gameObject;
            if (book != null)
            {
                int bookIndex = i - 1;
                originalPositions[bookIndex] = book.transform.localPosition;
                originalRotations[bookIndex] = book.transform.localRotation;

                BookClickHandler clickHandler = book.GetComponent<BookClickHandler>();
                if (clickHandler == null)
                {
                    clickHandler = book.AddComponent<BookClickHandler>();
                }
                clickHandler.Setup(this, bookIndex);

                SetLayerRecursively(book, INTERACTIVE_LAYER);
            }
        }
    }

    public void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    public void OnBookClicked(int bookIndex)
    {
        if (isActivated)
        {
            bookStates[bookIndex] = 1 - bookStates[bookIndex];

            GameObject book = EX_Shelf.transform.Find($"book{bookIndex + 1}")?.gameObject;
            if (book != null)
            {
                StartCoroutine(MoveBook(book, bookIndex));
            }

            CheckBookStates();
        }
    }

    public void CheckBookStates()
    {
        int[] targetState = { 0, 0, 1, 1, 1, 0, 1, 0 };

        bool isSuccess = true;
        for (int i = 0; i < bookStates.Length; i++)
        {
            if (bookStates[i] != targetState[i])
            {
                isSuccess = false;
                break;
            }
        }

        if (isSuccess)
        {
            CompleteEvent();
        }
    }
    public void CompleteEvent()
    {
        isActivated = false;
        isEventCompleted = true;

        if (EX_Shelf != null) EX_Shelf.SetActive(false);
        if (black != null) black.SetActive(false);
        if (xButton != null) xButton.SetActive(false);
        if (arrow != null) arrow.SetActive(true);
        if (arrow != null) arrow2.SetActive(true);

        EventManager.Instance.EndEvent("ShelfActivation");

        EGameManager.Instance.ActivateAndMoveBead(Bead); 
    }


    public IEnumerator ActivateAndMoveBead()
    {
        Bead.SetActive(true); 
        yield return null; 

        Bead.GetComponent<BeadScript>()?.ActivateBead(); 
    }

    public System.Collections.IEnumerator MoveBook(GameObject book, int bookIndex)
    {
        Vector3 startPosition = book.transform.localPosition;
        Quaternion startRotation = book.transform.localRotation;
        Vector3 targetPosition;
        Quaternion targetRotation;

        if (bookStates[bookIndex] == 1)
        {
            targetPosition = originalPositions[bookIndex] + new Vector3(0, -1.5f, 0);
            targetRotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            targetPosition = originalPositions[bookIndex];
            targetRotation = originalRotations[bookIndex];
        }

        float elapsedTime = 0;
        float duration = 0.5f;

        while (elapsedTime < duration)
        {
            book.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            book.transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        book.transform.localPosition = targetPosition;
        book.transform.localRotation = targetRotation;
    }

    public void EndShelfEvent()
    {
        isActivated = false;

        ResetBooks();

        if (EX_Shelf != null) EX_Shelf.SetActive(false);
        if (black != null) black.SetActive(false);
        if (xButton != null) xButton.SetActive(false);
        if (arrow != null) arrow.SetActive(true);
        if (arrow != null) arrow2.SetActive(true);

        EventManager.Instance.EndEvent("ShelfActivation");
    }

    public void ResetBooks()
    {
        for (int i = 0; i < bookStates.Length; i++)
        {
            bookStates[i] = 0;
            GameObject book = EX_Shelf.transform.Find($"book{i + 1}")?.gameObject;
            if (book != null)
            {
                book.transform.localPosition = originalPositions[i];
                book.transform.localRotation = originalRotations[i];
            }
        }
    }
        public void Update()
    {
        if (isActivated && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitCollider =
                Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, 1 << INTERACTIVE_LAYER);

            if (hitCollider.collider != null)
            {
                if (hitCollider.collider.gameObject == xButton)
                {
                    EndShelfEvent();
                }
                else
                {
                    BookClickHandler bookHandler =
                        hitCollider.collider.GetComponent<BookClickHandler>();
                    if (bookHandler != null)
                    {
                        bookHandler.OnClick();
                    }
                }
            }
        }
    }

    public class BookClickHandler : MonoBehaviour
    {
        private EshelfEvent shelfManager;
        private int bookIndex;

        public void Setup(EshelfEvent manager, int index)
        {
            shelfManager = manager;
            bookIndex = index;
        }

        public void OnClick()
        {
            shelfManager?.OnBookClicked(bookIndex);
        }
    }
}
