using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPooling : MonoBehaviour
{

    public static BulletPooling instance;
    public EnemyBullet enemyBulletPrefab;
    public List<EnemyBullet> enemyBulletPool;
    public int initialEnemyBulletSize = 20;
    public int initialPlayerBulletSize = 20;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        InstantiatePools();
    }

    private void InstantiatePools()
    {
        for (int i = 0; i < this.initialEnemyBulletSize; i++)
        {
            EnemyBullet enemyBullet = Instantiate(enemyBulletPrefab);
            enemyBullet.gameObject.SetActive(false);
            enemyBulletPool.Add(enemyBullet);
        }
    }

    public EnemyBullet GetAvailableEnemyBullet()
    {
        foreach (EnemyBullet bullet in enemyBulletPool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }

        // no available bullets, adding to enemyBulletPool
        EnemyBullet newBullet = Instantiate(enemyBulletPrefab);
        newBullet.gameObject.SetActive(true);
        enemyBulletPool.Add(newBullet);
        Debug.Log("enemyBulletPool is full: adding another. current size:"
            + enemyBulletPool.Count);
        return newBullet;
    }

    public void ReturnToPool( EnemyBullet bullet )
    {
        //TODO - there's gotta be a better way to do this man
        bullet.gameObject.SetActive(false);
    }
}
