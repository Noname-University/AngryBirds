using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    private Text mainScore;

    [SerializeField]
    private GameObject[] stars;

    // [SerializeField]
    // private GameObject endGamePanel;

    // [SerializeField]
    // private Button restartButton;

    // [SerializeField]
    // private Button nextLevelButton;

    // [SerializeField]
    // private Button mainMenuButton;

    public Button MainMenuButton;// {get; set;}//=> mainMenuButton;
    public Button NextLevelButton;// {get; set;}//=> nextLevelButton;
    public GameObject EndGamePanel;// {get; set;}//=> endGamePanel;
    public Button RestartButton;// {get; set;}//=> restartButton;

    private void Start()
    {
        GameManager.Instance.AfterGameStateChanged += OnAfterGameStateChanged;
        ScoreController.Instance.ScoreIncreased += OnScoreIncreased;
        CloseAllChilds();
    }

    private void CloseAllChilds()
    {
        stars[0].SetActive(false);
        stars[1].SetActive(false);
        stars[2].SetActive(false);
    }

    private void UpdateScoreText()
    {
        mainScore.text = ScoreController.Instance.CurrentScore.ToString();
    }

    private void OnAfterGameStateChanged(GameStates newState)
    {
        if (newState == GameStates.Success)
        {
            int starCount = 0;
            if (ScoreController.Instance.CurrentScore >= ScoreController.Instance.TotalStarScore / 3)
            {
                stars[0].SetActive(true);
                starCount++;
            }
            if (ScoreController.Instance.CurrentScore >= ScoreController.Instance.TotalStarScore / 2)
            {
                stars[1].SetActive(true);
                starCount++;

            }
            if (ScoreController.Instance.CurrentScore >= ScoreController.Instance.TotalStarScore)
            {
                stars[2].SetActive(true);
                starCount++;
            }
            LevelController.Instance.SetStarCount(starCount);
        }
    }

    private void OnScoreIncreased()
    {
        UpdateScoreText();
    }
}
