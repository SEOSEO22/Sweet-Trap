using UnityEngine;
using UnityEngine.UI;

public class ELet3Event : MonoBehaviour
{
    [Header("UI ����")]
    public Text targetText;

    private Collider2D[] _colliders; // 2D �ݶ��̴� ���

    void Start()
    {
        _colliders = GetComponents<Collider2D>(); // 2D �ݶ��̴� ��������
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleClick();
    }

    void HandleClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            for (int i = 0; i < _colliders.Length; i++)
            {
                if (hit.collider == _colliders[i])
                {
                    UpdateText(i);
                    break;
                }
            }
        }
    }

    void UpdateText(int index)
    {
        string message = index switch
        {
            0 => "����� �����ؿ�! ���� �� �ð� �뷡��\n �θ���, �̸����� �� �پ�ٴմϴ�.",
            1 => "���� ���� ���ƿ�. �� �õ� �������� ���� �ſ���.\n ��� �Ϸ翡 �� �ð��� �� �Ѵ�ϴ�.",
            2 => "���Ǹ� ���� �� ���� ���Ƿο� �����̿���.\n ��ҿ� ����ó�� �ٳ༭ �Ž����� ���� �ſ���.",
            _ => ""
        };

        targetText.text = message;
    }
}
