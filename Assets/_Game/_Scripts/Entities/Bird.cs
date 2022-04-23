using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private float power;

    [SerializeField]
    private Vector2 minPower;
    [SerializeField]
    private Vector2 maxPower;
    private Vector2 force;
    private Vector2 startPoint;
    private Vector2 endPoint;

    private Rigidbody2D rb;

    private void Update()
    {


        if (Input.touchCount > 0)
        {
            startPoint = Input.GetTouch(0).position;
            Debug.Log(startPoint);
        }
        else
        {
            endPoint = Input.GetTouch(0).position;
            force = new Vector2
            (
                Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
                Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y)
            );
            rb.AddForce(force * power, ForceMode2D.Impulse);
        }

    }
}
