using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleGameController : MonoBehaviour
{
    public BottleController FirstBottle;
    public BottleController SecondBottle;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
    }
}
