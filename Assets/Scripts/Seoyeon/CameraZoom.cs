using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoom;
    public float minZoom { get; private set; } = 2f;
    public float maxZoom { get; private set; }
    private float velocity = 0f;
    private float smoothTime = 0.25f;

    [SerializeField]
    private DialogDataManager dialogManager;
    [SerializeField]
    private GameObject background;
    private Vector2 backgroundSize;

    private Vector3 originPosition;
    private Vector3 targetPosition;
    private Vector3 currentPosition;
    private Vector3 vectorVelocity = Vector3.zero;
    private Collider2D zoomCollider;
    private Camera mainCam;

    public bool isZoom { get; private set; }

    private void Start()
    {
        mainCam = Camera.main;
        zoom = mainCam.orthographicSize;
        maxZoom = zoom;
        isZoom = false;

        originPosition = mainCam.transform.position;
        currentPosition = originPosition;

        backgroundSize = background.GetComponent<SpriteRenderer>().bounds.size;
    }

    private void Update()
    {
        if (dialogManager.isDialogPanelActive) return;

        if (Input.GetMouseButtonDown(0))
            ZoomIn();
        else if (Input.GetKeyDown(KeyCode.Escape)) 
            ZoomOut();

        mainCam.orthographicSize = Mathf.SmoothDamp(mainCam.orthographicSize, zoom, ref velocity, smoothTime);
        mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position, currentPosition, ref vectorVelocity, smoothTime);
    }

    private void ZoomIn()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider && hit.collider.gameObject.CompareTag("Zoom") && !isZoom)
        {
            zoomCollider = hit.collider;
            zoomCollider.enabled = false;

            zoom = minZoom;
            isZoom = true;

            // 클릭한 지점을 중심으로 카메라 이동
            targetPosition = ClampCameraPosition(new Vector3(mousePos.x, mousePos.y, mainCam.transform.position.z));
            currentPosition = targetPosition;
        }
    }

    private void ZoomOut()
    {
        if (isZoom)
        {
            zoomCollider.enabled = true;

            zoom = maxZoom;
            isZoom = false;

            currentPosition = originPosition;
        }
    }

    // 카메라 위치 제한 함수
    private Vector3 ClampCameraPosition(Vector3 targetPosition)
    {
        float cameraHeight = zoom;
        float cameraWidth = cameraHeight * mainCam.aspect;

        float minX = backgroundSize.x / -2 + cameraWidth;
        float maxX = backgroundSize.x / 2 - cameraWidth;
        float minY = backgroundSize.y / -2 + cameraHeight;
        float maxY = backgroundSize.y / 2 - cameraHeight;

        float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(clampedX, clampedY, targetPosition.z);
    }
}
