using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleGameController : MonoBehaviour
{
    [SerializeField] BottleController[] bottles;

    [Space]
    public BottleController FirstBottle;
    public BottleController SecondBottle;

    private bool isGameClear = false;

    private void InitBottles()
    {
        foreach (BottleController bottle in bottles)
        {
            bottle.InitBottle();
        }
    }

    private void TransferPotion()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            BottleController hitObjectBottleController = hit.collider.GetComponent<BottleController>();

            if (hitObjectBottleController != null)
            {
                if (FirstBottle == null)
                {
                    FirstBottle = hitObjectBottleController;
                }
                else
                {
                    if (FirstBottle == hitObjectBottleController)
                    {
                        FirstBottle = null;
                    }
                    else
                    {
                        SecondBottle = hitObjectBottleController;
                        FirstBottle.bottleControllerRef = SecondBottle;

                        FirstBottle.UpdateTopColorValues();
                        SecondBottle.UpdateTopColorValues();

                        if (SecondBottle.FillBottleCheck(FirstBottle.topColor) == true)
                        {
                            FirstBottle.StartColorTransfer();
                        }

                        FirstBottle = null;
                        SecondBottle = null;
                    }
                }
            }
        }
    }

    public bool GameClear()
    {
        foreach (BottleController bottle in bottles)
        {
            if (bottle.IsBottleAllSameColor() == false)
            {
                return false;
            }

        }

        isGameClear = true;
        return isGameClear;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameClear == false)
        {
            InitBottles();
        }

        if (Input.GetMouseButtonDown(0))
        {
            TransferPotion();
        }
    }
}
