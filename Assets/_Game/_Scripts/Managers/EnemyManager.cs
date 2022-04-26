using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public class EnemyManager : MonoSingleton<EnemyManager>
{
    private int enemyCount = 0;
    public int EnemyCount
    {
        get => enemyCount;

        set
        {
            enemyCount += value;
             Debug.Log(enemyCount);
            if (EnemyCount == 0)
            {
                GameManager.Instance.UpdateGameState(GameStates.Success);
            }
        }
    }
}
