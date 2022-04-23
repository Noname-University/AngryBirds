using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private Vector2 minPower;

    [SerializeField]
    private Vector2 maxPower;

    [SerializeField]
    private float force;

    private Rigidbody2D rb;

    private Vector2 startTouchPosition;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        InputController.Instance.Clicked += OnClicked;
    }

    private void OnClicked(TouchPhase phase, Vector2 touchPosition)
    {
        switch (phase)
        {
            case TouchPhase.Began:
                startTouchPosition = touchPosition;
                break;
            case TouchPhase.Moved:
                transform.position = touchPosition;
                break;
            case TouchPhase.Stationary:
                transform.position = touchPosition;
                break;
            case TouchPhase.Ended:
                rb.AddForce((startTouchPosition - touchPosition).normalized * force);
                InputController.Instance.Clicked -= OnClicked;
                break;
        }
    }
}
