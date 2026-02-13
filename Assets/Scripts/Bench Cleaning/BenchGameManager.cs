/// <summary>
/// BenchGameManager.cs
/// BenchGameManager handles the overall logic for the Bench Cleaning mechanic.
/// It tracks the number of benches cleaned, updates the UI counter,
/// displays OCD popups after 3 benches, and manages game completion.
/// When all benches are cleaned, it:
/// - Shows the completion UI
/// - Plays the completion sound
/// - Stops the background music
/// - Stops the timer
/// </summary>
/// <author> Schanelle Leah Jackson </author>
/// <date> 13/02/2026 </date>
/// <StudentID> S10269101G </StudentID>

using UnityEngine;
using TMPro;

public class BenchGameManager : MonoBehaviour
{
    public static BenchGameManager Instance;

    public int benchesCleaned = 0;
    public int benchesToClean = 10;
    public TextMeshProUGUI countText;
    public GameObject completionUI;
    public GameObject ocdPopupPanel; 
    public float popupDuration = 2f;
    public Timer timer;

    public AudioSource completionSound;  
    public AudioSource bgmSource;        

    private bool gameCompleted = false;  // to prevent double trigger

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();

        if (completionUI != null)
            completionUI.SetActive(false);

        if (ocdPopupPanel != null)
            ocdPopupPanel.SetActive(false);
    }

    /// <summary>
    /// Handles the logic executed whenever a bench is cleaned.
    /// Updates progress tracking and UI display.
    /// Triggers an OCD popup every 3 benches cleaned.
    /// When 10 benches are cleaned,
    /// finalizes the game state and completes event.
    /// </summary>
    public void BenchCleaned()
    {
        if (gameCompleted) return;

        benchesCleaned++;
        UpdateUI();

        // Show OCD popup for every 3 benches cleaned
        if (benchesCleaned % 3 == 0)
        {
            ShowOCDPopup();
        }

        // When all benches are cleaned
        if (benchesCleaned >= benchesToClean)
        {
            gameCompleted = true;

            ShowCompletionUI();

            // Play completion sound
            if (completionSound != null)
                completionSound.Play();

            // To stop background music
            if (bgmSource != null)
                bgmSource.Stop();

            if (timer != null)
                timer.StopTimer();
        }
    }

    /// <summary>
    /// Handles UI updates and feedback for this mechanic
    /// Updates the bench count display, shows the completion panel
    /// when all benches are cleaned,
    /// and manages the temporary OCD popup.
    /// </summary>
    void UpdateUI()
    {
        if (countText != null)
            countText.text = benchesCleaned.ToString();
    }

    void ShowCompletionUI()
    {
        if (completionUI != null)
            completionUI.SetActive(true);
    }

    void ShowOCDPopup()
    {
        if (ocdPopupPanel == null) return;

        ocdPopupPanel.SetActive(true);
        Invoke(nameof(HideOCDPopup), popupDuration);
    }

    void HideOCDPopup()
    {
        if (ocdPopupPanel != null)
            ocdPopupPanel.SetActive(false);
    }
}
