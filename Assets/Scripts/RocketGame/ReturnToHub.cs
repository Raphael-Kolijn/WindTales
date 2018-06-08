using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class ReturnToHub : MonoBehaviour
{
    public void Return()
    {
        EditorSceneManager.LoadScene(0);
    }
}
