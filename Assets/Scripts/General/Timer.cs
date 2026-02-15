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

/// <summary>
/// Manages game timer functionality with pause/resume capabilities and Firebase integration.
/// Tracks elapsed time, displays it in UI, and saves best completion times to Firebase database.
/// </summary>
public class Timer : MonoBehaviour
{
    /// <summary>
    /// UI text element that displays the formatted timer (MM:SS).
    /// </summary>
    [SerializeField] TextMeshProUGUI timerText;

    /// <summary>
    /// Total elapsed time in seconds since the timer started.
    /// </summary>
    private float elapsedTime;
    
    /// <summary>
    /// Indicates whether the timer is currently paused.
    /// </summary>
    private bool isPaused;

    /// <summary>
    /// Reference to the Firebase Realtime Database for storing player times.
    /// </summary>
    private DatabaseReference dbRef;

    /// <summary>
    /// Reference to Firebase Authentication for identifying the current user.
    /// </summary>
    private FirebaseAuth auth;

    /// <summary>
    /// Name of the current scene, used as a key for storing times in Firebase.
    /// Assign via Inspector or SceneManager.
    /// </summary>
    public string sceneName; // Assign via Inspector or SceneManager

    /// <summary>
    /// Initializes Firebase references and verifies user authentication.
    /// Starts the timer at zero.
    /// </summary>
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

    /// <summary>
    /// Updates the timer each frame if not paused.
    /// Formats elapsed time as MM:SS and updates the UI text.
    /// </summary>
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

    /// <summary>
    /// Pauses the timer and freezes game time.
    /// Sets Time.timeScale to 0.
    /// </summary>
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Resumes the timer and restores normal game time.
    /// Sets Time.timeScale to 1.
    /// </summary>
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Stops the timer, saves the completion time to Firebase, and checks for achievements.
    /// Disables further timer updates.
    /// </summary>
    public void StopTimer()
    {
        enabled = false;
        SaveBestTime(elapsedTime);

        // Check for achievement
        AchievementBehaviour achievementBehaviour = FindObjectOfType<AchievementBehaviour>();
        if (achievementBehaviour != null)
        {
            achievementBehaviour.CheckSingleScene(sceneName);
        }
        else
        {
            Debug.LogError("AchievementBehaviour not found in scene!");
        }
    }

    /// <summary>
    /// Saves the completion time to Firebase if it's better than the existing best time.
    /// Creates a new entry if this is the first completion of the scene.
    /// </summary>
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

    /// <summary>
    /// Gets the current elapsed time since the timer started.
    /// </summary>
    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    /// <summary>
    /// Checks whether the timer is currently paused.
    /// </summary>
    public bool IsPaused()
    {
        return isPaused;
    }
}