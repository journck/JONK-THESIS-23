using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public GameObject bulletPrefab;
    // interval in between bullet shots
    public float shootInterval = 1f;
    public float positionSendInterval = 1.0f;
    public HealthBar healthBar;

    private Rigidbody2D rigidBody;
    private Field parentField;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Shoot), shootInterval);
        Invoke(nameof(SendPositionToEnemies), positionSendInterval);
        healthBar = GetComponentInChildren<HealthBar>();
        health = maxHealth;
        healthBar.UpdateSlider(health / maxHealth);
        parentField = GetComponentInParent<Field>();
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
            Debug.Log("sendingposition");
            enemy.ReceivePlayerPosition(this.transform.position);
        }
        Invoke(nameof(SendPositionToEnemies), positionSendInterval);
    }

    private void Shoot()
    {
        Invoke(nameof(Shoot), shootInterval);
    }

    public override void takeDamage(float iDamage)
    {
        base.takeDamage(iDamage);
        healthBar.UpdateSlider(this.health / this.maxHealth);
    } 
}
