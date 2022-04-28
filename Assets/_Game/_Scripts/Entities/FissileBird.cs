using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FissileBird : Bird
{
    protected override void OnSecondClickAction(TouchPhase phase, Vector2 touchPosition)
    {
        for (int i = 0; i < 2; i++)
        {
            var bird =
            Instantiate
             (
             transform, new Vector2
             (
                 transform.position.x + 1f + i,
                 transform.position.y + 1f + i
             ),
             Quaternion.identity);

            bird.gameObject.GetComponent<Rigidbody2D>().velocity = (rb.velocity);





        }

        base.OnSecondClickAction(phase, touchPosition);
    }
}
