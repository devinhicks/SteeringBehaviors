using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerAngel : MonoBehaviour
{
    public delegate void KillBoid(Kinematic boid);
    public static event KillBoid OnKillBoid;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("boid")
            && collision.gameObject.name != "Player")
        {
            GameObject boid = collision.gameObject;
            Kinematic m_boid = collision.gameObject.GetComponent<Kinematic>();
            OnKillBoid?.Invoke(m_boid);
            Destroy(boid);
        }
    }
}
