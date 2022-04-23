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


    private Vector2 maxDistance;
    private Rigidbody2D rb;
    private SpringJoint2D sj;

    private Vector2 startTouchPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sj = GetComponent<SpringJoint2D>();
        InputController.Instance.Clicked += OnClicked;
    }

    private void OnClicked(TouchPhase phase, Vector2 touchPosition)
    {
        switch (phase)
        {
            case TouchPhase.Began:
                startTouchPosition = touchPosition;
                sj.enabled = true;
                break;
            case TouchPhase.Moved:

                maxDistance = new Vector2
                (
                Mathf.Clamp(touchPosition.x, minPower.x, maxPower.x),
                Mathf.Clamp(touchPosition.y, minPower.y, maxPower.y)
                );
                transform.position = maxDistance;
                break;
            case TouchPhase.Stationary:
                transform.position = maxDistance;
                break;
            case TouchPhase.Ended:
                rb.velocity = ((startTouchPosition - touchPosition).normalized * force * Vector2.Distance(touchPosition, startTouchPosition));
                sj.enabled = false;
                InputController.Instance.Clicked -= OnClicked;

                break;
        }
    }
}
