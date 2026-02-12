using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;
using System.Collections.Generic;
public class AchievementBehaviour : MonoBehaviour
{
    private DatabaseReference dbRef;
    private FirebaseAuth auth;

    private float REQUIRED_TIME = 180f;

    private List<string> scenes = new List<string>
    {
        "Bedroom",
        "Park",
        "BenchCleaning",
        "ConvenienceStore"
    };

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
    }

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

    public void CheckAllScenes()
    {
        Debug.Log("=== CheckAllScenes called ===");
        foreach (string scene in scenes)
        {
            CheckSingleScene(scene);
        }
    }

    private void UnlockAchievement(string userId, string sceneName)
    {
        Debug.Log($"=== UnlockAchievement called for {sceneName} ===");
        
        string badgeKey = "Badge_" + sceneName;
        string badgeTitle = "Fastest " + sceneName + " Cleaner";
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

    // Optional: Method to read achievement data
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
}