using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using UnityEngine.UI;
using System;

public class ScoreController : MonoSingleton<ScoreController>
{

    [SerializeField]
    private int totalStarScore;

    private int currentScore;
    private int score;

    public int CurrentScore => currentScore;
    public int TotalStarScore => totalStarScore;

    public event Action ScoreIncreased;
    
    public void IncreaseScore(int scoreValue)
    {
        currentScore += scoreValue;
        ScoreIncreased?.Invoke();
    }
}
