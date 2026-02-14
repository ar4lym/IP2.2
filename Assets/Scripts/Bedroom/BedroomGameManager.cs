/// <summary>
/// BedroomGameManager.cs
/// Manages the completion logic for the Bedroom scene.
/// Tracks puddle and trash cleanup progress and reveals the completion object
/// once all tasks are finished.
/// </summary>
/// <author> Lee Jia Lu </author>
/// <date> 14/02/2026 </date>
/// <StudentID> S10269187E </StudentID>
using UnityEngine;

/// <summary>
/// Controls scene-specific completion checks for the Bedroom game.
/// </summary>
public class BedroomGameManager : MonoBehaviour
{
    [Header("Managers")]
    public PuddleManager puddleManager;
    public Trashmanager trashManager;

    [Header("Completion Object")]
    /// <summary>
    /// Object shown when all cleanup tasks are completed.
    /// </summary>
    public GameObject completionObject; // Assign in Inspector

    private bool hasUnhidden = false;

    /// <summary>
    /// Initializes the completion object state.
    /// </summary>
    private void Start()
    {
        if (completionObject != null)
            completionObject.SetActive(false);
    }

    /// <summary>
    /// Checks if all puddles and trash have been cleaned.
    /// Reveals the completion object when requirements are met.
    /// </summary>
    public void CheckCompletion()
    {
        Debug.Log("Checking completion status...");
        if (hasUnhidden) return;
        if (puddleManager == null || trashManager == null) return;

        if (puddleManager.puddlesCleaned >= puddleManager.totalPuddles &&
            trashManager.trashCollected >= trashManager.totalTrash)
        {
            hasUnhidden = true;
            if (completionObject != null)
                completionObject.SetActive(true);
        }
    }
}
