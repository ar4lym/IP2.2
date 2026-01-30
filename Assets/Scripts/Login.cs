// using TMPro;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// // using Firebase.Auth;
// // using Firebase.Extensions;

//  public class Login : MonoBehaviour
// {
//     public TMP_InputField username;
//     public TMP_InputField password;
//     public TMP_Text message;



//     public void Loggingin()
//     {
//         if (username.text == "123" && password.text == "123")
//         {
//            message.text = ("Login Successful");
//            Debug.Log("Login Successful");
//            SceneManager.LoadScene("BasicScene");
//         }
//         else
//         {
//            message.text = ("Invalid username or password");
//         }
//     }
// }
// //     [SerializeField]
// //     private TMP_Text title;
// //     [SerializeField]
// //     private TMP_InputField emailField;
// //     [SerializeField]
// //     private TMP_InputField passwordField;
// //     [SerializeField]
// //     private TMP_Text errorText;

// //     public void Login()
// //     {
// //         // Obtain text from input fields
// //         var email = emailField.text;
// //         var password = passwordField.text;
// //         Debug.Log($"[Login] Email entered: '{email}'");

// //         // Input validation
// //         if (string.IsNullOrEmpty(email) || !email.Contains("@") || !email.Contains("."))
// //         {
// //             ShowError("Empty or invalid e-mail address");
// //             return;
// //         }
// //         else
// //         {   
// //             ShowError(""); // clear error
// //         }

// //         FirebaseAuth
// //             .DefaultInstance
// //             .SignInWithEmailAndPasswordAsync(email, password)
// //             .ContinueWithOnMainThread(task =>
// //             {
// //                 if (task.IsCanceled || task.IsFaulted)
// //                 {
// //                     if (task.Exception != null) Debug.Log(task.Exception);
// //                     ShowError("Error logging in");
// //                     return;
// //                 }

// //                 UIManager.Instance.ShowGame();
// //             });
// //     }

// //     void ShowError(string error)
// //     {
// //         errorText.text = error;
// //     }

// //     private Coroutine titleAnimationCoroutine;

// //     void OnEnable()
// //     {
// //         // Animate the title colours when GameObject is active
// //         StartCoroutine(ColourAnimator.Animate(title));
// //     }

// //     void OnDisable()
// //     {
// //         // Stop animating the title colours when GameObject is disabled
// //         if (titleAnimationCoroutine != null) StopCoroutine(titleAnimationCoroutine);
// //     }
// // }



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
                Debug.Log("User logged in successfully! Navigating to Third Page!");
                uiManager.ShowThirdPage(); // Navigate to ThirdPage
                var uid = task.Result.User.UserId;
                Debug.Log($"Logged in user UID: {uid}");
            }
        });
    }
}
