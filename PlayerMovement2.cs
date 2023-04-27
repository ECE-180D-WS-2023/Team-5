using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 moveDirection;

    // Update is called once per frame
    void Update()
    {
        //player boundary scripts from https://www.youtube.com/watch?v=jkWVj28yWQ4
        if(transform.position.y >= 17f)
        {
            transform.position = new Vector3(transform.position.x, 17f, 0);
        }
        else if(transform.position.y <= 0.5f)
        {
            transform.position = new Vector3(transform.position.x, 0.5f, 0);
        }
        if(transform.position.x >= 15.6f)
        {
            transform.position = new Vector3(15.6f, transform.position.y,0);
        }
        else if(transform.position.x <= -15.6f)
        {
            transform.position = new Vector3(-15.6f, transform.position.y,0);
        }
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        //-1, 0, 1 values
        float moveX = 0;
        float moveY = 0;

        if(Input.GetKey(KeyCode.J))
        {
            moveX = -1;
            
        }
        if(Input.GetKey(KeyCode.L))
        {
            moveX = 1;
            
        }
        if(Input.GetKey(KeyCode.I))
        {
            moveY = 1;
            
        }
        if(Input.GetKey(KeyCode.K))
        {
            moveY = -1;
            
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
    }
    // adding a velocity to the player
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
