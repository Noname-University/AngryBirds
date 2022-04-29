using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelController : MonoBehaviour
{
    [SerializeField]
    private GameObject endGamePanel;

    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button nextLevelButton;
    [SerializeField]
    private Button getMainMenuButton;

    private void Start()
    {
        endGamePanel.gameObject.SetActive(false);
        GameManager.Instance.AfterGameStateChanged += OnAfterGameStateChanged;

    }

    private void OnAfterGameStateChanged(GameStates newState)
    {
        if (newState == GameStates.Success)
        {

            if (PlayerPrefs.GetInt("level") <= SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
            }
            GoNextLevel();
        }
        else if (newState == GameStates.Fail)
        {
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        endGamePanel.gameObject.SetActive(true);
        nextLevelButton.interactable = false;
    }

    private void GoNextLevel()
    {
        endGamePanel.gameObject.SetActive(true);
        restartButton.interactable = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GetNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void GetMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
