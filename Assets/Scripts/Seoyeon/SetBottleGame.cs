using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetBottleGame : MonoBehaviour
{
    [Header("Bottle Game")]
    [SerializeField] private CameraZoom zoom;
    [SerializeField] private GameObject potionGamePanel;
    [SerializeField] private BottleGameController gameController;
    [SerializeField] private Collider2D[] interatableObjects;

    [Header("Inventory")]
    [SerializeField] public ItemSO InventoryItem;
    [SerializeField] public InventorySO InventoryData;

    private bool isGameClear;

    private void Start()
    {
        potionGamePanel.SetActive(false);
        isGameClear = false;
    }

    private void Update()
    {
        SetGamePanel();

        if (potionGamePanel.activeSelf == true)
        {
            GameClear();
        }
    }

    private void SetGamePanel()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider && hit.collider.gameObject.name == "Potions")
            {
                foreach (Collider2D interatableObject in interatableObjects)
                {
                    interatableObject.enabled = false;
                }

                zoom.zoom = zoom.minZoom;
                potionGamePanel.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (Collider2D interatableObject in interatableObjects)
            {
                interatableObject.enabled = true;
            }

            zoom.zoom = zoom.maxZoom;
            potionGamePanel.SetActive(false);
        }
    }

    private void GameClear()
    {
        if (gameController.GameClear() == true && isGameClear == false)
        {
            isGameClear = true;
            InventoryData.AddItem(InventoryItem);
        }
    }
}
