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
    /// <summary>Total number of items needed</summary>
    public int totalItems = 10;

    /// <summary>Current number of arranged items</summary>
    private int itemsArranged = 0;

    /// <summary>UI text displaying progress</summary>
    public TextMeshProUGUI storeProgressText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        UpdateUI();
    }

    /// <summary>
    /// Called when an item is correctly placed into a socket
    /// </summary>
    public void AddItem()
    {
        itemsArranged++;
        UpdateUI();
    }

    /// <summary>
    /// Updates the progress UI text
    /// </summary>
    private void UpdateUI()
    {
        storeProgressText.text = $"Items arranged: {itemsArranged} / {totalItems}";
    }
}
