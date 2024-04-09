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
    public Animator animator;


    private void Awake()
    {
        bulletSpawner = GetComponentInChildren<BulletSpawner>();
        animator = GetComponent<Animator>();
        Invoke(nameof(Shoot), shootInterval);
        parentField = GetComponentInParent<Field>();
    }

    // Start is called before the first frame update
    void Start()
    {

        //xpValue = GameManager.instance.difficulty;
        xpValue = 1f;
    }

    public float z = 0f;

    // Update is called once per frame
    void Update()
    {
        // look towards the player
        //z = z >= 360f ? 0f : z + 1;
        Vector3 playerPos = this.parentField.player.transform.position;

        float xDist = playerPos.x - this.transform.position.x;
        float yDist = - playerPos.y + this.transform.position.y;
        float xAngle = Mathf.Atan2(yDist, xDist) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler(xAngle, 90f, -90f);
        //Vector3 testingV;

        //Vector3 toPlayer = parentField.player.transform.position - this.transform.position;

        //Debug.DrawLine(this.transform.position, toPlayer, Color.green, 2f);
        //Debug.DrawLine(this.transform.position, this.transform.forward, Color.blue, 2f);
        ////this.transform.forward = Vector3.Lerp(this.transform.forward, parentField.player.transform.position - this.transform.position, turnEasing);
        //this.transform.LookAt(parentField.player.transform.position);
        //transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);

        bool doneDeathAnimation = animator.GetCurrentAnimatorStateInfo(0).IsName("die") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
        if (doneDeathAnimation)
        {
            ExperiencePoint xpPoint = Instantiate(xpPrefab);
            xpPoint.transform.position = this.transform.position;
            xpPoint.playerRef = parentField.player;
            xpPoint.value = this.xpValue;
            base.DoDeath();
        }
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
        animator.SetTrigger("ShootTrigger");
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
        // play the death animation
        animator.SetTrigger("Die");

        //wait for the death animation to finish?

        // spawning xp prefabs


        //base.DoDeath();
    }

    private void OnDestroy()
    {
        this.parentField.enemyList.Remove(this);
    }

}
