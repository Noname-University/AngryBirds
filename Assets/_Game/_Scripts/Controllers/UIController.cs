using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{    
    [SerializeField]
    private CanvasController canvasPrefab;

    private Button nextLevelButton;
    private Button restartButton;
    private GameObject endGamePanel;
    private Button mainMenuButton;

    private void Start() 
    {
        InitCanvas();
        GameManager.Instance.AfterGameStateChanged += OnAfterGameStateChanged;
        
    }

    private void InitCanvas()
    {
        var canvas = Instantiate(canvasPrefab);

        endGamePanel = canvas.EndGamePanel;
        canvas.EndGamePanel.gameObject.SetActive(false);

        nextLevelButton = canvas.NextLevelButton;
        canvas.NextLevelButton.GetComponent<Button>().onClick.AddListener(()=> LevelController.Instance.GetNextLevel());

        restartButton = canvas.RestartButton;
        canvas.RestartButton.GetComponent<Button>().onClick.AddListener(()=> LevelController.Instance.Restart());

        mainMenuButton = canvas.MainMenuButton;
        canvas.MainMenuButton.GetComponent<Button>().onClick.AddListener(()=> LevelController.Instance.GetMainMenu());
    }

    private void OnAfterGameStateChanged(GameStates newState)
    {
        if (newState == GameStates.Success)
        {
            endGamePanel.gameObject.SetActive(true);
        }
        else if(newState == GameStates.Start)
        {
            endGamePanel.gameObject.SetActive(false);
        }
        else if (newState == GameStates.Fail)
        {
            endGamePanel.gameObject.SetActive(true);
            nextLevelButton.interactable = false;
        }
    }  
}
