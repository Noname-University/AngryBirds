using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public event Action<GameStates> AfterGameStateChanged;
    public event Action<GameStates> BeforeGameStateChanged;


    public GameStates GameState => gameState;

    private GameStates gameState;

    private void Start()
    {
        UpdateGameState(GameStates.Start);
        LeanTween.delayedCall(4, () => UpdateGameState(GameStates.Clickable));
    }

    public void UpdateGameState(GameStates newState)
    {
        BeforeGameStateChanged?.Invoke(gameState);

        gameState = newState;

        AfterGameStateChanged?.Invoke(newState);
    }
}

