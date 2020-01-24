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

        // update linear and angular velocity
        Align myAlign = new Align();
        myAlign.character = this;
        myAlign.target = target;
        SteeringOutput steering = myAlign.getSteering();
        linearVelocity += steering.linear * Time.deltaTime;
        angularVelocity += steering.angular * Time.deltaTime;
    }
}
