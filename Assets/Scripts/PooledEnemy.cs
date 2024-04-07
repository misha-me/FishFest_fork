using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledEnemy : MonoBehaviour
{
    private EnemySpawner enemyPool;
    public EnemySpawner EnemyPool { get => enemyPool; set => enemyPool = value; }
    public void Release()
    {
        enemyPool.ReturnToPool(this);
    }
}
