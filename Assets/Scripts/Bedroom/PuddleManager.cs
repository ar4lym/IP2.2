/// <summary>
/// PuddleManager.cs
/// This script is a script for 1 of the 3 ai
/// this controls the puddles in the second scene
/// it tracks how many puddles are cleaned.
/// </summary>
/// <author> Lee Jia Lu </author>
/// <date> 15/08/2025 </date>
/// <StudentID> S10269187E </StudentID>
using UnityEngine;
using TMPro;

/// <summary>
/// Singleton manager that tracks puddle cleaning progress in the bedroom scene.
/// Coordinates with TrashManager and BedroomGameManager to check task completion.
/// </summary>
public class PuddleManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the PuddleManager.
    /// </summary>
    public static PuddleManager Instance;

    /// <summary>
    /// Reference to the BedroomGameManager for checking overall completion state.
    /// </summary>
    public GameObject BedroomGameManager;
    [Header("Puddles")]
    /// <summary>
    /// Total number of puddles that need to be cleaned in the scene.
    /// </summary>
    public int totalPuddles = 5;

    /// <summary>
    /// Current count of puddles that have been cleaned.
    /// </summary>
    public int puddlesCleaned = 0;

    /// <summary>
    /// Flag indicating whether all puddles have been cleaned.
    /// </summary>
    public bool puddlesCompleted = false;   // completion flag

    [Header("UI")]

    /// <summary>
    /// UI text element displaying the puddle cleaning progress counter.
    /// </summary>
    public TextMeshProUGUI puddleCounterText;

    /// <summary>
    /// Initializes the singleton instance.
    /// Ensures only one PuddleManager exists in the scene.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Initializes the puddle counter UI on scene start.
    /// </summary>
    private void Start()
    {
        UpdatePuddleUI();
    }

    /// <summary>
    /// Gets the current number of cleaned puddles.
    /// </summary>
    public int GetCleanedPuddles()
    {
        return puddlesCleaned;
    }

    /// <summary>
    /// Called when a puddle is successfully cleaned.
    /// Increments the counter, updates UI, and checks for task completion.
    /// Notifies TrashManager and BedroomGameManager when all puddles are cleaned.
    /// </summary>
    public void PuddleCleaned()
    {
        puddlesCleaned++;
        UpdatePuddleUI();

        //  Mark puddles done
        if (puddlesCleaned >= totalPuddles)
        {
            puddlesCompleted = true;
            Debug.Log("Puddles completed!");

            BedroomGameManager.GetComponent<BedroomGameManager>().CheckCompletion();
            // Ask TrashManager to check AND condition
            if (Trashmanager.Instance != null)
                Trashmanager.Instance.TryStopTimer();
        }
    }

    /// <summary>
    /// Updates the puddle counter UI text to display current progress.
    /// </summary>
    private void UpdatePuddleUI()
    {
        if (puddleCounterText != null)
            puddleCounterText.text = puddlesCleaned + " / " + totalPuddles;
    }
}