using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
//#include <deque>

public class RacketController : MonoBehaviour
{
    //deque<double> dq1 = {0,0,0,0,0,0,0,0,0,0};

    ///// MQTT broker settings
    private string brokerAddress = "mqtt.eclipseprojects.io";
    private int brokerPort = 1883;
    private string clientId = "UnityClient";
    private string topic = "arduino/unity";
    private Quaternion orientation = Quaternion.identity;
    public char action;
    
    // Unity object to update with received orientation
    public GameObject targetObject;
    
    // MQTT client instance
    private MqttClient mqttClient;
    /////

    void Start()
    {
        // Create MQTT client instance
        mqttClient = new MqttClient(brokerAddress, brokerPort, false, null, null, MqttSslProtocols.None);

        // Register callback for received MQTT messages
        mqttClient.MqttMsgPublishReceived += OnMQTTMessageReceived;
        
        // Connect to MQTT broker
        mqttClient.Connect(clientId);
        
        // Subscribe to MQTT topic
        mqttClient.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }
    private float w;
    private float x;
    private float y;
    private float z;

    private void OnMQTTMessageReceived(object sender, MqttMsgPublishEventArgs e)
    {
        // Parse received quaternion from message payload
        string payload = Encoding.UTF8.GetString(e.Message);
        print(payload);
        /*string[] quaternionStrings = payload.Split("'");
        w = float.Parse(quaternionStrings[0]);
        x = float.Parse(quaternionStrings[1]);
        y = float.Parse(quaternionStrings[2]);
        z = float.Parse(quaternionStrings[3]);
        orientation = new Quaternion(x, y, z, w);*/
        action=payload[0];
        //dq1.push_back(y);	
        //dq1.pop_front();
        // Update target object's rotation with received quaternion
        //targetObject.transform.rotation = quaternion;
        
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        if(action == 'f')
        {
            Forehand();
            action = ' ';
        }
        else if(action == 'b')
        {
            Backhand();
            action = ' ';
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
