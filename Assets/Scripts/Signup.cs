// using UnityEngine;
// using TMPro;
// using Firebase.Auth;
// using Firebase.Extensions;


// // public class Signup : MonoBehaviour
// // {

// //     [SerializeField]
// //     private TMP_InputField emailField;
// //     [SerializeField]
// //     private TMP_InputField passwordField;
// //     [SerializeField]
// //     private TMP_InputField displayNameField;
// //     [SerializeField]
// //     private TMP_Text errorText;

// //     public void Signup()
// //     {
// //         // Obtain text from input fields
// //         var email = emailField.text;
// //         var password = passwordField.text;
// //         var displayName = displayNameField.text;

// //         Debug.Log($"[Signup] Email entered: '{email}'");

// //         // Input validation
// //         if (string.IsNullOrEmpty(email) || !email.Contains("@") || !email.Contains("."))
// //         {
// //             Debug.LogWarning("[Signup] Invalid email format");
// //             ShowError("Empty or invalid e-mail address");
// //             return;
// //         }
// //         else
// //         {
// //             Debug.Log("[Signup] Email validation passed");
// //             ShowError(""); // clear error
// //         }

// //         FirebaseAuth
// //             .DefaultInstance
// //             .CreateUserWithEmailAndPasswordAsync(email, password)
// //             .ContinueWithOnMainThread(task =>
// //             {
// //                 if (task.IsCanceled || task.IsFaulted)
// //                 {
// //                     ShowError("Error signing up");
// //                     if (task.Exception != null) Debug.Log(task.Exception);
// //                     return;
// //                 }

// //                 DatabaseManager.Instance.SetDisplayName(displayName, ShowError, UIManager.Instance.ShowLogin);
// //             });
// //     }

// //     //This method displays an error message on the screen by putting text into a UI text element.
// //     void ShowError(string error)
// //     {
// //         errorText.text = error;
// //     }
// // }





using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using TMPro;

public class Signup : MonoBehaviour
{
    public TMP_InputField emailInput; //attatched to UIManager in unity
    public TMP_InputField passwordInput;
    public UIManager uiManager; 

    private bool isFirebaseReady = false;

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

    public void SignUp()
    {
        if (!isFirebaseReady)
        {
            Debug.LogWarning("Firebase not ready yet. Please wait...");
            return;
        }

        var createTask = FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailInput.text, passwordInput.text);
        createTask.ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled) //checks if tasks works
            {
                Debug.Log("Error signing up!");
                return;
            }

            if (task.IsCompleted)
            {
                Debug.Log("User created successfully! Navigating to Third Page!");
                //uiManager.ShowThirdPage(); // Navigate to ThirdPage if it works
                var uid = task.Result.User.UserId; //Retrieves the unique Firebase user ID
                Debug.Log($"Created user UID: {uid}");
            }
        });
    }
}