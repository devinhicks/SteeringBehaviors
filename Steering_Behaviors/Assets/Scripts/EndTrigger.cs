using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public int dropCount = 0;

    public GameManager gm;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Laser")
        {
            dropCount++;
        }
    }

    public void Update()
    {
        if (dropCount >= 25)
        {
            gm.ComepleteLevel();
        }
    }
}
