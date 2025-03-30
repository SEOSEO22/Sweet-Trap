using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistroyItem : MonoBehaviour
{
    [SerializeField] private CameraZoom zoom;
    private Collider2D itemCollider;

    private void Start()
    {
        itemCollider = GetComponent<Collider2D>();
        itemCollider.enabled = false;
    }

    private void Update()
    {
        DestroyItem();
    }

    private void DestroyItem()
    {
        if (zoom.isZoom)
        {
            itemCollider.enabled = true;

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hit.collider)
                    Destroy(gameObject);
            }
        }
        else
        {
            itemCollider.enabled = false;
        }
    }
}
