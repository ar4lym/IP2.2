/// <summary>
/// Trashmanager.cs
/// This script is a script for 1 of the 3 ai
/// this controls the ghost in the second scene
/// it chases you and you die upon touching it.
/// </summary>
/// <author> Lee Jia Lu </author>
/// <date> 25/01/2026 </date>
/// <StudentID> S10269187E </StudentID>
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages trash collection, updates UI counter, checks completion status,
/// and coordinates with PuddleManager and Timer to determine task completion.
/// </summary>
public class Trashmanager : MonoBehaviour
{
    public static Trashmanager Instance;
    public GameObject BedroomGameManager;
    public string sceneVariationName = "SceneVariation";

    [Header("Trash Settings")]
    public int totalTrash = 5;

    public int trashCollected = 0;
    public bool trashCompleted = false;   // completion flag

    [Header("UI")]
    public TextMeshProUGUI trashCounterText;

    [Header("Other Systems")]
    public PuddleManager puddleManager;   // drag PuddleManager here
    public Timer timer;                   // drag Timer here

    /// <summary>
    /// Sets singleton instance on awake.
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }
    /// <summary>
    /// Initialises trash UI at start.
    /// </summary>
    private void Start()
    {
        UpdateTrashUI();
    }

    /// <summary>
    /// Returns the current number of collected trash.
    /// </summary>
    public int GetCollectedTrash()
    {
        return trashCollected;
    }
    
    /// <summary>
    /// Called when a trash item is collected.
    /// Updates counter and checks for completion.
    /// </summary>
    public void TrashCollected()
    {
        trashCollected++;
        UpdateTrashUI();

        // Mark trash done
        if (trashCollected >= totalTrash)
        {
            BedroomGameManager.GetComponent<BedroomGameManager>().CheckCompletion();
            trashCompleted = true;
            Debug.Log("Trash completed!");
            TryStopTimer();
        }
    }

    /// <summary>
    /// Checks whether both trash and puddles are completed.
    /// Stops the timer when all tasks are done.
    /// </summary>
    public void TryStopTimer()
    {
        if (puddleManager == null)
        {
            Debug.LogWarning("PuddleManager not assigned!");
            return;
        }

        if (trashCompleted && puddleManager.puddlesCompleted)
        {
            Debug.Log("All tasks done — stopping timer and loading SceneVariation");

            if (timer != null)
                timer.StopTimer();

            // Small delay so player sees completion (optional but nice)
            //Invoke(nameof(LoadSceneVariation), 1f);
        }
    }

    /// <summary>
    /// Loads the next scene after all tasks are completed.
    /// </summary>
    void LoadSceneVariation()
    {
        SceneManager.LoadScene(sceneVariationName);
    }

    /// <summary>
    /// Updates the trash counter UI text.
    /// </summary>
    void UpdateTrashUI()
    {
        if (trashCounterText != null)
            trashCounterText.text = trashCollected + " / " + totalTrash;
    }
}