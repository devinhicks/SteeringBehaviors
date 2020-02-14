using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek
{
    public Kinematic character;
    public GameObject target;

    public float maxAcceleration = 10f;

    protected virtual Vector3 getTargetPosition()
    {
        return target.transform.position;
    }

    public virtual SteeringOutput getSteering()
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

public class Separation : Seek
{
    //public Kinematic character;
    float maxAcceleration = 6f;

    public GameObject[] targets; // list of potential targets
    float threshold = 15f; // the threshold to take action
    float decayCoefficient = 1f;

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // loop through each target
        foreach(GameObject target in targets)
        {
            // check if target is close
            Vector3 direction = target.transform.position - character.transform.position;
            float distance = direction.magnitude;

            if (distance < threshold)
            {
                // calculate strength of repulsion here (using inverse square law)
                float strength = Mathf.Min(decayCoefficient /
                    (distance * distance), maxAcceleration);

                // add accelaeration
                direction.Normalize();
                result.linear += strength * direction;
            }
        }

        return result;
    }
}
