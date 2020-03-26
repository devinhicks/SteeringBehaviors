using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<Connection> m_Connections;

    private static bool isButterfly;

    // an array of connections outgoing from the given node.
    public List<Connection> getConnections(Node fromNode)
    {
        List<Connection> connections = new List<Connection>();
        foreach (Connection c in m_Connections)
        {
            if (c.getFromNode() == fromNode)
            {
                connections.Add(c);
            }
        }
        return connections;
    }

    // TACTICAL PATHFINDING //
    // we will use booleans on our pathfinding characters to determine
    // what their cost will be
    // BEES will not be particular about flowers, and so they travel by shortest
    // distance, but BUTTERFLIES are very particular about their flowers, so
    // they will only fly to flowers of a particular color

    public void Build()
    {
        // find nodes, iterate over nodes, create connections, put in m_Connections
        m_Connections = new List<Connection>();

        isButterfly = PathFinder.isButterfly;

        Debug.Log("Graph: " + isButterfly);

        Node[] nodes = GameObject.FindObjectsOfType<Node>();
        foreach (Node fromNode in nodes)
        {
            foreach (Node toNode in fromNode.ConnectsTo)
            {
                Color nColor = toNode.gameObject.GetComponent<Renderer>().material.color;

                float cost = (toNode.transform.position
                    - fromNode.transform.position).magnitude;

                if (isButterfly == true) // if bool set to true on path finder
                {
                    if (nColor == Color.cyan) //if node's game object isn't yellow
                    {
                        cost *= 10; // make cost greater
                    }
                }
                Connection c = new Connection(cost, fromNode, toNode);
                m_Connections.Add(c);
            }
        }
    }
}

public class Connection
{
    float cost;
    Node fromNode; // the node that this connection came from
    Node toNode; // the node the connection is going to

    public Connection(float cost, Node fromNode, Node toNode)
    {
        this.cost = cost;
        this.fromNode = fromNode;
        this.toNode = toNode;
    }

    public float getCost()
    {
        return cost;
    }

    public Node getFromNode()
    {
        return fromNode;
    }

    public Node getToNode()
    {
        return toNode;
    }
}
