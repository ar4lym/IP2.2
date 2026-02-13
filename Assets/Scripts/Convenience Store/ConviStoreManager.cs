/// <summary>
/// ConviStoreManager.cs
/// This script manages the item arrangement progress in the convenience store level.
/// It tracks the number of items correctly placed into sockets and updates the UI accordingly.
/// </summary>
/// <author> Leong Ming Hui </author>
/// <date> 06/02/2026 </date>
/// <StudentID> S10267664J </StudentID>

using TMPro;
using UnityEngine;
using System.Collections;

public class ConviStoreManager : MonoBehaviour
{
    [Header("Item Settings")]
    // Total number of items needed to be arranged
    public int totalItems = 10;

    // Current number of arranged items
    private int itemsArranged = 0;

    [Header("UI References")]
    // UI text displaying progress
    public TextMeshProUGUI storeProgressText;
    public GameObject completeUI;
    public GameObject wrongItemUI;
    public float wrongUIShowSeconds = 1.5f;

    [Header("Manager References")]
    public Timer timer;
    public BGMManager audioManager;

    private Coroutine wrongUICoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        // Initialize UI
        UpdateUI();
        
        if (completeUI != null)
        {
            completeUI.SetActive(false);
        }

        if (wrongItemUI != null)
        {
            wrongItemUI.SetActive(false);
        }

        // Verify Timer is assigned
        if (timer == null)
        {
            Debug.LogError("Timer reference is not assigned in ConviStoreManager!");
        }
        else
        {
            Debug.Log("ConviStoreManager: Timer is running");
        }

        // Verify BGMManager is assigned
        if (audioManager == null)
        {
            Debug.LogWarning("BGMManager reference is not assigned in ConviStoreManager!");
        }
    }

    /// <summary>
    /// Called when an item is correctly placed into a socket
    /// </summary>
    public void AddItem()
    {
        // Prevent adding items beyond the total
        if (itemsArranged >= totalItems)
        {
            Debug.LogWarning("Cannot add more items - already at max!");
            return;
        }
        
        itemsArranged++;
        Debug.Log($"Item arranged! Progress: {itemsArranged}/{totalItems}");
        UpdateUI();

        // Check if all items are arranged
        if (itemsArranged >= totalItems)
        {
            OnAllItemsCompleted();
        }
    }

    /// <summary>
    /// Called when all items have been successfully arranged
    /// </summary>
    private void OnAllItemsCompleted()
    {
        Debug.Log("All items arranged! Task completed!");

        // Show completion UI
        if (completeUI != null)
        {
            completeUI.SetActive(true);
        }

        // Stop the timer and save the time
        if (timer != null)
        {
            timer.StopTimer();
            float finalTime = timer.GetElapsedTime();
            Debug.Log($"Task completed in {finalTime} seconds");
        }
        else
        {
            Debug.LogError("Timer reference is missing - cannot stop timer!");
        }

        // Play completion audio
        if (audioManager != null)
        {
            audioManager.OnTaskCompleted();
        }
    }

    /// <summary>
    /// Updates the progress UI text
    /// </summary>
    private void UpdateUI()
    {
        if (storeProgressText != null)
        {
            storeProgressText.text = $"Items arranged: {itemsArranged} / {totalItems}";
        }
        else
        {
            Debug.LogWarning("Store progress text is not assigned!");
        }
    }

    /// <summary>
    /// Shows the wrong item UI temporarily
    /// </summary>
    public void ShowWrongItemUI()
    {
        if (wrongItemUI == null)
        {
            Debug.LogWarning("Wrong item UI is not assigned!");
            return;
        }

        // Restart timer if player keeps doing wrong placements
        if (wrongUICoroutine != null)
        {
            StopCoroutine(wrongUICoroutine);
        }

        wrongItemUI.SetActive(true);
        wrongUICoroutine = StartCoroutine(HideWrongUIAfterDelay());
    }

    /// <summary>
    /// Hides the wrong item UI after a delay
    /// </summary>
    private IEnumerator HideWrongUIAfterDelay()
    {
        yield return new WaitForSeconds(wrongUIShowSeconds);
        
        if (wrongItemUI != null)
        {
            wrongItemUI.SetActive(false);
        }
        
        wrongUICoroutine = null;
    }

    /// <summary>
    /// Public method to get current progress
    /// </summary>
    public int GetItemsArranged()
    {
        return itemsArranged;
    }

    /// <summary>
    /// Public method to check if task is completed
    /// </summary>
    public bool IsCompleted()
    {
        return itemsArranged >= totalItems;
    }

    /// <summary>
    /// Optional: Reset the manager (for testing or retrying)
    /// </summary>
    public void ResetProgress()
    {
        itemsArranged = 0;
        UpdateUI();
        
        if (completeUI != null)
        {
            completeUI.SetActive(false);
        }
        
        if (wrongItemUI != null)
        {
            wrongItemUI.SetActive(false);
        }
        
        Debug.Log("ConviStoreManager progress reset");
    }
}