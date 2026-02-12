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