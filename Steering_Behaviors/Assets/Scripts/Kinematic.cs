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

    public float maxSpeed = 3.0f;
    public float maxAngularVel = 45.0f;

    protected SteeringOutput steeringUpdate;

    protected virtual void Start()
    {
        steeringUpdate = new SteeringOutput();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (float.IsNaN(angularVelocity))
        {
            angularVelocity = 0.0f;
        }

        // update position and rotation
        this.transform.position += linearVelocity * Time.deltaTime;
        Vector3 angularIncrement = new Vector3(0, angularVelocity * Time.deltaTime, 0);
        transform.eulerAngles += angularIncrement;

        // steering vector is then added to the character's current vector
        if (steeringUpdate != null)
        {
            linearVelocity += steeringUpdate.linear * Time.deltaTime;
            angularVelocity += steeringUpdate.angular * Time.deltaTime;
        }

        if (linearVelocity.magnitude > maxSpeed)
        {
            linearVelocity.Normalize();
            linearVelocity *= maxSpeed;
        }

        if (Mathf.Abs(angularVelocity) > maxAngularVel)
        {
            angularVelocity = maxAngularVel * (angularVelocity / angularVelocity);
        }
    }
}