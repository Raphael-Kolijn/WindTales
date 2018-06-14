using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonScript : MonoBehaviour {

    // Exit the game back to the plaza
    public void exit(string scene)
    {
        SceneManager.LoadScene(0);
    }

    // Try again
    public void restart(string scene)
    {
        SceneManager.LoadScene(5);
    }
}
