using UnityEngine;

public class BedroomGameManager : MonoBehaviour
{
    [Header("Managers")]
    public PuddleManager puddleManager;
    public Trashmanager trashManager;

    [Header("Completion Object")]
    public GameObject completionObject; // Assign in Inspector

    private bool hasUnhidden = false;

    private void Start()
    {
        if (completionObject != null)
            completionObject.SetActive(false);
    }

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