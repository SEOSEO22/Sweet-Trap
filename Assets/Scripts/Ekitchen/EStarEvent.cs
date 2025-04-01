using UnityEngine;
using System.Collections;

public class EStarEvent : MonoBehaviour
{
    public GameObject rope;
    public GameObject black;
    public GameObject xButton;
    public GameObject[] stars;
    public GameObject curtain;
    public GameObject curtainOpen;
    public GameObject plusStar;

    private bool isActivated = false;
    private bool isStar9Deactivated = false;

    void Start()
    {
        if (rope != null) rope.SetActive(false);
        if (black != null) black.SetActive(false);
        if (xButton != null) xButton.SetActive(false);

        foreach (GameObject star in stars)
        {
            if (star != null) star.SetActive(false);
        }

        if (curtainOpen != null) curtainOpen.SetActive(false);
        if (plusStar != null) plusStar.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (!isActivated && gameObject.CompareTag("star"))
        {
            ActivateObjects();
        }
    }

    public void ActivateObjects()
    {
        if (!isActivated)
        {
            isActivated = true;

            if (rope != null) rope.SetActive(true);
            if (black != null) black.SetActive(true);
            if (xButton != null) xButton.SetActive(true);

            foreach (GameObject star in stars)
            {
                if (star != null && !(isStar9Deactivated && star.name == "star9"))
                {
                    star.SetActive(true);
                }
            }
        }
    }

    public void DeactivateObjects()
    {
        if (isActivated)
        {
            isActivated = false;

            if (rope != null) rope.SetActive(false);
            if (black != null) black.SetActive(false);
            if (xButton != null) xButton.SetActive(false);

            foreach (GameObject star in stars)
            {
                if (star != null)
                {
                    star.SetActive(false);
                }
            }
        }
    }

    public void EndEvent()
    {
        StartCoroutine(AnimateAndDeactivateStar9());
    }

    private IEnumerator AnimateAndDeactivateStar9()
    {
        GameObject star9 = null;

        foreach (GameObject star in stars)
        {
            if (star.name == "star9")
            {
                star9 = star;
                break;
            }
        }

        if (star9 != null)
        {
            Vector3 startPosition = star9.transform.position;
            Vector3 endPosition = startPosition + new Vector3(0, -2f, 0);

            float duration = 1f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                star9.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            star9.transform.position = endPosition;

            yield return new WaitForSeconds(1f);

            star9.SetActive(false);
            isStar9Deactivated = true;

            if (plusStar != null)
            {
                plusStar.SetActive(false);
            }

            if (curtain != null) curtain.SetActive(false);
            if (curtainOpen != null) curtainOpen.SetActive(true);

            DeactivateObjects();
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

                foreach (GameObject star in stars)
                {
                    if (hitCollider.gameObject == star && star.name == "star9")
                    {
                        EndEvent();
                        return;
                    }
                }
            }
        }
    }
}
