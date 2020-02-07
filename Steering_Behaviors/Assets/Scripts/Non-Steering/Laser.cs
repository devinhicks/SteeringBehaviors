using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameManager gm;

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            gm.EndGame();
        }
    }
}
