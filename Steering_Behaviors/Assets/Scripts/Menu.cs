using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void SFA()
    {
        SceneManager.LoadScene(1);
    }

    public void fancySFA()
    {
        SceneManager.LoadScene(2);
    }

    public void AFL()
    {
        SceneManager.LoadScene(3);
    }

    public void fancyAFL()
    {
        SceneManager.LoadScene(4);
    }
}
