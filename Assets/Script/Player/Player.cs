using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    // interval in between bullet shots
    public float shootInterval = 1f;
    public float positionSendInterval = 1.0f;

    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ConstantShooting());
        StartCoroutine(SendPositionToEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.LeftShift))
        {
            shootInterval /= 2;
        }
        if ( Input.GetKeyUp(KeyCode.LeftShift))
        {
            shootInterval *= 2;
        }
    }

    IEnumerator ConstantShooting()
    {
        while ( true )
        {
            Shoot();
            yield return new WaitForSecondsRealtime(shootInterval);
        }
    }

    IEnumerator SendPositionToEnemies()
    {
        while ( true )
        {
            //TODO - make this a global array with enemies 
            Enemy[] enemies = FindObjectsOfType(typeof(Enemy)) as Enemy[];
            foreach (Enemy enemy in enemies)
            {
                enemy.ReceivePlayerPosition(this.transform.position);
            }
            yield return new WaitForSecondsRealtime(positionSendInterval);
        }
    }

    private void Shoot()
    {
        Bullet bullet = BulletPooling.instance.GetAvailableBullet();
        bullet.transform.position = this.transform.position + this.transform.up;
        bullet.transform.rotation = this.transform.rotation;
        bullet.gameObject.layer = LayerMask.NameToLayer("FriendlyBullet");
        bullet.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        bullet.ProjectBullet(transform.up);
    }
}
