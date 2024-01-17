using System;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Inscribed")]
    public Color bulletColor;
    public bool shouldRotatePlayer;
    public float scale;
    public float moveSpeed;
    public float damage;
    public ShootBehavior shootBehavior;

    [Header("Dynamic")]
    public Quaternion spawnRotation;
    public Character character;
    public bool canShoot = true;

    void Start()
    {
        character = GetComponent<Character>();
    }

    public void ShootBullet()
    {
        if (!canShoot)
            return;
        Bullet bullet = BulletPooling.instance.GetAvailableBullet();
        SetProperties(bullet);
        switch ( shootBehavior )
        {
            case ShootBehavior.ShootAtPlayer:
                if (character is Enemy enemy)
                {
                    Vector3 dir = enemy.playerPosition - this.transform.position;
                    bullet.ProjectBullet(dir);
                }
                break;
            case ShootBehavior.ShootForward:
                bullet.ProjectBullet(this.transform.up);
                break;
            default:
                break;
        }

    }

    public enum ShootBehavior
    {
        ShootAtPlayer,
        ShootForward,
        SineWave
    };

    public void SetProperties( Bullet bullet )
    {
        if (bullet == null)
        {
            Debug.Log("bullet is null");
        }
        bullet.owner = character;
        bullet.transform.localScale = new Vector3(this.scale, this.scale, this.scale);
        bullet.color = this.bulletColor;
        bullet.moveSpeed = this.moveSpeed;
        bullet.damage = this.damage;
        bullet.shouldRotate = shouldRotatePlayer;
        bullet.transform.SetPositionAndRotation(this.transform.position, this.spawnRotation);
        bullet.parentField = character.parentField;
    }

    private void OnEnable()
    {
        canShoot = true;
    }

    private void OnDisable()
    {
        canShoot = false;
    }
}
