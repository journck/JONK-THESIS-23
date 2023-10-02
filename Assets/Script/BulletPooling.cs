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


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        InstantiatePool();
    }

    private void InstantiatePool()
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
        return newBullet;
    }

    public void ReturnToPool( Bullet bullet )
    {
        //TODO - there's gotta be a better way to do this man
        bullet.gameObject.layer = LayerMask.NameToLayer("Bullet");
        bullet.gameObject.SetActive(false);
    }
}
