using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase.Extensions;


// public class Signup : MonoBehaviour
// {

//     [SerializeField]
//     private TMP_InputField emailField;
//     [SerializeField]
//     private TMP_InputField passwordField;
//     [SerializeField]
//     private TMP_InputField displayNameField;
//     [SerializeField]
//     private TMP_Text errorText;

//     public void Signup()
//     {
//         // Obtain text from input fields
//         var email = emailField.text;
//         var password = passwordField.text;
//         var displayName = displayNameField.text;

//         Debug.Log($"[Signup] Email entered: '{email}'");

//         // Input validation
//         if (string.IsNullOrEmpty(email) || !email.Contains("@") || !email.Contains("."))
//         {
//             Debug.LogWarning("[Signup] Invalid email format");
//             ShowError("Empty or invalid e-mail address");
//             return;
//         }
//         else
//         {
//             Debug.Log("[Signup] Email validation passed");
//             ShowError(""); // clear error
//         }

//         FirebaseAuth
//             .DefaultInstance
//             .CreateUserWithEmailAndPasswordAsync(email, password)
//             .ContinueWithOnMainThread(task =>
//             {
//                 if (task.IsCanceled || task.IsFaulted)
//                 {
//                     ShowError("Error signing up");
//                     if (task.Exception != null) Debug.Log(task.Exception);
//                     return;
//                 }

//                 DatabaseManager.Instance.SetDisplayName(displayName, ShowError, UIManager.Instance.ShowLogin);
//             });
//     }

//     //This method displays an error message on the screen by putting text into a UI text element.
//     void ShowError(string error)
//     {
//         errorText.text = error;
//     }
// }