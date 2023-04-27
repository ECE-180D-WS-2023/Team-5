using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RacketController2 : MonoBehaviour
{
    public Animator animator;

    //delay between swings
    public float delay = 0.3f;
    private bool swingBlocked;

    public Transform circleOrigin1;
    public Transform circleOrigin2;
    public float radius1;
    public float radius2;

    //use to do a strong hit vs weak hit
    public float strong = 15.0f;
    public float weak = 8.0f;

    void Update()
    {
        //O for backhand and U for forehand
        if(Input.GetKey(KeyCode.O))
        {
            Forehand2();
        }
        else if(Input.GetKey(KeyCode.U))
        {
            Backhand2();
        }
    }

    //forehand function assuming swing isn't blocked by delay timer
    public void Forehand2()
    {
        if(swingBlocked)
            return;
        animator.SetTrigger("Forehand2");
        swingBlocked = true;
        StartCoroutine(DelaySwing());
    }

    //backhand function assuming swing isn't blocked by delay timer
    public void Backhand2()
    {
        if(swingBlocked)
            return;
        animator.SetTrigger("Backhand2");
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
        Vector3 position1 = circleOrigin1 == null ? Vector3.zero : circleOrigin1.position;
        Gizmos.DrawWireSphere(position1, radius1);
        Gizmos.color = Color.red;
        Vector3 position2 = circleOrigin2 == null ? Vector3.zero : circleOrigin2.position;
        Gizmos.DrawWireSphere(position2, radius2);
    }

    public void DetectCollidersForehand2()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin1.position,radius1))
        {
            Debug.Log(collider.name);
            Rigidbody2D rb = collider.attachedRigidbody;
            Vector3 v = new Vector3(Random.Range(-0.75f,-0.5f) * weak, weak, 0.0f);
            rb.AddForce(v, ForceMode2D.Impulse);
        }
    }

    public void DetectCollidersBackhand2()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin2.position,radius2))
        {
            Debug.Log(collider.name);
            Rigidbody2D rb = collider.attachedRigidbody;
            Vector3 v = new Vector3(Random.Range(0.75f,0.5f) * weak, weak, 0.0f);
            rb.AddForce(v, ForceMode2D.Impulse);
        }
    }
}
