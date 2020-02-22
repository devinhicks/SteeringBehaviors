using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocker : Kinematic
{
    BlendedSteering m_Steering;
    Kinematic[] kBoids;
    public GameObject cohereTarget;

    // Start is called before the first frame update
    protected override void Start()
    {
        // personal space for the boidies
        Separation separate = new Separation();
        separate.character = this;
        GameObject[] goBoids = GameObject.FindGameObjectsWithTag("boid");
        kBoids = new Kinematic[goBoids.Length - 1];
        int j = 0;
        for (int i = 0; i < goBoids.Length-1; i++)
        {
            if (goBoids[i] == this)
            {
                continue;
            }
            kBoids[j++] = goBoids[i].GetComponent<Kinematic>();
        }
        separate.targets = kBoids;

        // cohere to center of mass
        Arrive cohere = new Arrive();
        cohere.character = this;
        cohere.target = cohereTarget;

        // and look where center of mass is going
        Face rotateType = new Face();
        rotateType.character = this;
        rotateType.target = cohereTarget;

        // m_Steering = new BlendedSteering();
        m_Steering = gameObject.AddComponent<BlendedSteering>();
        m_Steering.behaviors = new BehaviorAndWeight[3];
        m_Steering.behaviors[0] = new BehaviorAndWeight();
        m_Steering.behaviors[0].behavior = separate;
        m_Steering.behaviors[0].weight = 1f;
        m_Steering.behaviors[1] = new BehaviorAndWeight();
        m_Steering.behaviors[1].behavior = cohere;
        m_Steering.behaviors[1].weight = 1f;
        m_Steering.behaviors[2] = new BehaviorAndWeight();
        m_Steering.behaviors[2].behavior = rotateType;
        m_Steering.behaviors[2].weight = 1f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = m_Steering.getSteering();
        base.Update();
    }
}
