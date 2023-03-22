using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=u8tot-X_RBI for code on 2D top down movement

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 moveDirection;

    // Update is called once per frame
    void Update()
    {
        //player boundary scripts from https://www.youtube.com/watch?v=jkWVj28yWQ4
        if(transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if(transform.position.y <= -16.5f)
        {
            transform.position = new Vector3(transform.position.x, -16.5f, 0);
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
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }
    // adding a velocity to the player
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
