using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public event Action<GameStates> GameStateChanged;

    public GameStates GameState => gameState;

    private GameStates gameState;

    private void Start() 
    {
        UpdateGameState(GameStates.Start);
        LeanTween.delayedCall(2, ()=> UpdateGameState(GameStates.Clickable));    
    }

    public void UpdateGameState(GameStates newState)
    {
        gameState = newState;

        GameStateChanged?.Invoke(newState);
    }
}

