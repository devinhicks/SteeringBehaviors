using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitChaser : Kinematic
{
    FollowPath m_Move;
    LookWhereYoureGoing m_Rotate;

    public GameObject[] m_Path = new GameObject[5];

    public float targetRadius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        m_Rotate = new LookWhereYoureGoing();
        m_Rotate.character = this;
        m_Rotate.target = target;

        m_Move = new FollowPath();
        m_Move.character = this;
        m_Move.path = m_Path;
        m_Move.targetRadius = targetRadius;
    }

    // Update is called once per frame
    protected override void Update()
    {
        //transform.position += linearVelocity * Time.deltaTime;

        steeringUpdate = new SteeringOutput();
        steeringUpdate.angular = m_Rotate.getSteering().angular;
        steeringUpdate.linear = m_Move.getSteering().linear;

        base.Update();
    }
}
