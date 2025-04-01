using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform cameraBottomPosition; 
    public Transform cameraTopPosition;    
    public float moveSpeed = 5f;          

    private Transform mainCamera;
    private int currentClickCount = 1;     
    private bool isMoving = false;

    void Start()
    {
        mainCamera = Camera.main?.transform;
        if (mainCamera == null)
        {
            return;
        }

        currentClickCount = 1; 
        mainCamera.position = Vector3.Lerp(cameraBottomPosition.position, cameraTopPosition.position, 0.5f); 
    }

    public void MoveCameraUp()
    {
        if (!isMoving && currentClickCount < 2)
        {
            currentClickCount++;
            Vector3 targetPosition = cameraTopPosition.position;
            StartCoroutine(MoveToPosition(targetPosition));
        }
    }

    public void MoveCameraDown()
    {
        if (!isMoving && currentClickCount > 0)
        {
            currentClickCount--;
            Vector3 targetPosition = cameraBottomPosition.position;
            StartCoroutine(MoveToPosition(targetPosition));
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        if (mainCamera == null)
        {
            yield break;
        }

        isMoving = true;

        while (Vector3.Distance(mainCamera.position, targetPosition) > 0.01f)
        {
            mainCamera.position = Vector3.Lerp(
                mainCamera.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        mainCamera.position = targetPosition; 
        isMoving = false;

        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        ArrowClick[] arrowClicks = FindObjectsOfType<ArrowClick>();

        foreach (ArrowClick arrow in arrowClicks)
        {
            if (isMoving)
            {
                arrow.SetButtonInteractable(false);
            }
            else if (arrow.isArrowUp && currentClickCount == 2)
            {
                arrow.SetButtonInteractable(false);
            }
            else if (!arrow.isArrowUp && currentClickCount == 0) 
            {
                arrow.SetButtonInteractable(false);
            }
            else
            {
                arrow.SetButtonInteractable(true); 
            }
        }
    }
}






//using UnityEngine;
//using System.Collections;

//public class CameraController : MonoBehaviour
//{
//    public Transform cameraUpPosition;
//    public Transform cameraDownPosition;
//    public float moveSpeed = 5f;

//    private Transform mainCamera;
//    public bool isUp = false;
//    private bool isMoving = false;

//    void Start()
//    {
//        mainCamera = Camera.main?.transform;
//        if (mainCamera == null)
//        {
//            return;
//        }

//        if (Vector3.Distance(mainCamera.position, cameraDownPosition.position) < 0.1f)
//        {
//            isUp = false;
//        }
//        else if (Vector3.Distance(mainCamera.position, cameraUpPosition.position) < 0.1f)
//        {
//            isUp = true;
//        }
//    }

//    public void MoveCameraUp()
//    {
//        if (!isUp && !isMoving)
//        {
//            StartCoroutine(MoveToPosition(cameraUpPosition.position, true));
//        }
//        else
//        {
//        }
//    }

//    public void MoveCameraDown()
//    {
//        if (isUp && !isMoving)
//        {
//            StartCoroutine(MoveToPosition(cameraDownPosition.position, false));
//        }
//    }

//    private IEnumerator MoveToPosition(Vector3 targetPosition, bool newState)
//    {
//        if (mainCamera == null)
//        {
//            yield break;
//        }

//        isMoving = true;
//        while (Vector3.Distance(mainCamera.position, targetPosition) > 0.01f)
//        {
//            mainCamera.position = Vector3.Lerp(
//            mainCamera.position,
//            targetPosition,
//            moveSpeed * Time.deltaTime
//            );
//            yield return null;
//        }

//        mainCamera.position = targetPosition;
//        isUp = newState;
//        isMoving = false;

//        UpdateButtonStates();
//    }

//    private void UpdateButtonStates()
//    {
//        ArrowClick[] arrowClicks = FindObjectsOfType<ArrowClick>();

//        foreach (ArrowClick arrow in arrowClicks)
//        {
//            if (isMoving)
//            {
//                arrow.SetButtonInteractable(false);
//            }
//            else if (arrow.isArrowUp && isUp)
//            {
//                arrow.SetButtonInteractable(false);
//            }
//            else if (!arrow.isArrowUp && !isUp)
//            {
//                arrow.SetButtonInteractable(false);
//            }
//            else
//            {
//                arrow.SetButtonInteractable(true);
//            }
//        }
//    }
//}
