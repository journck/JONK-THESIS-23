using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 900f;
    public float damage = 10f;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void ProjectBullet(Vector2 direction)
    {
        rigidBody.AddForce(direction.normalized * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BulletPooling.instance.ReturnToPool(this);
    }
}