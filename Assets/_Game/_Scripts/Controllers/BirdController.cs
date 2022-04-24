using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using System;

public class BirdController : MonoSingleton<BirdController>
{
    [SerializeField]
    private Bird[] birds;

    private int index = 0;

    private void Start() 
    {
        birds[index++].Register();

        GameManager.Instance.GameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStates newState)
    {
        if(newState == GameStates.Clickable && birds.Length > index)
        {
            birds[index++].Register();
        }
    }
}
