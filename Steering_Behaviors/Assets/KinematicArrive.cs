using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicArrive : MonoBehaviour
{
    // position comes from attached gameobject transform
    // rotation as well
    public Vector3 linearVelocity;
    public float angularVelocity; // degrees please
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        // update position and rotation
        transform.position += linearVelocity * Time.deltaTime;
        Vector3 angularIncrement = new Vector3(0, angularVelocity * Time.deltaTime, 0);
        transform.eulerAngles += angularIncrement;

        // update linear and angular velocity
        Arrive myArrive = new Arrive();
        myArrive.character = this;
        myArrive.target = target;
        SteeringOutput steering = myArrive.getSteering();
        linearVelocity += steering.linear * Time.deltaTime;
        angularVelocity += steering.angular * Time.deltaTime;
    }
}
