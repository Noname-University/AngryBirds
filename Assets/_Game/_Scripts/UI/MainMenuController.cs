using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private int currentLevel;

    private void Start() 
    {
        currentLevel = PlayerPrefs.GetInt("Level");

        if(currentLevel == 0) currentLevel = 1;

        for (int i = currentLevel; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 1; i <= buttons.Length; i++)
        {
            int temp = i; // closure problem
            buttons[i-1].onClick.AddListener(()=> GetLevel(temp));
            buttons[i-1].transform.GetChild(0).GetComponent<Text>().text = i.ToString();
        }
    }

    public void GetLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void GetCurrentLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }
}
