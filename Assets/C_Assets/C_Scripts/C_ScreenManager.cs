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

        currentIndex = (currentIndex + 1) % screenPositions.Count; // ������ �̵� (��ȯ)
        cam.transform.position = screenPositions[currentIndex].position; // ��� �̵�
    }

    public void MoveLeft()
    {
        if (cam == null) return;

        currentIndex = (currentIndex - 1 + screenPositions.Count) % screenPositions.Count; // ���� �̵� (��ȯ)
        cam.transform.position = screenPositions[currentIndex].position; // ��� �̵�
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