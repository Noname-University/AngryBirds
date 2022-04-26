using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class BirdController : MonoSingleton<BirdController>
{
    [SerializeField]
    private Bird[] birdPrefabs;

    private Bird[] birds;

    [SerializeField]
    private GameObject throwPoint;

    [SerializeField]
    private int birdScore;


    private int index = 0;
    private Bird currentBird;
    public Bird CurrentBird => currentBird;
    private float timer;
    private bool isTimerActive = false;
    public Transform ThrowPoint => throwPoint.transform;

    private void Start()
    {
        birds = new Bird[birdPrefabs.Length];
        for (int i = 0; i < birdPrefabs.Length; i++)
        {
            var bird = Instantiate(birdPrefabs[i], new Vector3(-i * 2 - 2, 1f, 0), Quaternion.identity);
            birds[i] = bird;
        }
        GameManager.Instance.GameStateChanged += OnGameStateChanged;
    }
    private void Update()
    {
        if (isTimerActive)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0 && birdPrefabs.Length == index)
        {
            GameManager.Instance.UpdateGameState(GameStates.Fail);
            isTimerActive = false;
            Debug.Log("fail");
        }
        if (timer <= 0 && GameManager.Instance.GameState == GameStates.Unclickable)
        {
            GameManager.Instance.UpdateGameState(GameStates.Clickable);
            isTimerActive = false;
        }
    }

    private void OnGameStateChanged(GameStates newState)
    {
        if (newState == GameStates.Clickable && birdPrefabs.Length > index)
        {
            currentBird = birds[index];
            currentBird.transform.position = throwPoint.transform.position;
            currentBird.Register(throwPoint.GetComponent<Rigidbody2D>());

        }
        else if (newState == GameStates.Unclickable && birdPrefabs.Length > index)
        {
            timer = 3f;
            isTimerActive = true;
            index++;

        }
        else if (birdPrefabs.Length == index)
        {
            timer = 3f;
            isTimerActive = true;

        }
        else if (newState == GameStates.Success)
        {
            isTimerActive = false;
            ScoreController.Instance.IncreaseScore((birds.Length - index) * birdScore);
            Debug.Log("succes");
        }


    }
}
