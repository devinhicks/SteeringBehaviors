using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeking : Kinematic
{
    // MoveType Seek/Flee/Pursue
    Seek moveTypeSeek;
    Seek moveTypeFlee;
    Evade moveTypeEvade;
    ObstacleAvoidance moveTypeAvoid;
    //Pursue moveTypePursue;

    //Rotate Type Face/LWYG
    Face rotateTypeFace;
    LookWhereYoureGoing rotateTypeLWYG;

    public bool seeking = true;
    public bool fleeing = false;
    public bool evading = false;
    public bool obstacleAvoidance = false;
    //if both are false, character is pursuing

    public bool facing = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        moveTypeSeek = new Seek();
        moveTypeSeek.character = this;
        moveTypeSeek.target = target;
        
        moveTypeFlee = new Seek();
        moveTypeFlee.flee = true;
        moveTypeFlee.character = this;
        moveTypeFlee.target = target;
        
        moveTypeEvade = new Evade();
        moveTypeEvade.character = this;
        moveTypeEvade.target = target;

        moveTypeAvoid = new ObstacleAvoidance();
        moveTypeAvoid.character = this;
        moveTypeAvoid.target = target;

        rotateTypeFace = new Face();
        rotateTypeFace.character = this;
        rotateTypeFace.target = target;

        rotateTypeLWYG = new LookWhereYoureGoing();
        rotateTypeLWYG.character = this;
        rotateTypeLWYG.target = target;


    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        if (seeking)
        {
            steeringUpdate.linear = moveTypeSeek.getSteering().linear;
        }
        else if (fleeing)
        {
            steeringUpdate.linear = moveTypeFlee.getSteering().linear;
        }
        else if (evading)
        {
            steeringUpdate.linear = moveTypeEvade.getSteering().linear;
        }
        else
        {
            steeringUpdate.linear = moveTypeAvoid.getSteering().linear;
        }

        steeringUpdate.angular = facing ?
            rotateTypeFace.getSteering().angular :
            rotateTypeLWYG.getSteering().angular;
        base.Update();
    }
}
