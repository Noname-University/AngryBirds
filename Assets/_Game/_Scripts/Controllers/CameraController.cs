using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour

{
    private Camera mainCamera;

    [SerializeField]
    private float minX;

    [SerializeField]
    private float maxX;

    [SerializeField]
    private float yPos;

    private void FixedUpdate()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        switch (GameManager.Instance.GameState)
        {
            case GameStates.Start:
                InitCamPosition();
                break;
            case GameStates.Clickable:
                GoToStartPosition();
                break;
            case GameStates.Unclickable:
                FollowTheBird();
                break;
            case GameStates.Success:
                GoToObstacles();
                break;
            case GameStates.Fail:
                GoToObstacles();
                break;
        }
    }

    private void InitCamPosition()
    {
        mainCamera = Camera.main;

        mainCamera.transform.position = new Vector3
        (
            maxX,
            yPos,
            -10
        );
    }

    private void GoToObstacles()
    {
        mainCamera.transform.position = Vector3.Lerp
        (
            mainCamera.transform.position, 
            mainCamera.transform.position = new Vector3
            (
                maxX,
                mainCamera.transform.position.y,
                mainCamera.transform.position.z
            ), 
            0.05f
        );
    }

    private void GoToStartPosition()
    {
        mainCamera.transform.position = Vector3.Lerp
        (
            mainCamera.transform.position, 
            new Vector3
            (
                minX,
                mainCamera.transform.position.y,
                mainCamera.transform.position.z
            ), 
            0.125f
        );
    }

    private void FollowTheBird()
    {
        if (BirdController.Instance.CurrentBird != null)
        {
            if (BirdController.Instance.CurrentBird.transform.position.x >= 5)
            {
                mainCamera.transform.position = Vector3.Lerp
                (
                    new Vector3(mainCamera.transform.position.x, yPos, -10), 
                    new Vector3(Mathf.Clamp(BirdController.Instance.CurrentBird.transform.position.x,minX,maxX), yPos, -10),
                    0.125f
                );
            }
        }
    }
}
