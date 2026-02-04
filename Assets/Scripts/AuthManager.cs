using UnityEngine;
using Firebase.Auth;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance { get; private set; }
    private FirebaseAuth auth;
    private FirebaseUser user;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void SetUser(FirebaseUser firebaseUser)
    {
        user = firebaseUser;
    }

    public string GetUserId()
    {
        return user != null ? user.UserId : null;
    }

    public string GetEmail()
    {
        return user != null ? user.Email : null;
    }
}