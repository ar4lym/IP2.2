/// <summary>
/// TileGameManager.cs
/// Manages the overall game state for the OCD tile stepping mechanic.
/// Works together with OCDStepTile.cs to track how many correct tiles have been completed and updates the UI accordingly.
/// When all correct tiles are completed, it shows a congratulatory message and stops the timer.
/// </summary>
/// <author> Raeanne Ho </author>
/// <date> 14/02/2026 </date>
/// <StudentID> S10265738J </StudentID>

using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TileGameManager : MonoBehaviour
{
    [Header("Game Rules")]
    public int totalCorrectTiles = 9;

    [Header("UI")]
    public TMP_Text counterText;

    [Header("Congrats UI")]
    public GameObject congratsPopup;
    public TMP_Text congratsText;

    private int completed = 0;

    public Timer timer;

    /// <summary>
    /// Called when the game starts.
    /// Ensures the congratulations popup is hidden
    /// and starts the UI counter.
    /// </summary>
    private void Start()
    {
        // Ensure the popup is hidden when the game starts
        if (congratsPopup != null) congratsPopup.SetActive(false);
        UpdateUI();
    }

    
    /// <summary>
    /// Increases the completed tile count +1.
    /// Called by OCDStepTile when a correct red tile
    /// successfully changes to green.
    /// Triggers the win condition when all tiles are completed (9/9) 
    /// </summary>
    // This is called by the Tiles once they turn green
    public void AddCompletedTile()
    {
        completed++;
        UpdateUI();

        if (completed >= totalCorrectTiles)
        {
            if (congratsPopup != null)
            {
                congratsPopup.SetActive(true);
                if (congratsText != null)
                    congratsText.text = "Congratulations!\nAll tiles completed.";
                    timer.StopTimer();
            }
        }
    }
   
    /// <summary>
    /// Updates the on-screen counter text
    /// to display current progress.
    /// </summary>
    private void UpdateUI()
    {
        if (counterText != null)
            counterText.text = $"{completed}/{totalCorrectTiles}";
    }
}