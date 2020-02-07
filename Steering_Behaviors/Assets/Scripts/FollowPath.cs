using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : Arrive
{
    public GameObject[] path;

    int currentPathIndex;

    public float targetRadius = 1f;

   public override SteeringOutput getSteering()
    {
        if (target == null)
        {
            int nearestPathIndex = 0;
            float distanceToNearest = float.PositiveInfinity;
            for (int i = 0; i < path.Length; i++)
            {
                GameObject candidate = path[i];
                Vector3 vectorToCandidate = candidate.transform.position -
                    character.transform.position;
                //Debug.Log(vectorToCandidate);
                float distanceToCandidate = vectorToCandidate.magnitude;

                if (distanceToCandidate < distanceToNearest)
                {
                    nearestPathIndex = i;
                    distanceToNearest = distanceToCandidate;
                }
            }

            target = path[nearestPathIndex];
        }

        float distanceToTarget =
            (target.transform.position - character.transform.position).magnitude;
        if (distanceToTarget < targetRadius)
        {
            currentPathIndex++;
            if (currentPathIndex > path.Length - 1)
            {
                currentPathIndex = 0;
            }
        }

        target = path[currentPathIndex];

        return base.getSteering();
    }
}
