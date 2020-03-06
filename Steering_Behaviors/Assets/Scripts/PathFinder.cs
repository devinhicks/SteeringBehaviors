using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : Kinematic
{
    public Node start;
    public Node end;
    Graph m_Graph;

    FollowPath moveType;
    LookWhereYoureGoing rotateType;

    GameObject[] m_path;

    // Start is called before the first frame update
    void Start()
    {
        rotateType = new LookWhereYoureGoing();
        rotateType.character = this;
        rotateType.target = target;

        m_Graph = new Graph();
        m_Graph.Build();
        List<Connection> path = Dijkstra.pathfind(m_Graph, start, end);
        // convert connections to gameobjects
        m_path = new GameObject[path.Count + 1];
        int i = 0;
        foreach (Connection c in path)
        {
            Debug.Log("from " + c.getFromNode() + " to " + c.getToNode()
                + " @" + c.getCost());
            m_path[i] = c.getFromNode().gameObject;
            i++;
        }
        m_path[i] = end.gameObject;

        moveType = new FollowPath();
        moveType.character = this;
        moveType.path = m_path;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.angular = rotateType.getSteering().angular;
        steeringUpdate.linear = moveType.getSteering().linear;
        base.Update();
    }
}
