using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //TODO - consolidate this into the Player class. not really a need to have this separate
    [Header("Inscribed")]
    public float baseTurnTimeSec = 0.75f;

    [Header("Dynamic")]
    private float turnTimeSec;
    private Rigidbody2D rigidBody;
    private Player player;
    private Vector2 moveDirection = Vector2.zero;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private float currentTime = 0.0f;
    public bool isRotating;
    private float moveSpeed;

    public bool asteroidsMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        startRotation = this.transform.rotation;
        Debug.Log(startRotation.eulerAngles);
        endRotation = startRotation;
    }

    // Update is called once per frame
    void Update()
    {
        int abilityScalar = player.activeAbility ? 2 : 1;
        moveSpeed = abilityScalar * (player.baseMoveSpeed + 1.2f * player.upgrades["moveSpeed"]);
        turnTimeSec = baseTurnTimeSec * Mathf.Pow(0.8f, player.upgrades["turnSpeed"]);
        float angleFacingRadians = this.transform.rotation.z * Mathf.Deg2Rad;
        if ( isRotating )
        {
            currentTime += Time.deltaTime;
            float t = currentTime / turnTimeSec;
            this.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);

            if ( t >= 1)
            {
                isRotating = false;
            }
        }
        float moveX = 0f;
        float moveY = 0f;

        // horizontal movement
        if ( Input.GetKey( KeyCode.A ) )
        {
            if (asteroidsMovement)
            {
                //rotate here
            }
            else
            {
                moveX = -1f;
            }
        }
        else if ( Input.GetKey( KeyCode.D ) )
        {
            if (asteroidsMovement)
            {
                //rotate here
            }
            moveX = 1f;
        }

        //vertical movement
        if ( Input.GetKey( KeyCode.W ) )
        {
            if (asteroidsMovement)
            {
                moveX = Mathf.Cos(angleFacingRadians);
                moveY = Mathf.Sin(angleFacingRadians);
            }
            else
            {
                moveY = 1f;
            }
        }
        else if ( Input.GetKey( KeyCode.S ) )
        {
            if (asteroidsMovement)
            {
                //move back here
            }
            else
            {
                moveY = -1f;
            }
        }

        //rotation
        if ( Input.GetKeyDown( KeyCode.Q ) )
        {
            TurnPlayer(1);
        }
        else if ( Input.GetKeyDown( KeyCode.E ) )
        {
            TurnPlayer(-1);
        }
      
        moveDirection = new Vector2( moveX, moveY ).normalized;
    }

    // direction 1 represents left, -1 represents right
    public void TurnPlayer ( int dir )
    {
        //already turning
        if ( isRotating )
        {
            return;
        }
        startRotation = this.transform.rotation;
        float currentX = startRotation.eulerAngles.x;
        float nextX = currentX + 90f * dir;
        Debug.Log("current x rotation " + currentX);
        Debug.Log("end x rotation " + nextX);
        endRotation = startRotation * Quaternion.Euler(1, 90f * dir, 1);
        currentTime = 0;
        isRotating = true;
    }


    private void FixedUpdate()
    {
        rigidBody.AddForce(moveDirection.normalized * moveSpeed);
    }
}
