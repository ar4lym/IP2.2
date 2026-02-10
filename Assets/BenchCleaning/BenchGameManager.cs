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
            ShowCompletionUI();

            // To stop timer 
            if (timer != null)
            {
                timer.StopTimer();
            }
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
