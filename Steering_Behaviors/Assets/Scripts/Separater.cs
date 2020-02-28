using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separater : Kinematic
{
    Separation myMoveType;
    LookWhereYoureGoing myRotateType;

    public Kinematic[] targets;

    // Start is called before the first frame update
    protected override void Start()
    {
        myRotateType = new LookWhereYoureGoing();
        myRotateType.character = this;
        myRotateType.target = target;

        myMoveType = new Separation();
        myMoveType.character = this;
        myMoveType.targets = targets;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.angular = myRotateType.getSteering().angular;
        steeringUpdate.linear = myMoveType.getSteering().linear;
        base.Update();
    }
}

public class Separation : SteeringBehavior
{
    public Kinematic character;
    float maxAcceleration = 10f;

    public Kinematic[] targets; // list of potential targets

    float threshold = 5f; // the threshold to take action
    float decayCoefficient = 100f;

    public override SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // loop throh each target
        foreach (Kinematic target in targets)
        {
            // check if target is close
            Vector3 direction = character.transform.position - target.transform.position;
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
