using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;
using System.Collections.Generic;

public class AchievementBehaviour : MonoBehaviour
{
    private DatabaseReference dbRef;
    private FirebaseAuth auth;

    private int REQUIRED_TIME = 180;

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
        FirebaseUser user = auth.CurrentUser;

        if (user == null)
        {
            Debug.LogError("Cannot check achievement. User not logged in.");
            return;
        }

        string userId = user.UserId;

        DatabaseReference sceneRef = dbRef
            .Child("players")
            .Child(userId)
            .Child("sceneEntries")
            .Child(sceneName)
            .Child("bestTime");

        sceneRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error reading bestTime: " + task.Exception);
                return;
            }

            if (task.Result.Exists)
            {
                int bestTime = int.Parse(task.Result.Value.ToString());

                Debug.Log(sceneName + " best time: " + bestTime);

                if (bestTime < REQUIRED_TIME)
                {
                    UnlockAchievement(userId, sceneName);
                }
                else
                {
                    Debug.Log("Time not fast enough for achievement.");
                }
            }
            else
            {
                Debug.Log("bestTime does not exist for: " + sceneName);
            }
        });
    }

    public void CheckAllScenes()
    {
        foreach (string scene in scenes)
        {
            CheckSingleScene(scene);
        }
    }

    private void UnlockAchievement(string userId, string sceneName)
    {
        string badgeKey = "Badge_" + sceneName;
        string badgeTitle = "Fastest " + sceneName + " Cleaner";

        DatabaseReference achievementRef = dbRef
            .Child("players")
            .Child(userId)
            .Child("achievement")
            .Child(badgeKey);

        achievementRef.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error checking achievement: " + task.Exception);
                return;
            }

            if (!task.Result.Exists)
            {
                achievementRef.SetValueAsync(badgeTitle).ContinueWithOnMainThread(saveTask =>
                {
                    if (saveTask.IsFaulted)
                    {
                        Debug.LogError("Failed to save achievement: " + saveTask.Exception);
                    }
                    else
                    {
                        Debug.Log("Achievement unlocked: " + badgeTitle);
                    }
                });
            }
            else
            {
                Debug.Log("Achievement already unlocked.");
            }
        });
    }
}
