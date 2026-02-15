/// <summary>
/// AchievementBehaviour.cs
/// Manages achievement unlocking logic based on scene completion times.
/// Listens for best time updates in Firebase and awards achievements
/// when requirements are met.
/// </summary>
/// <author> Lee Jia Lu </author>
/// <date> 23/01/2026 </date>
/// <StudentID> S10269187E </StudentID>
using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;
using System.Collections.Generic;

/// <summary>
/// Manages achievement tracking and unlocking based on scene completion times.
/// Integrates with Firebase to store and retrieve achievement data for authenticated users.
/// Automatically checks if players complete scenes under the required time threshold.
/// </summary>
public class AchievementBehaviour : MonoBehaviour

{
    /// <summary>
    /// Reference to the Firebase Realtime Database for storing and retrieving achievement data.
    /// </summary>
    private DatabaseReference dbRef;
    
    /// <summary>
    /// Reference to Firebase Authentication for identifying the current user.
    /// </summary>
    private FirebaseAuth auth;

    /// <summary>
    /// The maximum time (in seconds) allowed to unlock speed-based achievements.
    /// Default is 180 seconds (3 minutes).
    /// </summary>

    private float REQUIRED_TIME = 180f;
    
    /// <summary>
    /// List of scene names that have associated achievements.
    /// </summary>
    private List<string> scenes = new List<string>
    {
        "Bedroom",
        "Park",
        "BenchCleaning",
        "ConvenienceStore",
        "Tiles"
    };

    /// <summary>
    /// Initializes Firebase references and sets up listeners for best time updates on all scenes.
    /// Verifies user authentication status.
    /// </summary>
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;

