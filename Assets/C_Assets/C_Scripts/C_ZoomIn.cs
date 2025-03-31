using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ZoomIn : MonoBehaviour
{
    private Camera cam;
    private Vector3 originalPosition;
    private float originalSize;

    public float zoomSize = 3f;
    public float zoomSpeed = 2f;
    private bool isZoomed = false;

    public GameObject backButton;
    public CameraScreenManager screenManager;

    void Start()
    {
        cam = Camera.main;
        originalSize = cam.orthographicSize;
        backButton.SetActive(false);
    }

    public void ZoomIn(Vector3 targetPosition)
    {
        if (!isZoomed)
        {
            if (screenManager != null)
            {
                originalPosition = screenManager.GetCurrentScreenPosition();
            }
            else
            {
                originalPosition = cam.transform.position;
            }

            StopAllCoroutines();
            StartCoroutine(ChangeZoom(targetPosition, zoomSize));
            isZoomed = true;
            backButton.SetActive(true);
        }
    }

    public void ZoomOut()
    {
        if (isZoomed)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeZoom(originalPosition, originalSize));
            isZoomed = false;
            backButton.SetActive(false);

            C_DialogueManager.Instance.HideDialogue();

            //  ÁÜ¾Æ¿ô ÈÄ ÇöÀç È­¸é ÀÎµ¦½º¸¦ °»½Å
            if (screenManager != null)
            {
                screenManager.UpdateCurrentIndex(originalPosition);
            }
        }
    }

    IEnumerator ChangeZoom(Vector3 targetPosition, float targetSize)
    {
        while (Mathf.Abs(cam.orthographicSize - targetSize) > 0.01f ||
               Vector3.Distance(cam.transform.position, targetPosition) > 0.01f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, Time.deltaTime * zoomSpeed);
            yield return null;
        }

        cam.orthographicSize = targetSize;
        cam.transform.position = targetPosition;
    }
}
