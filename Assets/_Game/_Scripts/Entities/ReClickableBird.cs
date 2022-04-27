using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReClickableBird : Bird
{
    protected override void OnSecondClickAction(TouchPhase phase, Vector2 touchPosition)
    {
        rb.velocity *= 3.5f;
        base.OnSecondClickAction(phase, touchPosition);
    }
}
