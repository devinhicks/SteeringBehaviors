using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillars : MonoBehaviour
{
    public Material activated;
    public int activatedCount = 0;

    public GameManager gm;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Chaser")
        {
            var color = this.GetComponent<Renderer>();
            color.material = activated;

            activatedCount++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activatedCount == 3)
        {
            gm.CompleteLevel();
        }
    }
}
