using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://forum.unity.com/threads/hinge-joint-2d-change-motor-speed.404402/ code for motor speed change from this forum, comment by Hassan-Kanso

public class RacketController : MonoBehaviour
{
    public Animator animator;

    //delay between swings
    public float delay = 0.3f;
    private bool swingBlocked;

    public Transform circleOrigin;
    public float radius;

    //use to do a strong hit vs weak hit
    public float strong = 15.0f;
    public float weak = 8.0f;

    void Update()
    {
        //was code for controlling racket swing with hingejoint but now using animation
        /* HingeJoint2D hingeJoint2D = GetComponent<HingeJoint2D>();
        JointMotor2D motor = hingeJoint2D.motor;

        if(Input.GetKey(KeyCode.Q))
        {
            motor.motorSpeed = 350;
            hingeJoint2D.useMotor = true;
            hingeJoint2D.motor = motor;
        }
        else if(Input.GetKey(KeyCode.E))
        {
            motor.motorSpeed = -350;
            hingeJoint2D.useMotor = true;
            hingeJoint2D.motor = motor;
        }
        else
        {
            GetComponent<HingeJoint2D>().useMotor = false;
        }
        */

        //Q for backhand and E for forehand
        if(Input.GetKey(KeyCode.E))
        {
            Forehand();
        }
        else if(Input.GetKey(KeyCode.Q))
        {
            Backhand();
        }
    }

    //forehand function assuming swing isn't blocked by delay timer
    public void Forehand()
    {
        if(swingBlocked)
            return;
        animator.SetTrigger("Forehand");
        swingBlocked = true;
        StartCoroutine(DelaySwing());
    }

    //backhand function assuming swing isn't blocked by delay timer
    public void Backhand()
    {
        if(swingBlocked)
            return;
        animator.SetTrigger("Backhand");
        swingBlocked = true;
        StartCoroutine(DelaySwing());
    }

    private IEnumerator DelaySwing()
    {
        yield return new WaitForSeconds(delay);
        swingBlocked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectCollidersForehand()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius))
        {
            Debug.Log(collider.name);
            Rigidbody2D rb = collider.attachedRigidbody;
            Vector3 v = new Vector3(Random.Range(-0.75f,-0.5f) * weak, weak, 0.0f);
            rb.AddForce(v, ForceMode2D.Impulse);
        }
    }

    public void DetectCollidersBackhand()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius))
        {
            Debug.Log(collider.name);
            Rigidbody2D rb = collider.attachedRigidbody;
            Vector3 v = new Vector3(Random.Range(0.75f,0.5f) * weak, weak, 0.0f);
            rb.AddForce(v, ForceMode2D.Impulse);
        }
    }
}
