using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float shootInterval = 1f;
    private Vector2 playerPosition = Vector2.zero;
    public float moveSpeed = 7f;
    public int health = 100;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootAtPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        // look towards the player
        this.transform.up = GameManager.instance.player.transform.position - this.transform.position;
    }

    private void FixedUpdate()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, step);
    }

    IEnumerator ShootAtPlayer()
    {
        while (true)
        {
            ShootAtPosition(playerPosition);
            yield return new WaitForSecondsRealtime(shootInterval);
        }
    }

    public void ShootAtPosition( Vector2 pos)
    {
        Bullet bullet = BulletPooling.instance.GetAvailableBullet();
        bullet.transform.position = this.transform.position + this.transform.up;
        bullet.transform.rotation = this.transform.rotation;

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
