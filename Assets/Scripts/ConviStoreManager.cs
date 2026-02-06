using TMPro;
using UnityEngine;
/// <summary>
/// Author: Leong Ming Hui
/// Date: 6/2/26
/// Description:
/// Tracks how many items have been correctly arranged in the Convenience Store and updates the UI text .
/// </summary>
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
