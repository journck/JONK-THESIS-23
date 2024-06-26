using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Character
{
    [Header("Inscribed")]
    public float baseShotsPerSecond = 0.5f;
    public float positionSendInterval = 1.0f;
    public bool debugShouldShoot = true;
    //maximum distance that xp points will lerp towards player
    public float maxSuckDist = 4f;

    public AudioClip shootSFX;
    public SoundController soundController;

    private BulletSpawner bulletSpawner;
    private Bar healthBar;
    private Bar expBar;

    private Rigidbody2D rigidBody;

    [Header("Dynamic")]
    public float shotsPerSecond;
    public PlayerMovement playerMovement;
    public bool activeAbility = false;
    public IDictionary<string, uint> upgrades = new Dictionary<string, uint>()
    {
        {"shootSpeed", 1 },
        {"moveSpeed", 1 },
        {"turnSpeed", 1 }
    };
    public uint level = 0;
    public float xp;
    public float xpForNextLevel {
        get { return Utility.ExpForLevel(this.level + 1); }
        set { }
    }


    const float shotsMultiplier = 1.2f;


    // Start is called before the first frame update

    void Awake()
    {
        //soundController = GameManager.instance.gameObject.GetComponent<SoundController>();
    }

    void Start()
    {
        Invoke(nameof(Shoot), 1);
        Invoke(nameof(SendPositionToEnemies), positionSendInterval);
        health = maxHealth;
        parentField = GetComponentInParent<Field>();
        healthBar = parentField.healthBar;
        healthBar.UpdateImg(health / maxHealth);
        expBar = parentField.expBar;
        expBar.UpdateImg(0);

        playerMovement = GetComponent<PlayerMovement>();
        bulletSpawner = GetComponentInChildren<BulletSpawner>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO - make this not recalculated more than it needs to

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            activeAbility = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            activeAbility = false;
        }
        int abilityScalar = activeAbility ? 2 : 1;
        shotsPerSecond = abilityScalar * (baseShotsPerSecond + shotsMultiplier * upgrades["shootSpeed"]);
        

        //Vector3 debugCircleVector = new Vector3 ( maxSuckDist, 0, 0 );
        //Debug.DrawLine(this.transform.position, this.transform.position + debugCircleVector, Color.red, Time.deltaTime);
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
            Invoke(nameof(Shoot), ( 1 / shotsPerSecond ));
            return;
        }

        soundController.PlaySound(shootSFX, 0.3f );
        this.bulletSpawner.ShootBullet();

        Invoke(nameof(Shoot), ( 1 / shotsPerSecond ));
    }

    public override void TakeDamage(float iDamage)
    {
        base.TakeDamage(iDamage);
        healthBar.UpdateImg(this.health / this.maxHealth);
    }

    public void GainXP ( float value )
    {
        //Debug.Log("gained " + value + " exp");
        this.xp += value;
        if ( this.xp >= xpForNextLevel )
        {
            GainLevel();
        }
        float progress = this.xp / this.xpForNextLevel;
        expBar.UpdateImg(progress);
    }

    public void GainLevel ()
    {
        while ( this.xp >= xpForNextLevel)
        {
            this.level++;
            //Debug.Log ("level up");
        }

        //show upgrade screen and options here.
        Utility.SetActiveGOAndChildren(this.parentField.gameObject, false);
        parentField.upgradeScreen.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        activeAbility = false;
    }

    public override void DoDeath()
    {
        soundController.PlaySound(base.deathSFX);
        base.DoDeath();
    }
}
