using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : Seek
{
    public float avoidDistance = 50f; // min distance to a wall, should be greater than radius of character
    public float lookAhead = 50f; // distance to look ahead for collision

    protected override Vector3 getTargetPosition()
    {
        RaycastHit center;
        RaycastHit left;
        RaycastHit right;

        // offset the raycast so that it lines up
        //with the outer edges of the object
        Vector3 offset = new Vector3(.6f, 0, 0);

        // check center raycast
        if (Physics.Raycast(character.transform.position,
            character.linearVelocity, out center, lookAhead))
        {
            Debug.DrawRay(character.transform.position,
                character.linearVelocity * center.distance, Color.yellow, 0.5f);
            return center.point + (center.normal * avoidDistance);
        }
        // check left raycast
        if (Physics.Raycast(character.transform.position - offset,
            character.linearVelocity, out left, lookAhead))
        {
            Debug.DrawRay(character.transform.position,
                character.linearVelocity * left.distance, Color.blue, 0.5f);
            return left.point + (left.normal * avoidDistance);
        }
        // check right raycast
        if (Physics.Raycast(character.transform.position + offset,
            character.linearVelocity, out right, lookAhead))
        {
            Debug.DrawRay(character.transform.position,
                character.linearVelocity * right.distance, Color.red, 0.5f);
            return right.point - (right.normal * avoidDistance);
        }
        // otherwise draw a white line and do nothing
        else
        {
            Debug.DrawRay(character.transform.position,
                character.linearVelocity * lookAhead, Color.white, 0.5f);
            //return Vector3.positiveInfinity;
        }

        return base.getTargetPosition();
    }
}