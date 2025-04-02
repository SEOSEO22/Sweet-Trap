using UnityEngine;
using UnityEngine.UI;

public class ELet3Event : MonoBehaviour
{
    [Header("UI 설정")]
    public Text targetText;

    private Collider2D[] _colliders; // 2D 콜라이더 사용

    void Start()
    {
        _colliders = GetComponents<Collider2D>(); // 2D 콜라이더 가져오기
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
            0 => "당신을 좋아해요! 저는 매 시간 노래를\n 부르고, 이리저리 잘 뛰어다닙니다.",
            1 => "저는 말이 많아요. 한 시도 지루하지 않을 거예요.\n 명상도 하루에 한 시간씩 꼭 한답니다.",
            2 => "불의를 보면 못 참는 정의로운 성격이에요.\n 평소엔 유령처럼 다녀서 거슬리지 않을 거예요.",
            _ => ""
        };

        targetText.text = message;
    }
}
