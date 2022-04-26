using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using System;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    private int enemyCount;
    public int EnemyCount
    {
        get => enemyCount;

        set
        {

            enemyCount += value;
            // Debug.Log(enemyCount);
            if (EnemyCount == 0)
            {
                GameManager.Instance.UpdateGameState(GameStates.Success);
            }
        }
    }
}
