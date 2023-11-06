using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Character
{
    [Header("Inscribed")]
    public float baseShotsPerSecond = 0.5f;
    public float positionSendInterval = 1.0f;
    public bool debugShouldShoot = true;

    private BulletSpawner bulletSpawner;
    private HealthBar healthBar;
    private Rigidbody2D rigidBody;
    private Field parentField;

    [Header("Dynamic")]
    public float shotsPerSecond;
    public PlayerMovement playerMovement;
    public IDictionary<string, uint> upgrades = new Dictionary<string, uint>()
    {
        {"shootSpeed", 1 },
        {"moveSpeed", 1 },
        {"turnSpeed", 1 }
    };
    public uint level;
    public float xp;


    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Shoot), 1);
        Invoke(nameof(SendPositionToEnemies), positionSendInterval);
        healthBar = GetComponentInChildren<HealthBar>();
        health = maxHealth;
        healthBar.UpdateSlider(health / maxHealth);
        parentField = GetComponentInParent<Field>();
        playerMovement = GetComponent<PlayerMovement>();
        bulletSpawner = GetComponent<BulletSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        shotsPerSecond = baseShotsPerSecond + 1.2f * upgrades["shootSpeed"];
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shotsPerSecond *= 2;
            bulletSpawner.moveSpeed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            shotsPerSecond /= 2;
            bulletSpawner.moveSpeed /= 2;
        }
    }


    public void SendPositionToEnemies()
    {
        foreach (Enemy enemy in parentField.enemyList)
        {
            enemy.ReceivePlayerPosition(this.transform.position);
        }
        Invoke(nameof(SendPositionToEnemies), positionSendInterval);
    }

    private void Shoot()
    {
        if (!debugShouldShoot)
        {
            Invoke(nameof(Shoot), ( 1 / shotsPerSecond ));
            return;
        }

        this.bulletSpawner.ShootBullet();

        Invoke(nameof(Shoot), ( 1 / shotsPerSecond ));
    }

    public override void TakeDamage(float iDamage)
    {
        base.TakeDamage(iDamage);
        healthBar.UpdateSlider(this.health / this.maxHealth);
    }
}
