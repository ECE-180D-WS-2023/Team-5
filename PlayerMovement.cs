using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// https://www.youtube.com/watch?v=u8tot-X_RBI for code on 2D top down movement

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    private Vector2 moveDirection;

    public List<string> faceTrack;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    string readFile(){
        var lines = System.IO.File.ReadAllLines("/Users/sraavyapradeep/180DA-WarmUp/facetracking/facetracking.txt");
        // foreach (var line in lines){
        //     var data = line;
        //     faceTrack.Add(data);
        //     print(data);
        //     break;
        // }
        return(lines[lines.Length - 1]);
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //moveDirection = new Vector2(moveX, moveY).normalized;
    }
    // adding a velocity to the player
    void Move()
    {
        string s = readFile();
        int val = System.Convert.ToInt32(s);
        print(val);
        if (val > 550){
            moveDirection = new Vector2(-1, 0).normalized;
        } else if (val < 450) {
            moveDirection = new Vector2(1, 0).normalized;
        } else {
            moveDirection = new Vector2(0, 0).normalized;
        }
        
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
