﻿using System.Collections;
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

    public void PPS()
    {
        SceneManager.LoadScene(5);
    }

    public void fancyPPS()
    {
        SceneManager.LoadScene(6);
    }

    public void CAOA()
    {
        SceneManager.LoadScene(7);
    }

    public void FancyCaOA()
    {
        SceneManager.LoadScene(8);
    }

    public void BlendedSteering()
    {
        SceneManager.LoadScene(9);
    }

    public void PrioritySteering()
    {
        SceneManager.LoadScene(10);
    }

    public void Dijkstra()
    {
        SceneManager.LoadScene(12);
    }

    public void FancyDijkstra()
    {
        SceneManager.LoadScene(13);
    }
}
