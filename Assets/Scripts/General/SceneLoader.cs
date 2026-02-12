using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Database;
using System;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void Sceneloader()
    {
        // Load the scene
        SceneManager.LoadScene(sceneName);

        // Teleport in  scene
        SceneManager.LoadScene(sceneName);
    }
}

