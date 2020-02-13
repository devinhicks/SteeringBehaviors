using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance
{
    public Kinematic character;
    public Kinematic[] targets;

    public float maxAcceleration = 3f;
    public float radius = 1f;

    public SteeringOutput getSteering()
    {
        /// step 1: see if there's impending danger
        
        // find target that's closest to collision
        // store the first collision time
        float shortestTime = float.PositiveInfinity;

        // store our data
        Vector3 relativePos = Vector3.positiveInfinity;

        // store that target and it's data
        Kinematic firstTarget = null;
        float firstMinSeparation= float.PositiveInfinity;
        float firstDistance = float.PositiveInfinity;
        Vector3 firstRelativePos = Vector3.positiveInfinity;
        Vector3 firstRelativeVel = Vector3.zero;

        // loop through targets
        foreach (Kinematic target in targets)
        {
            // get time until collision
            relativePos = target.transform.position - character.transform.position;
            Vector3 relativeVel = character.linearVelocity - target.linearVelocity;
            float relativeSpeed = relativeVel.magnitude;
            float timeToCollision = (Vector3.Dot(relativePos, relativeVel)) /
                (relativeSpeed * relativeSpeed);

            // check if we'll actually collide
            float distance = relativePos.magnitude;
            float minSeparation = distance - relativeSpeed * timeToCollision;
            if (minSeparation > 2 * radius)
            {
                continue;
            }

            // check if it is the shortest
            if (timeToCollision > 0 && timeToCollision < shortestTime)
            {
                // store the time, target and other data
                shortestTime = timeToCollision;
                firstTarget = target;
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;
            }
        }

        /// step 2: do somethin about it

        // if no target, exit
        if (firstTarget == null)
        {
            Debug.Log("SAFE!");
            return null;
        }

        /// if we're going to hit exactly, or if we're already colliding
        /// do the steering based on current Pos

        Debug.Log("DANGER - gonna hit " + firstTarget);
        //return null;

        SteeringOutput result = new SteeringOutput();

        float dotResult = Vector3.Dot(character.linearVelocity.normalized,
            firstTarget.linearVelocity.normalized);
        if (dotResult < -0.9)
        {
            result.linear = -firstTarget.transform.right;
        }
        else
        {
            result.linear = -firstTarget.linearVelocity;
        }
        result.linear.Normalize();
        result.linear *= maxAcceleration;
        result.angular = 0;
        return result;
    }
}
