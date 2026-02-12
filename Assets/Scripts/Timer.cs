/// <summary>
/// Timer.cs
/// This script handles the timer in the game
/// When player pauses the game and when player resume
/// It calculates how long player is in the game for
/// </summary>
/// <author> Lee Jia Lu </author>
/// <date> 09/12/2025 </date>
/// <StudentID> S10269187E </StudentID>
using UnityEngine;
using TMPro;
using Firebase.Database;
using Firebase.Auth;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float elapsedTime;
    private bool isPaused;
    private DatabaseReference dbRef;
    private FirebaseAuth auth;

    public string sceneName; // Assign via Inspector or SceneManager

    void Start()
    {
        // Initialize Firebase references
        auth = FirebaseAuth.DefaultInstance;
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        
        elapsedTime = 0f;
        isPaused = false;

        // Verify authentication on start
        if (auth.CurrentUser == null)
        {
            Debug.LogError("No user is currently logged in!");
        }
        else
        {
            Debug.Log($"Timer started for user: {auth.CurrentUser.UserId}");
        }
    }

    void Update()
    {
        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void StopTimer()
    {
        enabled = false; // stops Update
        SaveBestTime(elapsedTime);
        Debug.Log($"Timer.StopTimer() called! Final time: {elapsedTime}s");
    }

    private void SaveBestTime(float timeSpent)
    {
        // Check if user is authenticated
        if (auth == null || auth.CurrentUser == null)
        {
            Debug.LogError("Cannot save time: No user logged in!");
            return;
        }

        string userId = auth.CurrentUser.UserId;
        
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogError("User ID is null or empty!");
            return;
        }

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is not set!");
            return;
        }

        Debug.Log($"Saving time for user {userId} in scene {sceneName}");

        DatabaseReference sceneRef = dbRef
            .Child("players")
            .Child(userId)
            .Child("sceneEntries")
            .Child(sceneName);

        // First, get the current data
        sceneRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Failed to read data: {task.Exception}");
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                SceneEntryData entryData;

                if (snapshot.Exists)
                {
                    // Parse existing data
                    try
                    {
                        string json = snapshot.GetRawJsonValue();
                        entryData = JsonUtility.FromJson<SceneEntryData>(json);
                        
                        // Update best time if current time is better
                        if (timeSpent < entryData.bestTime)
                        {
                            entryData.bestTime = timeSpent;
                            Debug.Log($"New best time: {timeSpent}s (previous: {entryData.bestTime}s)");
                        }
                        else
                        {
                            Debug.Log($"Time {timeSpent}s did not beat best time of {entryData.bestTime}s");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error parsing existing data: {e.Message}");
                        entryData = new SceneEntryData();
                        entryData.bestTime = timeSpent;
                    }
                }
                else
                {
                    // No existing data, create new entry
                    entryData = new SceneEntryData();
                    entryData.bestTime = timeSpent;
                    Debug.Log($"First entry for this scene. Time: {timeSpent}s");
                }

                // Save the updated data
                string jsonData = JsonUtility.ToJson(entryData);
                sceneRef.SetRawJsonValueAsync(jsonData).ContinueWith(saveTask =>
                {
                    if (saveTask.IsFaulted)
                    {
                        Debug.LogError($"Failed to save data: {saveTask.Exception}");
                    }
                    else if (saveTask.IsCompleted)
                    {
                        Debug.Log($"Successfully saved time data for scene: {sceneName}");
                    }
                });
            }
        });
    }

    // Public method to get current elapsed time
    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    // Public method to check if timer is paused
    public bool IsPaused()
    {
        return isPaused;
    }
}