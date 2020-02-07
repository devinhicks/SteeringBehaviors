using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arriving : Kinematic
{
    Arrive moveType;
    Align rotateType;

    // Start is called before the first frame update
    protected override void Start()
    {
        moveType = new Arrive();
        moveType.character = this;
        moveType.target = target;

        rotateType = new Align();
        rotateType.character = this;
        rotateType.target = target;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = moveType.getSteering().linear;
        steeringUpdate.angular = rotateType.getSteering().angular;
        base.Update();
    }
}
