using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReClickableBird : Bird
{
    public override void OnClicked(TouchPhase phase, Vector2 touchPosition)
    {
            if (!isTouchBird)
            {
                base.OnClicked(phase, touchPosition);
                if(phase == TouchPhase.Ended)
                {
                    GameManager.Instance.UpdateGameState(GameStates.ReClickable);
                }
            }
            else
            {
                if (phase == TouchPhase.Began)
                {
                     trajectoryPrediction.enabled = false;
                }
                else if(phase == TouchPhase.Moved)
                {
                    return;
                }
                else if(phase == TouchPhase.Stationary)
                {
                    return;
                }
                else if(phase == TouchPhase.Ended)
                {
                    rb.velocity *= 3.5f;
                    isTouchBird=false;
                    InputController.Instance.Clicked -= OnClicked;
                    GameManager.Instance.UpdateGameState(GameStates.Unclickable);
                }
            }

        }
}
