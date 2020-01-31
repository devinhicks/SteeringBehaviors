using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset; // distance between player and camera

    void Update()
    {
        // change position of camera to offset behind player
        transform.position = player.position + offset;

        if (Input.GetKey(KeyCode.Escape))
        {
            // SceneManager.LoadScene("Menu");
        }

        //Invoke("InstructionsUI.SetActive(false)", 10f);
    }
}