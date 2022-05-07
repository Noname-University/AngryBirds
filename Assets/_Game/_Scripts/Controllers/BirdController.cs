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
    private Transform leftLinePoint;

    [SerializeField]
    private Transform rightLinePoint;

    [SerializeField]
    private LineRenderer leftLine;

    [SerializeField]
    private LineRenderer rightLine;

    public Transform LeftLinePoint => leftLinePoint;
    public Transform RightLinePoint => rightLinePoint;



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
            var bird = Instantiate(birdPrefabs[i], new Vector3(-i * 2 - 3, 1f, 0), Quaternion.identity);
            bird.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            birds[i] = bird;
        }
        GameManager.Instance.AfterGameStateChanged += OnGameStateChanged;
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

        if (GameManager.Instance.GameState == GameStates.Clickable)
        {
            SetSlingLines();
        }
    }

    public void SetSlingLines()
    {
        leftLine.SetPosition(0, currentBird.transform.position);
        rightLine.SetPosition(0, currentBird.transform.position);

        leftLine.SetPosition(1, BirdController.Instance.LeftLinePoint.position);
        rightLine.SetPosition(1, BirdController.Instance.RightLinePoint.position);
    }

    private void OnGameStateChanged(GameStates newState)
    {
        if (newState == GameStates.Clickable && birdPrefabs.Length > index)
        {
            currentBird = birds[index];
            currentBird.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            currentBird.transform.position = throwPoint.transform.position;
            currentBird.Register(throwPoint.GetComponent<Rigidbody2D>());
            rightLine.enabled = true;
            leftLine.enabled = true;

            for (int i = index + 1; i < birds.Length; i++)
            {
                birds[i].transform.LeanMoveX(birds[i].transform.position.x + 2, .5f);
            }

        }
        else if (newState == GameStates.Unclickable && birdPrefabs.Length > index)
        {
            timer = 3f;
            isTimerActive = true;
            index++;
            rightLine.enabled = false;
            leftLine.enabled = false;
        }
        else if (birdPrefabs.Length == index)
        {
            timer = 3f;
            isTimerActive = true;

        }
        else if (newState == GameStates.Success)
        {
            isTimerActive = false;
            for (int i = index; i < birds.Length; i++)
            {
                ScoreController.Instance.IncreaseScore(birds[i].GetComponent<Bird>().BirdScore);
            }
        }
        else if (newState == GameStates.ReClickable)
        {
            rightLine.enabled = false;
            leftLine.enabled = false;
        }



    }
}
