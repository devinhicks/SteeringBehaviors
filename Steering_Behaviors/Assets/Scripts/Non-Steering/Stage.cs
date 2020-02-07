using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public float scaleFactor = 0.05f;
    public float scaleMin = 3f;
    Vector3 scaler;

    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        scaler = new Vector3(scaleFactor, 0.0f, scaleFactor);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= scaler * Time.deltaTime;

        if (transform.localScale.x <= scaleMin)
        {
            gm.CompleteLevel();
        }
    }
}
