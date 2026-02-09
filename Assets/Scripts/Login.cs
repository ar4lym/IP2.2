using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField emailInput; //attatched to UIManager in unity
    public TMP_InputField passwordInput;
    private bool isFirebaseReady = false;

    public UIManager uiManager; 

    void Start()
    {
        // Initialize Firebase before proceeding 
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                isFirebaseReady = true; //confirms if ready for usage 
                Debug.Log("Firebase is ready!");
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }

    public void LogIn()
    {
        if (!isFirebaseReady)
        {
            Debug.LogWarning("Firebase not ready yet. Please wait...");
            return;
        }

        var loginTask = FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text, passwordInput.text);
        loginTask.ContinueWithOnMainThread(task =>          //log into fb account 
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log("Error logging in!");
                return;
            }

            if (task.IsCompleted)
            {
                Debug.Log("User logged in successfully!");
                //uiManager.ShowThirdPage(); // Navigate to ThirdPage
                var uid = task.Result.User.UserId;
                Debug.Log($"Logged in user UID: {uid}");
            }
        });
    }
}
