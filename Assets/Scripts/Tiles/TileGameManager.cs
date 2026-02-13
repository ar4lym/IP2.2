using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Needed for the teleport button

public class TileGameManager : MonoBehaviour
{
    [Header("Game Rules")]
    public int totalCorrectTiles = 9;

    [Header("UI")]
    public TMP_Text counterText;

    [Header("Congrats UI")]
    public GameObject congratsPopup;
    public TMP_Text congratsText;

    [Header("Reminder UI")]
    public GameObject reminderPopup;
    public TMP_Text reminderText;
    public float reminderDuration = 3f;

    private int completed = 0;

    private void Start()
    {
        if (congratsPopup != null) congratsPopup.SetActive(false);
        if (reminderPopup != null) reminderPopup.SetActive(false);
        UpdateUI();
    }

    // This is the function called by the Tiles
    public void AddCompletedTile()
    {
        completed++; // 1. Increase the number
        UpdateUI();  // 2. Update the 0/9 text

        if (completed >= totalCorrectTiles)
        {
            if (congratsPopup != null)
            {
                congratsPopup.SetActive(true);
                if (congratsText != null) 
                    congratsText.text = "Congratulations!\nAll tiles completed.";
            }
        }
    }

    private void UpdateUI()
    {
        if (counterText != null)
            counterText.text = $"{completed}/{totalCorrectTiles}";
    }


    private IEnumerator ShowReminder(string message)
    {
        if (reminderPopup == null || reminderText == null) yield break;

        reminderText.text = message;
        reminderPopup.SetActive(true);

        yield return new WaitForSeconds(reminderDuration);

        reminderPopup.SetActive(false);
    }

    // Link this to your Button OnClick event in the inspector
    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}