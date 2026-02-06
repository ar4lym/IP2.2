using UnityEngine;
using TMPro;

public class PuddleManager : MonoBehaviour
{
    public static PuddleManager Instance;

    [Header("Puddles")]
    public int totalPuddles = 5;      // Total puddles in scene
    private int puddlesCleaned = 0;   // How many have been cleaned

    [Header("UI")]
    public TextMeshProUGUI puddleCounterText; // Assign TMP text in Inspector
    // public GameObject levelCompleteUI;        // Optional, shows when all puddles cleaned

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // private void Start()
    // {
    //     UpdatePuddleUI();               // Initialize puddle counter
    //     if (levelCompleteUI != null)
    //         levelCompleteUI.SetActive(false);
    // }

    // Call this when a puddle is cleaned
    public void PuddleCleaned()
    {
        puddlesCleaned++;
        UpdatePuddleUI();
        //CheckCompletion();
    }

    // Updates the TMP text
    private void UpdatePuddleUI()
    {
        if (puddleCounterText != null)
            puddleCounterText.text = puddlesCleaned + " / " + totalPuddles;
    }

    // Optional: check if all puddles are cleaned
    // private void CheckCompletion()
    // {
    //     if (puddlesCleaned >= totalPuddles)
    //     {
    //         Debug.Log("ALL PUDDLES CLEANED!");
    //         if (levelCompleteUI != null)
    //             levelCompleteUI.SetActive(true);
    //     }
    // }
}