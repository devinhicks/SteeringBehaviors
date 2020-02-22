using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : SteeringBehavior
{
    public Kinematic character;
    public GameObject target;

    float maxAngularAcceleration = 100f;
    float maxRotation = 45f;
        
    //float targetRadius = 1f; // the radius for arriving at the target
    float slowRadius = 10f; // the radius for beginning to slow down
    float timeToTarget = 0.1f; // the time over which to achieve target speed

    public virtual float getTargetAngle()
    {
        return target.transform.eulerAngles.y;
    }

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        float rotation, rotationSize, angularAcceleration;

        // get naive direction to the target
        //Vector3 direction = target.transform.position - character.transform.position;

        rotation = Mathf.DeltaAngle(character.transform.eulerAngles.y, getTargetAngle());
        rotationSize = Mathf.Abs(rotation);

        // check if we are there, return no steering
        //if (rotationSize < targetRadius)
        //{
        //    return null;
        //}

        // if we are outside the slowRadius, then use max rotation
        float targetRotation = 0.0f;
        if (rotationSize > slowRadius)
        {
            targetRotation = maxRotation;
        }
        else // otherwise calculate a scaled rotation
        {
            targetRotation = maxRotation * rotationSize / slowRadius;
        }

        // the final target rotation combines speed (already in the variable)
        // and direction
        targetRotation *= rotation / rotationSize;
        
        // acceleration tries to get to the target rotation
        // check if NaN and make 0 if so
        float currentAngularVelocity = float.IsNaN(character.angularVelocity)
                                            ? 0f : character.angularVelocity;
        result.angular = targetRotation - currentAngularVelocity;
        result.angular /= timeToTarget;

        // check if the acceleration is too great
        angularAcceleration = Mathf.Abs(result.angular);
        if (angularAcceleration > maxAngularAcceleration)
        {
            result.angular /= angularAcceleration;
            result.angular *= maxAngularAcceleration;
        }

        result.linear = Vector3.zero;
        return result;
    }
}

public class Face : Align
{
    public override float getTargetAngle()
    {
        Vector3 direction = target.transform.position - character.transform.position;

        float targetAngle = Mathf.Atan2(direction.x, direction.z);
        return targetAngle * Mathf.Rad2Deg;
    }
}

public class LookWhereYoureGoing : Align
{
    public override float getTargetAngle()
    {
        Vector3 velocity = character.linearVelocity;
        if (velocity.magnitude == 0)
        {
            return character.transform.eulerAngles.y;
        }

        float targetAngle = Mathf.Atan2(velocity.x, velocity.z);
        return targetAngle * Mathf.Rad2Deg;
    }
}
