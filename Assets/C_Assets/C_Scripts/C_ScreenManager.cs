using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenManager : MonoBehaviour
{
    public List<Transform> screenPositions; // 카메라가 이동할 위치 리스트
    private int currentIndex = 0; // 현재 화면 인덱스
    private Camera cam; // 메인 카메라

    void Start()
    {
        cam = Camera.main; // 메인 카메라 가져오기

        if (screenPositions.Count > 0 && cam != null)
        {
            cam.transform.position = screenPositions[currentIndex].position; // 초기 위치 설정
        }
    }

    public void MoveRight()
    {
        if (cam == null) return;

        currentIndex = (currentIndex + 1) % screenPositions.Count; // 오른쪽 이동 (순환)
        cam.transform.position = screenPositions[currentIndex].position; // 즉시 이동
    }

    public void MoveLeft()
    {
        if (cam == null) return;

        currentIndex = (currentIndex - 1 + screenPositions.Count) % screenPositions.Count; // 왼쪽 이동 (순환)
        cam.transform.position = screenPositions[currentIndex].position; // 즉시 이동
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