using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicAlign : MonoBehaviour
{
    // position comes from attached gameobject transform
    // rotation as well
    public Vector3 linearVelocity = new Vector3(1,1,1);
    public float angularVelocity; // degrees please
    public GameObject target;

    public bool face = false;
    public bool lwyg = false;

    // Update is called once per frame
    void Update()
    {
        if (float.IsNaN(angularVelocity))
        {
            angularVelocity = 0.0f;
        }

        // update position and rotation
        transform.position += linearVelocity * Time.deltaTime;

        if (Mathf.Abs(angularVelocity) > 0.01f)
        {
            Vector3 v = new Vector3(0, angularVelocity, 0);
            this.transform.eulerAngles += v * Time.deltaTime;
        }

        Align myAlign = new Align();

        if (face)
        {
            myAlign = new Face();
        }
        else if (lwyg)
        {
            myAlign = new LookWhereYoureGoing();
        }

        myAlign.character = this;
        myAlign.target = target;
        SteeringOutput steering = myAlign.getSteering();
        linearVelocity += steering.linear * Time.deltaTime;
        angularVelocity += steering.angular * Time.deltaTime;
    }
}
