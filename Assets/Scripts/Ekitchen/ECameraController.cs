using UnityEngine;

public class ECameraController : MonoBehaviour
{
    public Transform[] positions; // 0, 1, 2 위치
    public float moveSpeed = 5f; // 이동 속도
    private int currentIndex = 1; // 시작 위치는 1번 (중간)


    private void Update()
    {
        Debug.Log(positions[currentIndex].position);
        // 카메라를 목표 위치로 부드럽게 이동
        transform.position = Vector3.Lerp(transform.position, positions[currentIndex].position, Time.deltaTime * moveSpeed);
    }

    public void MoveLeft()
    {
        if (currentIndex > 0) // 왼쪽 이동 가능할 때만 이동
        {
            currentIndex--;
        }
    }

    public void MoveRight()
    {
        if (currentIndex < positions.Length - 1) // 오른쪽 이동 가능할 때만 이동
        {
            currentIndex++;
        }
    }
}
