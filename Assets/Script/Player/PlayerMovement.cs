using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float turnSpeed = 10f;

    private Rigidbody2D rigidBody;
    private Vector2 moveDirection = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        // horizontal movement
        if ( Input.GetKey( KeyCode.A ) )
        {
            moveX = -1f;
        }
        else if ( Input.GetKey( KeyCode.D ) )
        {
            moveX = 1f;
        }

        //vertical movement
        if ( Input.GetKey( KeyCode.W ) )
        {
            moveY = 1f;
        }
        else if ( Input.GetKey( KeyCode.S ) )
        {
            moveY = -1f;
        }

        //rotation
        if ( Input.GetKeyDown( KeyCode.Q ) )
        {
            turnPlayer(1);
        }
        else if ( Input.GetKeyDown( KeyCode.E ) )
        {
            turnPlayer(-1);
        }
      
        moveDirection = new Vector2( moveX, moveY ).normalized;
    }

    // direction 1 represents left, -1 represents right
    private void turnPlayer ( int dir )
    { 
        rigidBody.rotation += 90f * dir;
    }


    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
