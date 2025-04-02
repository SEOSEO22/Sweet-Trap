using UnityEngine;

public class ECameraController : MonoBehaviour
{
    public Transform[] positions; // 0, 1, 2 ��ġ
    public float moveSpeed = 5f; // �̵� �ӵ�
    private int currentIndex = 1; // ���� ��ġ�� 1�� (�߰�)


    private void Update()
    {
        Debug.Log(positions[currentIndex].position);
        // ī�޶� ��ǥ ��ġ�� �ε巴�� �̵�
        transform.position = Vector3.Lerp(transform.position, positions[currentIndex].position, Time.deltaTime * moveSpeed);
    }

    public void MoveLeft()
    {
        if (currentIndex > 0) // ���� �̵� ������ ���� �̵�
        {
            currentIndex--;
        }
    }

    public void MoveRight()
    {
        if (currentIndex < positions.Length - 1) // ������ �̵� ������ ���� �̵�
        {
            currentIndex++;
        }
    }
}
