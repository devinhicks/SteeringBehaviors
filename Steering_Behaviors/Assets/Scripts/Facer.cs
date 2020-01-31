using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facer : MonoBehaviour
{
    public float firingDelay;
    public float firingVelocity = 10f;

    public GameManager gm;

    // Update is called once per frame
    void Start()
    {
        Invoke("Missile", firingDelay);
    }

    public void Missile()
    {
        firingDelay = Random.Range(5, 10);

        GameObject missile =
            (GameObject)Instantiate(Resources.Load("MissilePrefab"),
            this.transform.position, this.transform.rotation);
        Rigidbody rb = missile.GetComponent<Rigidbody>();
        rb.velocity = (this.transform.forward * firingVelocity * Time.deltaTime);

        Invoke("Missile", firingDelay);
    }
}
