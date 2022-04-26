using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class InputController : MonoSingleton<InputController>
{
    public event Action<TouchPhase, Vector2> Clicked;

    private void Update()
    {
        InputControl();
    }

    private void InputControl()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit)
            {
                if (hit.collider.gameObject.GetComponent<Bird>() != null)
                {
                    Debug.Log(Camera.main.ScreenToWorldPoint(touch.position));
                    Clicked?.Invoke(touch.phase, Camera.main.ScreenToWorldPoint(touch.position));
                }
            }
        }
    }
}
