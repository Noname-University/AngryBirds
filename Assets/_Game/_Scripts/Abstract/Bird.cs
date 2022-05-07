using System;
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

    [SerializeField]
    private int birdScore;


    #endregion

    #region Varriables

    private Vector2 maxDistance;
    protected Rigidbody2D rb;
    protected SpringJoint2D springJoint;
    protected LineRenderer trajectoryPrediction;
    protected Vector2 startTouchPosition;
    private Vector2 throwPoint;

    private bool isOnAir = false;

    public int BirdScore => birdScore;

    //private GameStates before;
    //private GameStates after;

    #endregion

    #region Unity Methods

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();
        trajectoryPrediction = GetComponent<LineRenderer>();

        trajectoryPrediction.enabled = false;
        rb.velocity = Vector2.zero;
        throwPoint = (Vector2)BirdController.Instance.ThrowPoint.position;

        GetComponent<SpringJoint2D>().enabled = false;
    }

    private void Update() 
    {
        if(isOnAir)
        {
            Vector3 diff = (transform.position + (Vector3)rb.velocity) - transform.position;
            diff.Normalize();
            float rotZ = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0, rotZ);
        }
    }

    private void OnBeforeGameStateChanged(GameStates oldState)
    {
        if (oldState == GameStates.Unclickable)
        {
            InputController.Instance.SecondClicked -= OnSecondClickAction;
            GameManager.Instance.BeforeGameStateChanged -= OnBeforeGameStateChanged;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var destroyable = other.gameObject.GetComponent<DestroyableBase>();
        if (destroyable != null)
        {
            destroyable.Hit(rb.velocity.magnitude);
        }
        isOnAir = false;
    }

    #endregion

    #region Methods

    protected virtual void OnSecondClickAction()
    {
        InputController.Instance.SecondClicked -= OnSecondClickAction;
        GameManager.Instance.BeforeGameStateChanged -= OnBeforeGameStateChanged;
    }

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
        var desiredPosition = touchPosition - (Vector2)BirdController.Instance.ThrowPoint.position;
        desiredPosition = Vector2.ClampMagnitude(desiredPosition, maxLenght);
        switch (phase)
        {
            case TouchPhase.Began:
                trajectoryPrediction.enabled = true;
                SetTrajectoryPositions(touchPosition, throwPoint);
                break;
            case TouchPhase.Moved:
                transform.position = throwPoint + desiredPosition;
                SetTrajectoryPositions(touchPosition, throwPoint);
                break;
            case TouchPhase.Stationary:
                transform.position = throwPoint + desiredPosition;
                break;
            case TouchPhase.Ended:
                rb.velocity = desiredPosition * -force;
                trajectoryPrediction.enabled = false;
                springJoint.enabled = false;
                InputController.Instance.Clicked -= OnClicked;
                GameManager.Instance.UpdateGameState(GameStates.Unclickable);
                InputController.Instance.SecondClicked += OnSecondClickAction;
                GameManager.Instance.BeforeGameStateChanged += OnBeforeGameStateChanged;
                isOnAir = true;
                break;
        }
    }

    #endregion

}
