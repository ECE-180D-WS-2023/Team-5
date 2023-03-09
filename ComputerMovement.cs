using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMovement : MonoBehaviour
{
    public Rigidbody2D ball;
    public float speed = 10.0f;
    public Rigidbody2D computer;

    private void FixedUpdate()
    {
        if(this.ball.velocity.y > 0.0f)
        {
            if(this.ball.position.x > this.transform.position.x)
            {
                computer.AddForce(Vector2.right * speed);
            }
            else if(this.ball.position.x < this.transform.position.x)
            {
                computer.AddForce(Vector2.left * speed);
            }
        }
        else
        {
            if(this.transform.position.x > 0.0f)
            {
                computer.AddForce(Vector2.left * speed);
            }
            else if(this.transform.position.x < 0.0f)
            {
                computer.AddForce(Vector2.right * speed);
            }
        }
    }
}
