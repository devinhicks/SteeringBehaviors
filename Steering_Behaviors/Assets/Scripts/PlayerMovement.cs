using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // This is a reference to the Rigidbody component called 'rb'
    public Rigidbody rb;

    public float moveForce = 25f;

    public GameManager gm;

    // We marked this as "Fixed"Update because we
    // are using it to mess with physics
    void FixedUpdate()
    {
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))     // If the player is perssing the "d" key
        {
            // Add a force to the right
            rb.AddForce(moveForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))      // If the player is pressing the "a" key
        {
            // Add a force to the left
            rb.AddForce(-moveForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))     // If the player is perssing the "d" key
        {
            // Add a force to the right
            rb.AddForce(0, 0, moveForce * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))      // If the player is pressing the "a" key
        {
            // Add a force to the left
            rb.AddForce(0, 0, -moveForce * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (transform.position.y < 1)
        {
            gm.EndGame();
        }
    }
}
