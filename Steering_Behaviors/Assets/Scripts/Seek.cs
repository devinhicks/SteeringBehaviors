using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehavior
{
    public Kinematic character;
    public GameObject target;

    public float maxAcceleration = 20f;

    protected virtual Vector3 getTargetPosition()
    {
        return target.transform.position;
    }

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        //result.linear = new Vector3(0, 0, 1);
        //result.angular = 5f;

        // get direction to the target
        result.linear = getTargetPosition() - character.transform.position;

        // give full acceleration
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;
        return result;
    }
}

public class Flee
{
    public Kinematic character;
    public GameObject target;

    float maxAcceleration = 5f;

    public virtual Vector3 getTargetPosition()
    {
        return target.transform.position;
    }

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        //result.linear = new Vector3(0, 0, 1);
        //result.angular = 5f;

        // get direction to the target
        result.linear = character.transform.position - getTargetPosition();

        // give full acceleration
        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;
        return result;
    }
}

public class Evade : Flee
{
    // will override target in Seek
    float maxPrediction = 1f;

    public override Vector3 getTargetPosition()
    {
        // distance to target
        Vector3 direction = target.transform.position - character.transform.position;
        float distance = direction.magnitude;

        // get speed of character
        float speed = character.linearVelocity.magnitude;

        // check if speed gives reasonable prediction time
        float prediction;
        if (speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
        }

        Kinematic movingTarget = target.GetComponent<Kinematic>();
        if (movingTarget == null)
        {
            return base.getTargetPosition();
        }

        return target.transform.position + movingTarget.linearVelocity * prediction;
    }
}