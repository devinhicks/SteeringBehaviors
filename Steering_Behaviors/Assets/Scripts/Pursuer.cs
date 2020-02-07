using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : Kinematic
{
    Pursue myMoveType;
    LookWhereYoureGoing myRotateType;

    // Start is called before the first frame update
    protected override void Start()
    {
        myRotateType = new LookWhereYoureGoing();
        myRotateType.character = this;
        myRotateType.target = target;

        myMoveType = new Pursue();
        myMoveType.character = this;
        myMoveType.target = target;
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