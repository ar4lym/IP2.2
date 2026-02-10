using UnityEngine;
using Firebase.Database;
using System.Collections.Generic;
using Firebase.Extensions;
using TMPro;
using Firebase.Auth;
using Firebase;
using System;

public class DatabaseManager : MonoBehaviour
{
    public TMP_InputField EmailInput;
    public TMP_InputField PasswordInput;
    public TMP_InputField DisplayInput;

    public static DatabaseManager Instance;
    public string userName;

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

        FirebaseAuth.DefaultInstance
            .CreateUserWithEmailAndPasswordAsync(email, password)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError(task.Exception);
                    return;
                }

                var user = task.Result.User;

                FirebaseDatabase.DefaultInstance
                    .RootReference
                    .Child("players")
                    .Child(user.UserId)
                    .Child("email")
                    .SetValueAsync(user.Email);

                FirebaseDatabase.DefaultInstance
                    .RootReference
                    .Child("players")
                    .Child(user.UserId)
                    .Child("userName")
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


    public void SetUserName(string userName, Action<string> onError, Action onSuccess)
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
            .Child("userName")
            .SetValueAsync(userName)
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

    public void GetUserName()
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
            .Child("userName")
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
                    userName = task.Result.Value.ToString();
                    Debug.Log("Display name: " + userName);
                }
            });
    }
}