using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bird : MonoBehaviour
{
    #region SerializeFields

    [SerializeField]
    protected float maxLenght;

    [SerializeField]
    protected float force;


    #endregion

    #region Varriables

    private Vector2 maxDistance;
    protected Rigidbody2D rb;
    protected SpringJoint2D springJoint;
    protected LineRenderer trajectoryPrediction;
    protected Vector2 startTouchPosition;
    protected bool isTouchBird=false;

    #endregion

    #region Unity Methods

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();
        trajectoryPrediction = GetComponent<LineRenderer>();

        trajectoryPrediction.enabled = false;
        rb.velocity = Vector2.zero;

        GetComponent<SpringJoint2D>().enabled = false;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        var destroyable = other.gameObject.GetComponent<DestroyableBase>();
        if (destroyable != null)
        {
            destroyable.Hit(rb.velocity.magnitude);
        }
    }

    #endregion

    #region Methods
    public void Register(Rigidbody2D throwPoint)
    {
        GetComponent<SpringJoint2D>().enabled = true;
        GetComponent<SpringJoint2D>().connectedBody = throwPoint;

        InputController.Instance.Clicked += OnClicked;
    }

    private Vector2[] Plot(Vector2 position, Vector2 velocity, int steps)
    {
        Vector2[] result = new Vector2[steps];

        float timeStep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rb.gravityScale * timeStep * timeStep;

        float drag = 1f - timeStep * rb.drag;
        Vector2 moveStep = velocity * timeStep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            position += moveStep;
            result[i] = position;
        }

        return result;
    }

    protected void SetTrajectoryPositions(Vector2 touchPosition, Vector2 startTouchPosition)
    {
        Vector2 velocity = (startTouchPosition - touchPosition).normalized * force * Vector2.Distance(touchPosition, startTouchPosition);
        Vector2[] trajector = Plot((Vector2)transform.position, velocity, 500);
        trajectoryPrediction.positionCount = trajector.Length;
        Vector3[] positions = new Vector3[trajector.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = trajector[i];
        }
        trajectoryPrediction.SetPositions(positions);
    }


    #endregion

    #region Callbacks
    public virtual void OnClicked(TouchPhase phase, Vector2 touchPosition)
    {
        var desiredPosition = touchPosition - startTouchPosition;
        desiredPosition = Vector2.ClampMagnitude(desiredPosition, maxLenght);
        switch (phase)
        {
            case TouchPhase.Began:
                startTouchPosition = touchPosition; //(Vector2)BirdController.Instance.ThrowPoint.position;
                trajectoryPrediction.enabled = true;
                break;
            case TouchPhase.Moved:
                transform.position = startTouchPosition + desiredPosition;
                SetTrajectoryPositions(touchPosition, startTouchPosition);
                break;
            case TouchPhase.Stationary:
                transform.position = startTouchPosition + desiredPosition;
                break;
            case TouchPhase.Ended:
                rb.velocity = ((startTouchPosition - touchPosition).normalized * force * Vector2.Distance(transform.position, startTouchPosition));
                trajectoryPrediction.enabled = false;
                springJoint.enabled = false;
                isTouchBird=true;
                break;

        }

    }


    #endregion

}
