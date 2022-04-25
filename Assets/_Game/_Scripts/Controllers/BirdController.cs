using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class BirdController : MonoSingleton<BirdController>
{
    [SerializeField]
    private Bird[] birdPrefabs;

    private Bird[] birds;

    [SerializeField]
    private GameObject throwPoint;


    private int index = 0;
    private Bird currentBird;
    public Bird CurrentBird => currentBird;



    private void Start() 
    {
        birds = new Bird[birdPrefabs.Length];
        for (int i = 0; i < birdPrefabs.Length; i++)
        {
            var bird = Instantiate(birdPrefabs[i],new Vector3(-i*2 - 2,1f,0),Quaternion.identity);
            birds[i] = bird;
        }
        GameManager.Instance.GameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStates newState)
    {
        if (newState == GameStates.Clickable && birdPrefabs.Length > index)
        {
            currentBird = birds[index];
            currentBird.transform.position = throwPoint.transform.position;
            currentBird.Register(throwPoint.GetComponent<Rigidbody2D>());
            index++;
        }
        else if (newState == GameStates.Unclickable && birdPrefabs.Length > index )
        {
            LeanTween.delayedCall(3,()=> GameManager.Instance.UpdateGameState(GameStates.Clickable)); 
        }
        else if (birdPrefabs.Length == index)
        {
            LeanTween.delayedCall(3,()=> GameManager.Instance.UpdateGameState(GameStates.Ended)); 
        }

    
    }
}
