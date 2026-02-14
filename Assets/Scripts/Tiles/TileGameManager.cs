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

    private void Start()
    {
        // Ensure the popup is hidden when the game starts
        if (congratsPopup != null) congratsPopup.SetActive(false);
        UpdateUI();
    }

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

    private void UpdateUI()
    {
        if (counterText != null)
            counterText.text = $"{completed}/{totalCorrectTiles}";
    }
}