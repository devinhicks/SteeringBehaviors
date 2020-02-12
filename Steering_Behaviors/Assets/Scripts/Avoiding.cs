using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoiding : Kinematic
{
    CollisionAvoidance moveType;
    LookWhereYoureGoing rotateType;

    public Kinematic[] avoidingTargets;

    // Start is called before the first frame update
    protected override void Start()
    {
        moveType = new CollisionAvoidance();
        moveType.character = this;
        moveType.targets = avoidingTargets;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = moveType.getSteering().linear;
        steeringUpdate.angular = moveType.getSteering().angular;
        base.Update();
    }
}
