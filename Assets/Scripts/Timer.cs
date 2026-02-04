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
using System.Collections.Generic;

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
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
        elapsedTime = 0f;
        isPaused = false;
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
    }

    private void SaveBestTime(float timeSpent)
    {
        string userId = AuthManager.Instance.GetUserId();
        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogError("No user logged in!");
            return;
        }

        DatabaseReference sceneRef = dbRef.Child("players").Child(userId).Child("sceneEntries").Child(sceneName);

        sceneRef.RunTransaction(mutableData =>
        {
            var dict = mutableData.Value as Dictionary<string, object>;
            if (dict == null)
            {
                dict = new Dictionary<string, object>();
                dict["bestTime"] = timeSpent;
                dict["entryCount"] = 1;
            }
            else
            {
                float oldBest = Convert.ToSingle(dict["bestTime"]);
                int oldCount = Convert.ToInt32(dict["entryCount"]);

                if (timeSpent < oldBest)
                {
                    dict["bestTime"] = timeSpent;
                }

                dict["entryCount"] = oldCount + 1;
            }

            dict["lastTimestamp"] = DateTime.UtcNow.ToString("o");
            mutableData.Value = dict;
            return TransactionResult.Success(mutableData);
        });
    }
}