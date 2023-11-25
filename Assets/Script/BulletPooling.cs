using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPooling : MonoBehaviour
{

    public static BulletPooling instance;
    public Bullet bulletPrefab;
    public List<Bullet> bulletPool;
    public int initialSize = 20;
    public Vector3 cachedVelocity;


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
        for (int i = 0; i < this.initialSize; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public Bullet GetAvailableBullet()
    {
        foreach (Bullet bullet in bulletPool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }

        // no available bullets, adding to bulletPool
        Bullet newBullet = Instantiate(bulletPrefab);
        newBullet.gameObject.SetActive(true);
        bulletPool.Add(newBullet);
        Debug.Log("bulletPool is full: adding another. current size:"
            + bulletPool.Count);
        return newBullet;
    }

    public void ReturnToPool( Bullet bullet )
    {
        bullet.gameObject.SetActive(false);
    }
}
