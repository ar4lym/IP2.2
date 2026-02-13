/// <summary>
/// SceneLoader.cs
/// Handles loading of scenes in the game,
/// allowing for transitions between different areas or levels.
/// </summary>
/// <author> Aralyn Han Zi Ning </author>
/// <date> 27/01/2026 </date>
/// <StudentID> S10267170A </StudentID>
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Database;
using System;

/// <summary>
/// Handles scene transitions by loading a specified scene when triggered.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;

    /// <summary>
    /// Loads the selected scene.
    /// This method can be called from UI buttons or game events.
    /// </summary>
    public void Sceneloader()
    {
        // Load the scene
        SceneManager.LoadScene(sceneName);

        // Teleport in  scene
        SceneManager.LoadScene(sceneName);
    }
}

