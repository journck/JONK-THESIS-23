using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public Character owner;
    public float moveSpeed;
    public float damage;
    public float knockbackMultiplier = 400f;


    private Rigidbody2D rigidBody;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // linear projection
    public void ProjectBullet(Vector2 direction)
    {
        Vector2 vel = direction.normalized * moveSpeed;
        rigidBody.velocity = vel;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherGO = other.gameObject;

        if (otherGO.CompareTag("Boundary"))
        {
            BulletPooling.instance.ReturnToPool(this);
        }


        Enemy hitEnemy = otherGO.GetComponent<Enemy>();
        if ( hitEnemy != null )
        {
            Debug.Log("yo you hit an enemyy");
        }
        Player hitPlayer = otherGO.GetComponent<Player>();
        if ( owner is Player && hitEnemy != null )
        {
            Debug.Log("PLAYER BULLET HIT ENEMY");
            hitEnemy.TakeDamage(this.damage);
            BulletPooling.instance.ReturnToPool(this);
            return;
        }
        if ( owner is Enemy && hitPlayer != null)
        {
            hitPlayer.TakeDamage(this.damage);
            BulletPooling.instance.ReturnToPool(this);
            return;
        }
    }

    public void setBulletProperties( Vector3 pos, Quaternion rotation, Color color, float moveSpeed, float damage)
    {
        this.transform.position = pos;
        this.transform.rotation = rotation;
        this.GetComponent<SpriteRenderer>().color = color;
        this.moveSpeed = moveSpeed;
        this.damage = damage;
    }
}
