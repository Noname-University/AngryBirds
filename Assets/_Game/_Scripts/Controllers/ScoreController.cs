using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using UnityEngine.UI;
using System;

public class ScoreController : MonoSingleton<ScoreController>
{
    [SerializeField]
    private Text mainScore;
    [SerializeField]
    private int starScore;
    [SerializeField]
    private GameObject starPrefab;

    private int currentScore;
    private int score;

    private void Start()
    {
        GameManager.Instance.AfterGameStateChanged += OnAfterGameStateChanged;
        CloseAllChilds();
    }

    private void OnAfterGameStateChanged(GameStates newState)
    {

        if (newState == GameStates.Success)
        {
            StartCoroutine(waitSeconds());
        }
    }

    private IEnumerator waitSeconds()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log(currentScore + "bu skor");
        starPrefab.transform.GetChild(0).gameObject.SetActive(true);
        if (currentScore >= starScore / 3)
        {
            starPrefab.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (currentScore >= starScore / 2)
        {
            starPrefab.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (currentScore >= starScore)
        {
            starPrefab.transform.GetChild(3).gameObject.SetActive(true);

        }
    }

    public void IncreaseScore(int scoreValue)
    {
        currentScore += scoreValue;
        mainScore.text = currentScore.ToString();

    }
    private void CloseAllChilds()
    {
        starPrefab.transform.GetChild(1).gameObject.SetActive(false);
        starPrefab.transform.GetChild(2).gameObject.SetActive(false);
        starPrefab.transform.GetChild(3).gameObject.SetActive(false);
    }



}