        if (auth.CurrentUser == null)
        {
            Debug.LogError("User is NOT logged in!");
        }
        else
        {
            Debug.Log("Logged in as: " + auth.CurrentUser.UserId);
        }
        foreach (string scene in scenes)
        {
            ListenForBestTime(scene);
        }
    }
    /// <summary>
    /// Checks if a specific scene's best time qualifies for an achievement unlock.
    /// Reads the scene entry from Firebase and unlocks the achievement if time requirements are met.
    /// </summary>
    public void CheckSingleScene(string sceneName)
    {
        Debug.Log($"=== CheckSingleScene called for: {sceneName} ===");

        FirebaseUser user = auth.CurrentUser;

        if (user == null)
        {
            Debug.LogError("Cannot check achievement. User not logged in.");
            return;
        }

        string userId = user.UserId;
        Debug.Log($"Checking for user: {userId}");

        // Read the entire scene entry to get bestTime
        DatabaseReference sceneRef = dbRef
            .Child("players")
            .Child(userId)
            .Child("sceneEntries")
            .Child(sceneName);

        Debug.Log($"Reading from path: players/{userId}/sceneEntries/{sceneName}");

        sceneRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            Debug.Log($"GetValueAsync completed for {sceneName}");

            if (task.IsFaulted)
            {
                Debug.LogError($"Error reading scene data for {sceneName}: {task.Exception}");
                return;
            }

            if (task.Result.Exists)
            {
                Debug.Log($"Scene entry EXISTS for {sceneName}");
                DataSnapshot snapshot = task.Result;

                // Check if bestTime exists
                if (snapshot.Child("bestTime").Exists)
                {
                    string bestTimeStr = snapshot.Child("bestTime").Value.ToString();
                    Debug.Log($"bestTime value (string): {bestTimeStr}");

                    float bestTime = float.Parse(bestTimeStr);

                    Debug.Log($"{sceneName} best time: {bestTime}s (required: {REQUIRED_TIME}s or less)");

                    if (bestTime <= REQUIRED_TIME)
                    {
                        Debug.Log($"✓ Time qualifies! Calling UnlockAchievement...");
                        UnlockAchievement(userId, sceneName);
                    }
                    else
                    {
                        Debug.Log($"✗ Time {bestTime}s not fast enough for achievement (required: {REQUIRED_TIME}s or less).");
                    }
                }
                else
                {
                    Debug.LogWarning($"bestTime child does NOT exist for: {sceneName}");
                    Debug.Log($"Available children: {snapshot.ChildrenCount}");
                    foreach (var child in snapshot.Children)
                    {
                        Debug.Log($"  - {child.Key}: {child.Value}");
                    }
                }
            }
            else
            {
                Debug.LogWarning($"No scene entry found for: {sceneName}");
            }
        });
    }

    /// <summary>
    /// Checks all registered scenes for achievement qualification.
    /// Iterates through the scenes list and calls CheckSingleScene for each.
    /// </summary>
    public void CheckAllScenes()
    {
        Debug.Log("=== CheckAllScenes called ===");
        foreach (string scene in scenes)
        {
            CheckSingleScene(scene);
        }
    }

    /// <summary>
    /// Unlocks an achievement for a specific scene if not already unlocked.
    /// Creates and saves achievement data to Firebase under the player's achievement node.
    /// </summary>
    private void UnlockAchievement(string userId, string sceneName)
    {
        Debug.Log($"=== UnlockAchievement called for {sceneName} ===");

        string badgeKey = "Badge_" + sceneName;
        string badgeTitle = "Fastest " + sceneName + " Completer";
        string badgeDescription = "Completed the " + sceneName + " scene in under 3 minutes.";

        Debug.Log($"Badge key: {badgeKey}");
        Debug.Log($"Badge title: {badgeTitle}");
        Debug.Log($"Badge description: {badgeDescription}");

        // Path: players -> uid -> sceneEntries -> achievement -> badgeKey
        DatabaseReference achievementRef = dbRef
            .Child("players")
            .Child(userId)
            .Child("achievement")
            .Child(badgeKey);

        string achievementPath = $"players/{userId}/sceneEntries/achievement/{badgeKey}";
        Debug.Log($"Achievement path: {achievementPath}");

        achievementRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            Debug.Log($"Achievement check completed for {badgeKey}");

            if (task.IsFaulted)
            {
                Debug.LogError($"Error checking achievement for {sceneName}: {task.Exception}");
                return;
            }

            if (!task.Result.Exists)
            {
                Debug.Log($"Achievement does NOT exist yet. Creating new achievement...");

                // Create achievement object
                AchievementData achievementData = new AchievementData(badgeTitle, sceneName, badgeDescription);

                // Convert to JSON and save
                string jsonData = JsonUtility.ToJson(achievementData);
                Debug.Log($"JSON to save: {jsonData}");

                achievementRef.SetRawJsonValueAsync(jsonData).ContinueWithOnMainThread(saveTask =>
                {
                    Debug.Log($"SetRawJsonValueAsync completed for {badgeKey}");

                    if (saveTask.IsFaulted)
                    {
                        Debug.LogError($"Failed to save achievement {badgeKey}: {saveTask.Exception}");
                    }
                    else if (saveTask.IsCompleted)
                    {
                        Debug.Log($"🏆 Achievement unlocked: {badgeTitle}");
                        Debug.Log($"Achievement data created at: {achievementPath}");
                    }
                    else
                    {
                        Debug.LogWarning($"Save task status: Canceled={saveTask.IsCanceled}, Faulted={saveTask.IsFaulted}");
                    }
                });
            }
            else
            {
                Debug.Log($"Achievement '{badgeTitle}' already unlocked.");
            }
        });
    }

    /// <summary>
    /// Retrieves and logs achievement data for a specific scene from Firebase.
    /// </summary>
    public void GetAchievement(string userId, string sceneName)
    {
        string badgeKey = "Badge_" + sceneName;

        DatabaseReference achievementRef = dbRef
            .Child("players")
            .Child(userId)
            .Child("achievement")
            .Child(badgeKey);

        achievementRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Error reading achievement: {task.Exception}");
                return;
            }

            if (task.Result.Exists)
            {
                string json = task.Result.GetRawJsonValue();
                AchievementData achievement = JsonUtility.FromJson<AchievementData>(json);

                Debug.Log($"Achievement: {achievement.title}");
                Debug.Log($"Scene: {achievement.sceneName}");
                Debug.Log($"Description: {achievement.description}");
            }
            else
            {
                Debug.Log($"Achievement {badgeKey} not found.");
            }
        });
    }

    /// <summary>
    /// Sets up a real-time listener for best time updates on a specific scene.
    /// Automatically attempts to unlock achievements when best times are updated in Firebase.
    /// </summary>
    public void ListenForBestTime(string sceneName)
    {
        FirebaseUser user = auth.CurrentUser;

        if (user == null)
        {
            Debug.LogError("User not logged in.");
            return;
        }

        string userId = user.UserId;

        DatabaseReference bestTimeRef = dbRef
            .Child("players")
            .Child(userId)
            .Child("sceneEntries")
            .Child(sceneName)
            .Child("bestTime");

        bestTimeRef.ValueChanged += (sender, args) =>
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError("Database error: " + args.DatabaseError.Message);
                return;
            }

            if (args.Snapshot.Exists)
            {
                float bestTime = float.Parse(args.Snapshot.Value.ToString());

                Debug.Log($"bestTime updated for {sceneName}: {bestTime}s");

                if (bestTime <= REQUIRED_TIME)
                {
                    UnlockAchievement(userId, sceneName);
                }
            }
        };
    }
}