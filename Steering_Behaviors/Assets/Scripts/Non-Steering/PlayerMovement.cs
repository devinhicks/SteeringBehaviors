using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // This is a reference to the Rigidbody component called 'rb'
    public Rigidbody rb;

    public float moveForce = 25f;

    public float rotateForce = 3f;

    public GameManager gm;

    // We marked this as "Fixed"Update because we

    void FixedUpdate()
    {
        if (Input.GetKey("d"))     // If the player is perssing the "d" key
        {
            // Add a force to the right
            rb.AddForce(moveForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a"))      // If the player is pressing the "a" key
        {
            // Add a force to the left
            rb.AddForce(-moveForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("w"))     // If the player is perssing the "d" key
        {
            // Add a force forward
            rb.AddForce(0, 0, moveForce * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (Input.GetKey("s"))      // If the player is pressing the "a" key
        {
            // Add a force backward
            rb.AddForce(0, 0, -moveForce * Time.deltaTime, ForceMode.VelocityChange);
        }

        // if player falls below 0
        if (transform.position.y < 0)
        {
            // call EndGame from game manager
            gm.EndGame();
        }
    }
}
