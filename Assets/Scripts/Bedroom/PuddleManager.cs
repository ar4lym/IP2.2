/// <summary>
/// Ghost.cs
/// This script is a script for 1 of the 3 ai
/// this controls the ghost in the second scene
/// it chases you and you die upon touching it.
/// </summary>
/// <author> Lee Jia Lu </author>
/// <date> 15/08/2025 </date>
/// <StudentID> S10269187E </StudentID>
using UnityEngine;
using TMPro;

public class PuddleManager : MonoBehaviour
{
    public static PuddleManager Instance;

    [Header("Puddles")]
    public int totalPuddles = 5;
    private int puddlesCleaned = 0;

    public bool puddlesCompleted = false;   // ✅ completion flag

    [Header("UI")]
    public TextMeshProUGUI puddleCounterText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdatePuddleUI();
    }

    public int GetCleanedPuddles()
    {
        return puddlesCleaned;
    }

    // Call this when a puddle is cleaned
    public void PuddleCleaned()
    {
        puddlesCleaned++;
        UpdatePuddleUI();

        // ✅ Mark puddles done
        if (puddlesCleaned >= totalPuddles)
        {
            puddlesCompleted = true;
            Debug.Log("Puddles completed!");

            // 🔗 Ask TrashManager to check AND condition
            if (Trashmanager.Instance != null)
                Trashmanager.Instance.TryStopTimer();
        }
    }

    // Updates UI
    private void UpdatePuddleUI()
    {
        if (puddleCounterText != null)
            puddleCounterText.text = puddlesCleaned + " / " + totalPuddles;
    }
}