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

public class ConviStoreManager : MonoBehaviour
{
    // Total number of items needed to be arranged
    public int totalItems = 10;

    // Current number of arranged items
    private int itemsArranged = 0;

    // UI text displaying progress
    public TextMeshProUGUI storeProgressText;

    public Timer timer;

    public GameObject completeUI; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        UpdateUI();
        if (completeUI != null)
        {
            completeUI.SetActive(false);
        }
    }

    /// <summary>
    /// Called when an item is correctly placed into a socket
    /// </summary>
    public void AddItem()
    {
        itemsArranged++;
        UpdateUI();


        if (itemsArranged == totalItems)
        {
            completeUI.SetActive(true);
            timer.StopTimer();
        }
    }

    
    /// <summary>
    /// Updates the progress UI text
    /// </summary>
    private void UpdateUI()
    {
        storeProgressText.text = $"Items arranged: {itemsArranged} / {totalItems}";
    }
}
