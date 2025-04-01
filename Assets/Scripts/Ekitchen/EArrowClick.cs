using UnityEngine;
using UnityEngine.UI;

public class ArrowClick : MonoBehaviour
{
    public CameraController cameraController;
    public bool isArrowUp;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnButtonClick()
    {
        if (cameraController == null)
        {
            return;
        }

        if (isArrowUp)
        {
            cameraController.MoveCameraUp();
        }
        else
        {
            cameraController.MoveCameraDown();
        }
    }

    public void SetButtonInteractable(bool state)
    {
        if (button != null)
        {
            button.interactable = state;
        }
    }
}


//using UnityEngine;
//using UnityEngine.UI;

//public class ArrowClick : MonoBehaviour
//{
//    public CameraController cameraController;
//    public bool isArrowUp;

//    private Button button;

//    void Start()
//    {
//        button = GetComponent<Button>();
//    }

//    public void OnButtonClick()
//    {
//        if (cameraController == null)
//        {
//            return;
//        }

//        if (isArrowUp)
//        {
//            if (!cameraController.isUp)
//            {
//                cameraController.MoveCameraUp();
//            }
//        }
//        else
//        {
//            if (cameraController.isUp)
//            {
//                cameraController.MoveCameraDown();
//            }
//        }
//    }

//    public void SetButtonInteractable(bool state)
//    {
//        if (button != null)
//        {
//            button.interactable = state;
//        }
//    }
//}
