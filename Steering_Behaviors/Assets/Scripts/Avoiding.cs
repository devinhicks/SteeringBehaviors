using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoiding : Kinematic
{
    public bool obstacle = false;

    CollisionAvoidance avoidTypeCol;
    ObstacleAvoidance avoidTypeObs;
    //LookWhereYoureGoing rotateType;

    public Kinematic[] avoidingTargets = new Kinematic[2];
    public GameObject obstacleTarget;

    // Start is called before the first frame update
    protected override void Start()
    {
        if (obstacle)
        {
            avoidTypeObs = new ObstacleAvoidance();
            avoidTypeObs.character = this;
            avoidTypeObs.target = obstacleTarget;
        }
        else
        {
            avoidTypeCol = new CollisionAvoidance();
            avoidTypeCol.character = this;
            avoidTypeCol.targets = avoidingTargets;
        } 
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (obstacle)
        {
            steeringUpdate = avoidTypeObs.getSteering();
        }
        else
        {
            steeringUpdate = avoidTypeCol.getSteering();
        }
  
        base.Update();
    }
}
