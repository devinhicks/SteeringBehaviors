﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool gameHasEnded = false;

    public float restartDelay = 1f;

    public GameObject completeLevelUI;
    public GameObject gameOverUI;
    public GameObject InstructionsUI;


    public GameObject laser;
    public GameObject player;

    public int collected;

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
        laser.SetActive(false);
        player.SetActive(false);
        Invoke("Application.Quit()", 1f);
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            gameOverUI.SetActive(true);
            Invoke("Restart", restartDelay); // Restart after specfied delay
        }
    }

    void Restart()
    {
        // if player does not end game, restart the current scene/level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}