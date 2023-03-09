using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=AcpaYq0ihaM for code on resetting the ball at the center with a random force

public class Ball : MonoBehaviour
{
    public float speed = 100.0f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        ResetPosition();
        AddStartingForce();
    }

    // place ball back in center
    public void ResetPosition()
    {
        rb.position = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    // give a random starting force when the ball is reset
    public void AddStartingForce()
    {
        float x = Random.value < 0.5f ? Random.Range(-1.0f, -0.5f) : Random.Range(0.5f, 1.0f);
        float y = Random.value < 0.5f ? -1.0f : 1.0f;

        Vector2 direction = new Vector2(x, y);
        rb.AddForce(direction * this.speed);
    }
}
