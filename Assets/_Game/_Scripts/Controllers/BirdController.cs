using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class BirdController : MonoSingleton<BirdController>
{
    [SerializeField]
    private Bird[] birds;

    [SerializeField]
    private GameObject throwPoint;



    private int index = 0;
    private Bird currentBird;
    public Bird CurrentBird => currentBird;



    private void Start() 
    {
        for (int i = 0; i < birds.Length; i++)
        {
            Instantiate(birds[i],new Vector3(-i*2,0.2f,0),Quaternion.identity);
            birds[i].GetComponent<SpringJoint2D>().enabled=false;
        }
        currentBird = birds[index];
        GameManager.Instance.GameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStates newState)
    {
        if(newState == GameStates.Clickable && birds.Length > index)
        {
            Debug.Log("asdasdas");
            currentBird = birds[index];
            currentBird.GetComponent<SpringJoint2D>().enabled=true;
            currentBird.GetComponent<SpringJoint2D>().connectedBody = throwPoint.GetComponent<Rigidbody2D>();
            currentBird.transform.position = throwPoint.transform.position;
            birds[index++].Register();
        }
    }
}
