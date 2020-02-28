using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PriorityGame : MonoBehaviour
{
    Kinematic newBoid;
    public int count = 0;

    public GameObject player;
    public GameObject wall;

    public delegate void AddBoid(Kinematic boid);
    public static event AddBoid OnAddBoid;

    public void SwitchMode()
    {
        if (SceneManager.GetActiveScene().buildIndex == 10)
        {
            SceneManager.LoadScene(11);
        }
        if (SceneManager.GetActiveScene().buildIndex == 11)
        {
            SceneManager.LoadScene(10);
        }
    }
    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        newBoid = (Kinematic)Resources.Load("boidy", typeof(Kinematic));
    //        newBoid.name = "boidy_" + count;
    //        newBoid.transform.Translate(getRand(), 0, getRand());
    //        Flocker flock = newBoid.GetComponent<Flocker>();
    //        flock.cohereTarget = player;
    //        flock.avoidThis = wall;
    //        Kinematic kin = newBoid.GetComponent<Kinematic>();
    //        kin.target = player;
    //        Instantiate(newBoid);
    //        count++;

    //        OnAddBoid?.Invoke(kin);
    //    }
    //}

    public float getRand()
    {
        return Random.Range(-20.0f, 20.0f);
    }
}
