using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float moveSpeed = 900f;
    public float damage = 10f;
    private Rigidbody2D rigidBody;

    // How far you should bounce back the player when colliding with bullet
    const float BOUNCE_BACK_MULTIPLIER = 4f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void ProjectBullet(Vector2 direction)
    {
        rigidBody.AddForce(direction.normalized * moveSpeed);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        GameObject otherObj = other.gameObject;
        Debug.Log(otherObj.tag);

        if (otherObj.CompareTag("Boundary"))
        {
            BulletPooling.instance.ReturnToPool(this);
        }

        Character target = otherObj.GetComponent<Character>();
        if ( target != null )
        {
            if ( target is Player )
            {
                Vector2 reflectForce = Vector2.Reflect(rigidBody.velocity, other.contacts[0].normal) * BOUNCE_BACK_MULTIPLIER;
                target.GetComponent<Rigidbody2D>().AddForce(reflectForce);
            }
            target.takeDamage(damage);
            BulletPooling.instance.ReturnToPool(this);
        }
    }
}