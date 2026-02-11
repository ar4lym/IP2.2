// using UnityEngine;
// using TMPro;

// public class Trashmanager : MonoBehaviour
// {
//     public static Trashmanager Instance;

//     public int totalTrash = 5;

//     private int trashCollected = 0;

//     public TextMeshProUGUI trashCounterText;

//     private void Awake()
//     {
//         Instance = this;
//     }

//     private void Start()
//     {
//         UpdateTrashUI();
//     }

//     public int GetCollectedTrash()
//     {
//         return trashCollected;
//     }


//     public void TrashCollected()
//     {
//         trashCollected++;
//         UpdateTrashUI();
//     }


//     void UpdateTrashUI()
//     {
//         trashCounterText.text = trashCollected + " / " + totalTrash;
//     }
// }


using UnityEngine;
using TMPro;

public class Trashmanager : MonoBehaviour
{
    public static Trashmanager Instance;

    [Header("Trash Settings")]
    public int totalTrash = 5;

    private int trashCollected = 0;
    public bool trashCompleted = false;   // completion flag

    [Header("UI")]
    public TextMeshProUGUI trashCounterText;

    [Header("Other Systems")]
    public PuddleManager puddleManager;   // drag PuddleManager here
    public Timer timer;                   // drag Timer here

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateTrashUI();
    }

    public int GetCollectedTrash()
    {
        return trashCollected;
    }

    public void TrashCollected()
    {
        trashCollected++;
        UpdateTrashUI();

        // Mark trash done
        if (trashCollected >= totalTrash)
        {
            trashCompleted = true;
            Debug.Log("Trash completed!");
            TryStopTimer();
        }
    }

    // AND condition lives here (clean & simple)
    public void TryStopTimer()
    {
        if (puddleManager == null)
        {
            Debug.LogWarning("PuddleManager not assigned!");
            return;
        }

        if (trashCompleted && puddleManager.puddlesCompleted)
        {
            Debug.Log("All tasks done — stopping timer");

            if (timer != null)
                timer.StopTimer();
            else
                Debug.LogWarning("Timer not assigned!");
        }
    }

    void UpdateTrashUI()
    {
        if (trashCounterText != null)
            trashCounterText.text = trashCollected + " / " + totalTrash;
    }
}