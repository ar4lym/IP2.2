/// <summary>
/// Trashmanager.cs
/// This script manages the trash collection process in the Bedroom scene.
/// It tracks the number of trash items collected, updates the UI counter,
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

    [Header("Clean Popup")]
    public GameObject cleanPopup;
    public float popupDelay = 30f;
    public float popupDuration = 5f;

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

        if (cleanPopup != null)
            cleanPopup.SetActive(false);

            Invoke(nameof(ShowCleanPopup), popupDelay);
    }

    /// <summary>
    /// Shows the clean popup after a delay if trash is not yet completed.
    /// </summary>
    void ShowCleanPopup()
    {
        if (!trashCompleted && cleanPopup != null)  // Check if popup is assigned and trash not completed
        {
            cleanPopup.SetActive(true);

            // Automatically hide after duration
            Invoke(nameof(HideCleanPopup), popupDuration);
        }
    }

    /// <summary>
    /// Hides the clean popup after showing it for a set duration.
    /// </summary>
    void HideCleanPopup()
    {
        if (cleanPopup != null)
            cleanPopup.SetActive(false);
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