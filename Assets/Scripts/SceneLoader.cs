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

        // Log to Firebase
        LogSceneEntry(sceneName);
    }

    private void LogSceneEntry(string scene)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        string entryId = reference.Child("sceneEntries").Push().Key;
        var entryData = new SceneEntry
        {
            sceneName = scene,
            timestamp = DateTime.UtcNow.ToString("o")
        };
        Debug.Log($"Logging scene entry: {entryData.sceneName} at {entryData.timestamp}");
        
        string json = JsonUtility.ToJson(entryData);
        reference.Child("sceneEntries").Child(entryId).SetRawJsonValueAsync(json);
    }
}

