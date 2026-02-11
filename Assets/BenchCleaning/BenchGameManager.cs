// using UnityEngine;
// using TMPro;

// public class BenchGameManager : MonoBehaviour
// {
//     public static BenchGameManager Instance;

//     public int benchesCleaned = 0;
//     public int benchesToClean = 10;
//     public TextMeshProUGUI countText;
//     public GameObject completionUI;

//     void Awake()
//     {
//         if (Instance == null)
//             Instance = this;
//         else
//             Destroy(gameObject);
//     }

//     void Start()
//     {
//         UpdateUI();

//         if (completionUI != null)
//             completionUI.SetActive(false);
//     }

//     public void BenchCleaned()
//     {
//         benchesCleaned++;
//         UpdateUI();

//         if (benchesCleaned >= benchesToClean)
//         {
//             ShowCompletionUI();
//         }
//     }

//     void UpdateUI()
//     {
//         if (countText != null)
//             countText.text = benchesCleaned.ToString();
//     }

//     void ShowCompletionUI()
//     {
//         if (completionUI != null)
//         {
//             completionUI.SetActive(true);
//         }
//     }
// }


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
