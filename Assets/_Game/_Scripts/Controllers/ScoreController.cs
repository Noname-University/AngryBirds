using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using TMPro;

public class ScoreController : MonoSingleton<ScoreController>
{
    private int currentScore;


    public void IncreaseScore(int scoreValue)
    {
        currentScore += scoreValue;
    }


}
