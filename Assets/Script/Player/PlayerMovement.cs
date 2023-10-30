using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    public float turnTimeSec = 0.75f;

    private Rigidbody2D rigidBody;
    private Player player;
    private Vector2 moveDirection = Vector2.zero;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private float currentTime = 0.0f;
    public bool isRotating;

    public bool asteroidsMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        startRotation = this.transform.rotation;
        endRotation = startRotation;
    }

    // Update is called once per frame
    void Update()
    {
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
                moveX = asteroidsMovement ? 0f : -1f;

            }
        }
        else if ( Input.GetKey( KeyCode.D ) )
        {
            moveX = asteroidsMovement ? 0f : 1f;
        }

        //vertical movement
        if ( Input.GetKey( KeyCode.W ) )
        {
            moveX = asteroidsMovement ? Mathf.Cos(angleFacingRadians) : 1f;
            moveY = asteroidsMovement ? Mathf.Sin(angleFacingRadians) : 0f;
        }
        else if ( Input.GetKey( KeyCode.S ) )
        {
            moveY = -1f;
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
        endRotation = startRotation * Quaternion.Euler(0, 0, 90*dir);
        currentTime = 0;
        isRotating = true;
    }


    private void FixedUpdate()
    {
        rigidBody.AddForce(moveDirection.normalized * player.moveSpeed);
    }
}
