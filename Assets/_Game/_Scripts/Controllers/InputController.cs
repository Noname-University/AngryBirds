using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class InputController : MonoSingleton<InputController>
{
    [SerializeField]
    private Vector2 minPower;

    [SerializeField]
    private Vector2 maxPower;


    private Vector2 force;
    private Vector2 startPoint;
    private Vector2 direction;

    public Vector2 Force => force;

    private void Update() 
    {
        InputControl();    
    }

    private void InputControl()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPoint = touch.position;
                    break;
                case TouchPhase.Moved:
                    direction = touch.position - startPoint;
                    break;
                case TouchPhase.Ended:
                    force = new Vector2
                    (
                        Mathf.Clamp((direction.x)*-1, minPower.x, maxPower.x),
                        Mathf.Clamp((direction.y)*-1, minPower.y, maxPower.y)
                    );
                    break;
            }
            

        }
    }
}
