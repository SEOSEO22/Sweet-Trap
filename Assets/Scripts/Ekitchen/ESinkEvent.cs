using UnityEngine;
using System.Collections;

public class ESinkEvent : MonoBehaviour
{
    public GameObject black;
    public GameObject xButton;
    public GameObject faucet;
    public GameObject sink;
    public GameObject water;
    public GameObject seaweed;

    private bool isActivated = false;
    private int currentStep = 0;
    private readonly int[] correctSequence = { 1, 2, 2, 1, 2 };

    void Start()
    {
        if (black != null) black.SetActive(false);
        if (xButton != null) xButton.SetActive(false);
        if (faucet != null) faucet.SetActive(false);
        if (sink != null) sink.SetActive(true);
        if (water != null) water.SetActive(false);
        if (seaweed != null) seaweed.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!isActivated && gameObject.CompareTag("blank"))
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
            if (faucet != null) faucet.SetActive(true);
        }
    }

    public void DeactivateObjects()
    {
        if (isActivated)
        {
            isActivated = false;

            if (black != null) black.SetActive(false);
            if (xButton != null) xButton.SetActive(false);
            if (faucet != null) faucet.SetActive(false);

            currentStep = 0;

            if (water != null)
            {
                water.SetActive(false);
            }
        }
    }

    public void HandleFaucetClick(int colliderID)
    {
        if (!isActivated || currentStep >= correctSequence.Length) return;

        if (correctSequence[currentStep] == colliderID)
        {
            currentStep++;
            UpdateWaterState();

            if (currentStep >= correctSequence.Length)
            {
                StartCoroutine(AnimateSeaweedAndCompletePuzzle());
            }
        }
        else
        {
            currentStep = 0;

            if (water != null)
            {
                water.SetActive(false);
            }
        }
    }

    private void UpdateWaterState()
    {
        if (water != null)
        {
            water.SetActive(true);

            Color newColor = water.GetComponent<Renderer>().material.color;

            switch (currentStep)
            {
                case 1:
                    newColor = new Color(0.5f, 0f, 0.5f); 
                    break;
                case 2:
                    newColor = new Color(1f, 0.5f, 0f); 
                    break;
                case 3:
                    newColor = new Color(1f, 1f, 1f);
                    break;
                case 4:
                    newColor = new Color(0.6f, 0.3f, 0.2f); 
                    break;
                case 5:
                    newColor = water.GetComponent<Renderer>().material.color; 
                    break;
            }

            Renderer renderer = water.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = newColor; 
            }
        }
    }

    private IEnumerator AnimateSeaweedAndCompletePuzzle()
    {
        if (seaweed != null)
        {
            seaweed.SetActive(true);

            Vector3 startPosition = seaweed.transform.position;
            Vector3 endPosition = startPosition + new Vector3(0, -1.5f, 0);

            float duration = 1f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                seaweed.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            seaweed.transform.position = endPosition;
        }

        CompletePuzzle();
    }

    private void CompletePuzzle()
    {
        DeactivateObjects();

        if (sink != null)
        {
            sink.SetActive(false);
        }

        if (water != null)
        {
            water.SetActive(false);
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

                FaucetCollider faucetCollider = hitCollider.GetComponent<FaucetCollider>();
                if (faucetCollider != null)
                {
                    HandleFaucetClick(faucetCollider.colliderID);
                }
            }
        }
    }
}
