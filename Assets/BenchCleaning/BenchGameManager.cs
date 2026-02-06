using UnityEngine;
using TMPro;

public class BenchGameManager : MonoBehaviour
{
    public static BenchGameManager Instance;

    public int benchesCleaned = 0;
    public int benchesToClean = 10;
    public TextMeshProUGUI countText;
    public GameObject completionUI;

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
    }

    public void BenchCleaned()
    {
        benchesCleaned++;
        UpdateUI();

        if (benchesCleaned >= benchesToClean)
        {
            ShowCompletionUI();
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
        {
            completionUI.SetActive(true);
        }
    }
}
