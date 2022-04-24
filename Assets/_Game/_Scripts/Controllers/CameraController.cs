using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    
    private void FixedUpdate() 
    {
        CameraMovement();
    }
    private void CameraMovement()
    {
        if (BirdController.Instance.CurrentBird.transform.position.x >= 0)
        {
            //mainCamera.transform.position = new Vector3(bird.transform.position.x,0,-10);
            mainCamera.transform.position = Vector3.Lerp(new Vector3(mainCamera.transform.position.x,4.33f,-10),new Vector3(BirdController.Instance.CurrentBird.transform.position.x,4.33f,-10),0.125f);
        }
    }
}
