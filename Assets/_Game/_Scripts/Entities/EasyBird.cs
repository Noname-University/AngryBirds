using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBird : Bird
{

    public override void OnClicked(TouchPhase phase, Vector2 touchPosition)
    {
        base.OnClicked(phase, touchPosition);
        if (phase == TouchPhase.Ended)
        {
            InputController.Instance.Clicked -= OnClicked;
            GameManager.Instance.UpdateGameState(GameStates.Unclickable);

        }
    }
}
