using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenManager : MonoBehaviour
{
    public List<Transform> screenPositions; // ī�޶� �̵��� ��ġ ����Ʈ
    private int currentIndex = 0; // ���� ȭ�� �ε���
    private Camera cam; // ���� ī�޶�

    void Start()
    {
        cam = Camera.main; // ���� ī�޶� ��������

        if (screenPositions.Count > 0 && cam != null)
        {
            cam.transform.position = screenPositions[currentIndex].position; // �ʱ� ��ġ ����
        }
    }

    public void MoveRight()
    {
        if (cam == null) return;

        currentIndex = (currentIndex + 1) % screenPositions.Count;
        Vector3 newPosition = screenPositions[currentIndex].position;
        cam.transform.position = newPosition;

        FindObjectOfType<CameraZoom>().SetCameraPosition(newPosition); // �߰�
    }

    public void MoveLeft()
    {
        if (cam == null) return;

        currentIndex = (currentIndex - 1 + screenPositions.Count) % screenPositions.Count;
        Vector3 newPosition = screenPositions[currentIndex].position;
        cam.transform.position = newPosition;

        FindObjectOfType<CameraZoom>().SetCameraPosition(newPosition); // �߰�
    }

    public Vector3 GetCurrentScreenPosition()
    {
        return screenPositions[currentIndex].position;
    }

    public void UpdateCurrentIndex(Vector3 cameraPosition)
    {
        for (int i = 0; i < screenPositions.Count; i++)
        {
            if (Vector3.Distance(cameraPosition, screenPositions[i].position) < 0.1f)
            {
                currentIndex = i;
                break;
            }
        }
    }
}