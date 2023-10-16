using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float shootInterval = 1f;
    private Vector2 playerPosition = Vector2.zero;
    public float bulletMoveSpeed = 30f;
    public float bulletDamage = 10f;
    public float turnEasing = 0.05f;
    public float moveEasing = 0.05f;

    public Field parentField;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(ShootAtPlayer), shootInterval);
        parentField = GetComponentInParent<Field>();
    }

    // Update is called once per frame
    void Update()
    {
        // look towards the player
        this.transform.up = Vector3.Lerp(this.transform.up, parentField.player.transform.position - this.transform.position, turnEasing);
    }

    private void FixedUpdate()
    {
        // move towards the player
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, step);
    }

    public void ShootAtPlayer()
    {   
        ShootAtPosition(playerPosition);
        Invoke(nameof(ShootAtPlayer), shootInterval);
    }

    public void ShootAtPosition( Vector2 pos)
    {
        Bullet bullet = BulletPooling.instance.GetAvailableBullet();
        bullet.setBulletProperties(this.transform.position + this.transform.up,
            this.transform.rotation,
            Color.red, bulletMoveSpeed, bulletDamage);

        // you're already looking @ the player, so you should shoot @ it.
        bullet.ProjectBullet(this.transform.up);
    }

    public void ReceivePlayerPosition (Vector2 pos)
    {
        playerPosition = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D testing = collision.otherCollider;
    }
}
