using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistroyItem : MonoBehaviour
{
    [SerializeField] private CameraZoom zoom;
    [SerializeField] private GameObject target;
    private Collider2D itemCollider;

    private void Start()
    {
        itemCollider = GetComponent<Collider2D>();
        itemCollider.enabled = false;

        if (target != null)
            target.SetActive(false);
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

            if (Camera.main.orthographicSize < (zoom.minZoom + 0.1f) && Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hit.collider && hit.collider.gameObject.name == this.name)
                {
                    if (target != null)
                        target.SetActive(true);
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            itemCollider.enabled = false;
        }
    }
}
