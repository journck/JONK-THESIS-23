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
        Vector2 force = direction.normalized * moveSpeed;
        rigidBody.velocity = force;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        GameObject otherGO = other.gameObject;

        if (otherGO.CompareTag("Boundary"))
        {
            BulletPooling.instance.ReturnToPool(this);
        }

        Character target = otherGO.GetComponent<Character>();
        if (target != null && target != owner)
        {
            if (target is Player)
            {
                (target as Player).takeDamage(this.damage);
            }
        }
        BulletPooling.instance.ReturnToPool(this);
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
