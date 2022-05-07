using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReClickableBird : Bird
{
    protected override void OnSecondClickAction()
    {
        rb.velocity *= 1.5f;
        base.OnSecondClickAction();
    }
}
