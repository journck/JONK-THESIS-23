
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public Character owner;
    public float moveSpeed;
    public float damage;
    public float knockbackMultiplier = 400f;
    private Rigidbody rigidBody;
    public bool shouldRotate;
    public Vector2 cachedVelocity;
    public bool isPaused = false;
    public Field parentField;
    public MeshRenderer meshRenderer;
    public Color color
    {
        get { return meshRenderer.material.color; }
        set { meshRenderer.material.color = value; }
    }
    


    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        rigidBody = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        if ( parentField != null )
        {
            // need to pause the bullet to match the field.

            if ( !isPaused && parentField.fieldPaused )
            {
                 //Debug.Log("pausing bullet " + Time.time);
                 PauseBullet();
                 return;
            }
            if (isPaused)
            {
                // wait to get unpaused
                if (parentField.fieldPaused)
                {
                    //Debug.Log("waiting to get unpaused");
                    return;
                }
                else if ( !parentField.fieldPaused )
                {
                    //Debug.Log("resuming bullet" + Time.time);
                    ResumeBullet();
                }
            }
        }
    }

    // linear projection
    public void ProjectBullet(Vector3 direction)
    {
        Vector3 vel = direction.normalized * moveSpeed;
        //Debug.Log("projecting bullet");

        // TODO - fix this so it actaully looks @ where it's going
        this.gameObject.transform.rotation = this.owner.gameObject.transform.rotation;
        rigidBody.velocity = vel;
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject otherGO = other.gameObject;

        if (otherGO.CompareTag("Boundary"))
        {
            BulletPooling.instance.ReturnToPool(this);
        }


        Enemy hitEnemy = otherGO.GetComponent<Enemy>();
        Player hitPlayer = otherGO.GetComponent<Player>();
        // player bullet hit enemy
        if ( owner is Player && hitEnemy != null )
        {
            hitEnemy.TakeDamage(this.damage);
            BulletPooling.instance.ReturnToPool(this);
            return;
        }
        // enemy bullet hit player 
        if ( owner is Enemy && hitPlayer != null)
        {
            if ( this.shouldRotate)
            {
                //TODO - should probably make this dependant on where the collision occurred instead of random
                float randomVal = Random.Range(0f, 1f);
                if (randomVal < 0.5f)
                {
                    hitPlayer.playerMovement.TurnPlayer(1);
                }
                else
                {
                    hitPlayer.playerMovement.TurnPlayer(-1);
                }
            }
            hitPlayer.TakeDamage(this.damage);
            BulletPooling.instance.ReturnToPool(this);
            return;
        }
    }

    public void setBulletProperties( Vector3 pos, Quaternion rotation, Color color, float moveSpeed, float damage, bool shouldRotate)
    {
        this.transform.position = pos;
        this.transform.rotation = rotation;
        this.color = color;
        this.moveSpeed = moveSpeed;
        this.damage = damage;
        this.shouldRotate = shouldRotate;
    }

    //public void SetActiveBullet(bool boolean)
    //{
    //    if ( boolean )
    //    {
    //        ResumeBullet();
    //    }
    //    else
    //    {
    //        PauseBullet();
    //    }
    //}

    public void PauseBullet ()
    {
        if (isPaused)
            return;


        cachedVelocity = this.rigidBody.velocity;
        //Debug.Log(" freezing this bullet and caching " + cachedVelocity.ToString());
        this.rigidBody.velocity = Vector2.zero;
        isPaused = true;
    }

    public void ResumeBullet()
    {
        if (!isPaused)
            return;



        if ( cachedVelocity == null)
        {
            Debug.LogError("cachedVelocity for " + this.gameObject + " is null.");
        }

        this.rigidBody.velocity = cachedVelocity;
        //Debug.Log("cachedVelocity " + cachedVelocity.ToString() + " this velocity " + this.rigidBody.velocity);
        isPaused = false;
    }

    private void OnEnable()
    {
        isPaused = false;
    }

    private void OnDisable()
    {
        isPaused = true;
    }
}
