using System;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using TMPro;

public class DatabaseManager : MonoBehaviour
{
    public TMP_InputField EmailInput;
    public TMP_InputField PasswordInput;
    public TMP_InputField DisplayInput;

    public static DatabaseManager Instance;
    public string DisplayName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // REQUIRED: Firebase init
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                Debug.Log("Firebase initialized successfully");
            }
            else
            {
                Debug.LogError("Firebase dependency error: " + task.Result);
            }
        });
    }

    // ---------------- AUTH ----------------

    public void SignUp()
    {
        string email = EmailInput.text.Trim();
        string password = PasswordInput.text.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("Email or password is empty");
            return;
        }

        Debug.Log($"Signing up with email: '{email}'");

        FirebaseAuth.DefaultInstance
            .CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError(task.Exception);
                    return;
                }

                Debug.Log($"User created: {task.Result.User.UserId}");

                // Create user node in database
                FirebaseDatabase.DefaultInstance
                    .RootReference
                    .Child("players")
                    .Child(task.Result.User.UserId)
                    .Child("displayName")
                    .SetValueAsync(DisplayInput.text.Trim());
            });
    }

    public void SignIn()
    {
        string email = EmailInput.text.Trim();
        string password = PasswordInput.text.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("Email or password is empty");
            return;
        }

        Debug.Log($"Signing in with email: '{email}'");

        FirebaseAuth.DefaultInstance
            .SignInWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError(task.Exception);
                    return;
                }

                Debug.Log($"User logged in: {task.Result.User.UserId}");
            });
    }

    public void SignOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        Debug.Log("User signed out");
    }

    private bool IsAuthenticated()
    {
        return FirebaseAuth.DefaultInstance.CurrentUser != null;
    }

    private string CurrentUserId()
    {
        return FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    }


    public void SetDisplayName(string displayName, Action<string> onError, Action onSuccess)
    {
        if (!IsAuthenticated())
        {
            Debug.LogError("User not logged in");
            return;
        }

        FirebaseDatabase.DefaultInstance
            .RootReference
            .Child("players")
            .Child(CurrentUserId())
            .Child("displayName")
            .SetValueAsync(displayName)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError(task.Exception);
                    onError(task.Exception.ToString());
                    return;
                }

                onSuccess();
            });
    }

    public void GetDisplayName()
    {
        if (!IsAuthenticated())
        {
            Debug.LogError("User not logged in");
            return;
        }

        FirebaseDatabase.DefaultInstance
            .RootReference
            .Child("players")
            .Child(CurrentUserId())
            .Child("displayName")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError(task.Exception);
                    return;
                }

                if (task.Result.Exists)
                {
                    DisplayName = task.Result.Value.ToString();
                    Debug.Log("Display name: " + DisplayName);
                }
            });
    }

    public void UpdateScore(float score)
    {
        if (!IsAuthenticated())
        {
            Debug.LogError("User not logged in");
            return;
        }

        FirebaseDatabase.DefaultInstance
            .RootReference
            .Child("players")
            .Child(CurrentUserId())
            .Child("score")
            .SetValueAsync(score);
    }
}