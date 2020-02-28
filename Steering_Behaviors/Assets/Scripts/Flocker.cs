using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocker : Kinematic
{
    BlendedSteering m_Steering;
    Kinematic[] kBoids;
    public GameObject cohereTarget;
    public GameObject avoidThis;

    PrioritySteering prioritySteering;
    public bool advanced = false;

    public Separation separate;

    // Failed attempt to add observer pattern to allow for adding
    // or deleting boids
    //private void OnEnable()
    //{
    //    DestroyerAngel.OnKillBoid += killBoids;
    //    //PriorityGame.OnAddBoid += addBoids;
    //}

    //private void OnDisable()
    //{
    //    DestroyerAngel.OnKillBoid -= killBoids;
    //    //PriorityGame.OnAddBoid -= addBoids;
    //}

    // Start is called before the first frame update
    protected override void Start()
    {
        // personal space for the boidies
        separate = new Separation();
        separate.character = this;
        GameObject[] goBoids = GameObject.FindGameObjectsWithTag("boid");
        kBoids = new Kinematic[goBoids.Length - 1];
        int j = 0;
        for (int i = 0; i < goBoids.Length - 1; i++)
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

        // add prioritysteering
        ObstacleAvoidance avoid = new ObstacleAvoidance();
        avoid.character = this;
        avoid.target = avoidThis;
        avoid.lookAhead = 200f;
        avoid.avoidDistance = 100f;
        avoid.flee = true;

        // blend the priority steering with the original blended steering
        BlendedSteering highPriority = gameObject.AddComponent<BlendedSteering>();
        highPriority.behaviors = new BehaviorAndWeight[1];
        highPriority.behaviors[0] = new BehaviorAndWeight();
        highPriority.behaviors[0].behavior = avoid;
        highPriority.behaviors[0].weight = 100f;

        prioritySteering = gameObject.AddComponent<PrioritySteering>();
        prioritySteering.groups = new BlendedSteering[2];
        prioritySteering.groups[0] = gameObject.AddComponent<BlendedSteering>();
        prioritySteering.groups[0] = highPriority;
        prioritySteering.groups[1] = gameObject.AddComponent<BlendedSteering>();
        prioritySteering.groups[1] = m_Steering;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();

        if (advanced)
        {
            //separate.targets = kBoids;
            steeringUpdate = prioritySteering.getSteering();
        }
        else
        {
            steeringUpdate = m_Steering.getSteering();
        }

        base.Update();
    }

    // more observer pattern things
    //public void killBoids(Kinematic boid)
    //{
    //    DestroyerAngel.OnKillBoid -= killBoids;

    //    List<Kinematic> list = new List<Kinematic>(kBoids);
    //    list.Remove(boid);
    //    kBoids = list.ToArray();
    //}

    //public void addBoids(Kinematic boid)
    //{
    //    PriorityGame.OnAddBoid -= addBoids;

    //    List<Kinematic> list = new List<Kinematic>(kBoids);
    //    list.Add(boid);
    //    kBoids = list.ToArray();
    //}
}