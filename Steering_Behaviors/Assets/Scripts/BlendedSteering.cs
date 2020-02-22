using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehavior
{
    public abstract SteeringOutput getSteering();
}

public class BehaviorAndWeight
{
    public SteeringBehavior behavior;
    public float weight;
}

public class BlendedSteering : MonoBehaviour
{
    public BehaviorAndWeight[] behaviors;

    // overall max acceleration and rotation
    float maxAcceleration = 1f;
    float maxRotation = 5f;

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // accumulate all accelerations
        foreach (BehaviorAndWeight b in behaviors)
        {
            SteeringOutput s = b.behavior.getSteering();
            if (s != null)
            {
                result.linear += b.weight * s.linear;
                result.angular += b.weight * s.angular;
            }
        }

        // crop result and return - but not the way Millington does it
        //result.linear = Mathf.Max(result.linear, maxAcceleration);
        //result.angular = Mathf.Max(result.angular, maxRotation);
        result.linear = result.linear.normalized
            * Mathf.Min(maxAcceleration,result.linear.magnitude);
        result.angular = Mathf.Abs(result.angular) / result.angular * maxRotation;
        return result;
    }
}