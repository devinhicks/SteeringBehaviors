using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinematic : MonoBehaviour
{
    // position comes from attached gameobject transform
    // rotation as well
    public Vector3 linearVelocity;
    public float angularVelocity; // degrees please
    public GameObject target;
    public float maxSpeed;

    public bool seeking = true; // toggle seek or flee

    // Update is called once per frame
    void Update()
    {
        // update position and rotation
        transform.position += linearVelocity * Time.deltaTime;
        Vector3 angularIncrement = new Vector3(0, angularVelocity * Time.deltaTime, 0);
        transform.eulerAngles += angularIncrement;

        // update linear and angular velocity
        // check mode of character to know whether to seek or flee
        // if/else determines what direction steering vector will be pointing
        SteeringOutput steering;

        if (seeking == true)
        {
            Seek mySeek = new Seek();
            mySeek.character = this;
            mySeek.target = target;
            steering = mySeek.getSteering();
        }
        else
        {
            Flee myFlee = new Flee();
            myFlee.character = this;
            myFlee.target = target;
            steering = myFlee.getSteering();
        }

        // steering vector is then added to the character's current vector
        linearVelocity += steering.linear * Time.deltaTime;
        angularVelocity += steering.angular * Time.deltaTime;


        if (linearVelocity.magnitude > maxSpeed)
        {
            linearVelocity.Normalize();
            linearVelocity *= maxSpeed;
        }
    }
}