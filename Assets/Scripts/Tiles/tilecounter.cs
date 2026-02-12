using UnityEngine;

using TMPro;

public class tilecounter : MonoBehaviour
{
    public TextMeshProUGUI tileCountText;
    public int completedTiles = 0;

    // Placeholder for database integration
    // e.g., Firebase or PlayerPrefs

    void Start()
    {
        UpdateTileCountText();
        // TODO: Load completedTiles from database
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            completedTiles++;
            UpdateTileCountText();
            // TODO: Save completedTiles to database
        }
    }

    void UpdateTileCountText()
    {
        if (tileCountText != null)
            tileCountText.text = $"Tiles Completed: {completedTiles}";
    }
}
