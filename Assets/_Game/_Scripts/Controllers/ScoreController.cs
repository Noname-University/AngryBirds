using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using UnityEngine.UI;

public class ScoreController : MonoSingleton<ScoreController>
{
    [SerializeField]
    private Text mainScore;
    private int currentScore;



    public void IncreaseScore(int scoreValue)
    {
        currentScore += scoreValue;
        mainScore.text = currentScore.ToString();
    }


}
