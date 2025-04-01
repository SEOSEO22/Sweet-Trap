using UnityEngine;
using System.Collections;

public class EAreaClickEvent : MonoBehaviour
{
    public GameObject open1;
    public GameObject open2;
    public GameObject drawer;
    public GameObject letter;
    public GameObject memo;

    public Collider2D area1Collider;
    public Collider2D area2Collider;

    private bool isArea1Clicked = false;
    private bool isArea2Clicked = false;

    public void Start()
    {
        if (open1 != null) open1.SetActive(false);
        if (open2 != null) open2.SetActive(false);
        if (drawer != null) drawer.SetActive(true);
        if (letter != null) letter.SetActive(false);
        if (memo != null) memo.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null)
            {
                if (hitCollider == area1Collider && !isArea1Clicked)
                {
                    ActivateOpen1();
                    isArea1Clicked = true; 
                }
                else if (hitCollider == area2Collider && !isArea2Clicked)
                {
                    ActivateOpen2();
                    isArea2Clicked = true; 
                }
            }
        }
    }

    private void ActivateOpen1()
    {
        if (letter != null)
        {
            letter.SetActive(true);
            StartCoroutine(MoveDiagonally(letter));
        }

        if (drawer != null) drawer.SetActive(false);

        if (open1 != null) open1.SetActive(true);
        StartCoroutine(DeactivateOpenAfterDelay(open1, 1f));
    }

    private void ActivateOpen2()
    {
        if (memo != null)
        {
            memo.SetActive(true);
            StartCoroutine(MoveDiagonally(memo));
        }

        if (drawer != null) drawer.SetActive(false);

        if (open2 != null) open2.SetActive(true);
        StartCoroutine(DeactivateOpenAfterDelay(open2, 1f));
    }

    private IEnumerator DeactivateOpenAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (obj != null) obj.SetActive(false);
        if (drawer != null) drawer.SetActive(true);
    }

    private IEnumerator MoveDiagonally(GameObject obj)
    {
        float duration = 1f;
        float elapsedTime = 0f;
        Vector3 startPosition = obj.transform.position;
        Vector3 targetPosition = startPosition + new Vector3(2f, -2f, 0f);

        while (elapsedTime < duration)
        {
            if (obj == null || !obj.activeInHierarchy)
                yield break;

            elapsedTime += Time.deltaTime;
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }
    }
}
