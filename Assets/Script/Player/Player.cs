using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Inscribed")]
    public float shootInterval = 1f;
    public float positionSendInterval = 1.0f;
    public bool debugShouldShoot = true;

    private HealthBar healthBar;
    private Rigidbody2D rigidBody;
    private Field parentField;

    [Header("Dynamic")]
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Shoot), shootInterval);
        Invoke(nameof(SendPositionToEnemies), positionSendInterval);
        healthBar = GetComponentInChildren<HealthBar>();
        health = maxHealth;
        healthBar.UpdateSlider(health / maxHealth);
        parentField = GetComponentInParent<Field>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.LeftShift))
        {
            shootInterval /= 2;
            moveSpeed *= 2;
        }
        if ( Input.GetKeyUp(KeyCode.LeftShift))
        {
            shootInterval *= 2;
            moveSpeed /= 2;
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
            Invoke(nameof(Shoot), shootInterval);
            return;
        }


        Bullet availableBullet = BulletPooling.instance.GetAvailableBullet();

        availableBullet.setBulletProperties(this.transform.position + this.transform.up,
            this.transform.rotation,
            Color.green,
            bulletMoveSpeed,
            bulletDamage, false);

        availableBullet.owner = this;

        availableBullet.ProjectBullet(this.transform.up);

        Invoke(nameof(Shoot), shootInterval);
    }

    public override void TakeDamage(float iDamage)
    {
        base.TakeDamage(iDamage);
        healthBar.UpdateSlider(this.health / this.maxHealth);
    }

    public void randomRotate()
    {

    }
}
