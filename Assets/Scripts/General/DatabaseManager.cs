/// <summary>
/// DatabaseManager.cs
/// Manages Firebase authentication and database operations,
/// including user sign-up, sign-in, sign-out, and profile data handling.
/// </summary>
/// <author> Lee Jia Lu </author>
/// <date> 23/01/2026 </date>
/// <StudentID> S10269187E </StudentID>
using UnityEngine;
using Firebase.Database;
using System.Collections.Generic;
using Firebase.Extensions;
using TMPro;
using Firebase.Auth;
using Firebase;
using System;

/// <summary>
/// Handles Firebase authentication and database interactions.
/// Provides methods for signing up, signing in, signing out,
/// and managing user profile data.
/// </summary>
public class DatabaseManager : MonoBehaviour
{
    public TMP_InputField EmailInput;
    public TMP_InputField PasswordInput;
    public TMP_InputField DisplayInput;

    public static DatabaseManager Instance;
    public string userName;

    public GameObject mainCanvas;

    /// <summary>
    /// Initializes Firebase dependencies and hides the main canvas until login.
    /// </summary>
    void Start()
    {
        mainCanvas.SetActive(false);   // hide before login

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

    /// <summary>
    /// Registers a new user with Firebase Authentication
    /// and stores their email and display name in the database.
    /// </summary>
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
                mainCanvas.SetActive(true);

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

    /// <summary>
    /// Signs in an existing user with Firebase Authentication.
    /// </summary>
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

            mainCanvas.SetActive(true);
        });
    }

    /// <summary>
    /// Signs out the current user.
    /// </summary>
    public void SignOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        Debug.Log("User signed out");
    }

    /// <summary>
    /// Checks if a user is currently authenticated.
    /// </summary>
    private bool IsAuthenticated()
    {
        return FirebaseAuth.DefaultInstance.CurrentUser != null;
    }

    /// <summary>
    /// Returns the current user's ID.
    /// </summary>
    private string CurrentUserId()
    {
        return FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    }

    /// <summary>
    /// Sets the current user's display name in the database.
    /// </summary>
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

    /// <summary>
    /// Retrieves the current user's display name from the database.
    /// </summary>
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
