using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : DestroyableBase
{
    private void Start()
    {
        EnemyManager.Instance.EnemyCount = 1;
    }

    public override void Destroy()
    {
        EnemyManager.Instance.EnemyCount = -1;
        base.Destroy();

    }
}
