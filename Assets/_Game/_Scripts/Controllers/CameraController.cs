using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
    
{   
    [SerializeField] 
    private Transform obstaclePosition;
    private Camera mainCamera;

    private Vector3 startPosition;
    

    private void Start() 
    {
        mainCamera = Camera.main;
        startPosition = mainCamera.transform.position;
    }
    
    private void FixedUpdate() 
    {
        CameraMovement();
    }
    private void CameraMovement()
    {
        switch (GameManager.Instance.GameState)
        {
            case GameStates.Start:
                mainCamera.transform.position = obstaclePosition.position;
            break;
            case GameStates.Clickable:
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,startPosition,0.125f);
            break;
            case GameStates.Unclickable:

                if(BirdController.Instance.CurrentBird != null)
                    if (BirdController.Instance.CurrentBird.transform.position.x >= 5)
                    {
                        mainCamera.transform.position = Vector3.Lerp(new Vector3(mainCamera.transform.position.x,4.33f,-10),new Vector3(BirdController.Instance.CurrentBird.transform.position.x,4.33f,-10),0.125f);
                    }
            break;
            case GameStates.Ended:
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,obstaclePosition.position,0.05f);
            break;
        }
        
    }
}
