using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelController : MonoSingleton<LevelController>
{
    private int currentLevel;
    public int CurrentLevel => currentLevel;

    private void Awake() 
    {
        currentLevel = PlayerPrefs.GetInt("Level");
    }

    private void Start()
    {
        GameManager.Instance.AfterGameStateChanged += OnAfterGameStateChanged;
    }

    private void OnAfterGameStateChanged(GameStates newState)
    {
        if (newState == GameStates.Success)
        {
            if (PlayerPrefs.GetInt("Level") <= SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void SetStarCount(int currentLevelStar)
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex.ToString(), currentLevelStar);
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
