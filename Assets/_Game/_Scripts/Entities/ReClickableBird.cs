using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReClickableBird : Bird
{
    public override void OnClicked(TouchPhase phase, Vector2 touchPosition)
    {
        base.OnClicked(phase, touchPosition);
        if (phase == TouchPhase.Ended)
        {
            GameManager.Instance.UpdateGameState(GameStates.ReClickable);
        }

    }
}
