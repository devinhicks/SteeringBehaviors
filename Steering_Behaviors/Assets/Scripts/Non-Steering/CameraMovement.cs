using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;
    Transform t;

    // Start is called before the first frame update
    void Start()
    {
        t = cam.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            t.Rotate(new Vector3(0, 0, 1), -10.0f * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            t.Rotate(new Vector3(0, 0, 1), 10.0f * Time.deltaTime);
        }
    }
}
