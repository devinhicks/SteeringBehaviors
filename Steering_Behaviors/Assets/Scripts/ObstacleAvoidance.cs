using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : Seek
{
    public float avoidDistance = 20f; // min distance to a wall should be greater than radius of character
    public float lookAhead = 5f; // distance to look ahead for collision

    protected override Vector3 getTargetPosition()
    {
        RaycastHit hit;
        if (Physics.Raycast(character.transform.position,
            character.linearVelocity, out hit, lookAhead))
        {
            Debug.DrawRay(character.transform.position,
                character.linearVelocity * hit.distance, Color.yellow, 0.5f);
            return hit.point + (hit.normal * avoidDistance);
        }
        else
        {
            Debug.DrawRay(character.transform.position,
                character.linearVelocity * lookAhead, Color.white, 0.5f);
        }

        return base.getTargetPosition();
    }
}

//public abstract class CollisionDetector
//{
//    public abstract Collision getCol(Vector3 position, Vector3 moveAmount);
//}

//public abstract class Collision
//{
//    public Vector3 position;
//    public Vector3 normal;
//}