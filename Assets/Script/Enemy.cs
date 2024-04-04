using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public BulletSpawner bulletSpawner;
    public float shootInterval = 1f;
    public Vector3 playerPosition = Vector2.zero;
    public float turnEasing = 0.05f;
    public float moveEasing = 0.05f;
    private float xpValue = 0f;
    public ExperiencePoint xpPrefab;


    private void Awake()
    {
        bulletSpawner = GetComponentInChildren<BulletSpawner>();
        Invoke(nameof(Shoot), shootInterval);
        parentField = GetComponentInParent<Field>();
    }

    // Start is called before the first frame update
    void Start()
    {
   
        xpValue = GameManager.instance.difficulty;
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
        float step = baseMoveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, step);
    }

    public void Shoot()
    {
        bulletSpawner.ShootBullet();
        Invoke(nameof(Shoot), shootInterval);
    }

    //public void ShootAtPosition( Vector2 pos)
    //{
    //    Bullet bullet = BulletPooling.instance.GetAvailableBullet();
    //    bullet.setBulletProperties(this.transform.position + this.transform.up,
    //        this.transform.rotation,
    //        Color.red, bulletMoveSpeed, bulletDamage, true);

    //    bullet.owner = this;

    //    // you're already looking @ the player, so you should shoot @ it.
    //    bullet.ProjectBullet(this.transform.up);
    //}

    public void ReceivePlayerPosition (Vector2 pos)
    {
        playerPosition = pos;
    }

    public override void DoDeath()
    {
        // spawning xp prefabs
        ExperiencePoint xpPoint = Instantiate(xpPrefab);
        xpPoint.transform.position = this.transform.position;
        xpPoint.playerRef = parentField.player;
        xpPoint.value = this.xpValue;

        base.DoDeath();
    }

    private void OnDestroy()
    {
        this.parentField.enemyList.Remove(this);
    }
}
