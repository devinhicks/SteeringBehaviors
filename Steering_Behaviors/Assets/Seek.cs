using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek
{
    public Kinematic character;
    public GameObject target;

    float maxAcceleration = 5f;

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        //result.linear = new Vector3(0, 0, 1);
        //result.angular = 5f;

        // get direction to the target
        result.linear = target.transform.position - character.transform.position;

        // give full acceleration
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;
        return result;
    }
}
