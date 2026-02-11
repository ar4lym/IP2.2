/// <summary>
/// BGMManager.cs
/// This script manages the background music in the convenience store.
/// It handles switching between calm and anxious background music based on time.
/// </summary>
/// <author> Leong Ming Hui </author>
/// <date> 11/02/2026 </date>
/// <StudentID> S10267664J </StudentID>

using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource csBGM;
    public AudioSource anxiousBGM;

    public float anxietyStartTime = 60f;
    public float fadeSpeed = 0.4f;

    private float localTimer = 0f;
    private bool anxietyActive = false;
    public GameObject anxietyPopup;
    private bool popupShown = false;

    private bool taskCompleted = false;

    void Start()
    {
        csBGM.Play();
        anxiousBGM.volume = 0f;
    }


    /// Updates the background music based on the elapsed time.
    /// After a certain time, it fades out the Convenience Store BGM and fades in the Anxious BGM,
    /// while also showing a popup to indicate the change in atmosphere.
    void Update()
    {
        if (taskCompleted) return;

        localTimer += Time.deltaTime;

        if (localTimer >= anxietyStartTime)
        {
            anxietyActive = true;
        }

        if (anxietyActive)
        {
            csBGM.volume = Mathf.MoveTowards(csBGM.volume, 0f, fadeSpeed * Time.deltaTime);

            if (!anxiousBGM.isPlaying)
                anxiousBGM.Play();

            anxiousBGM.volume = Mathf.MoveTowards(anxiousBGM.volume, 0.8f, fadeSpeed * Time.deltaTime);
        }

        // Show the popup when the anxiety music starts playing.
        if (localTimer >= anxietyStartTime && !popupShown)
        {
            anxietyActive = true;
            popupShown = true;
            ShowPopup();
        }
    }

    
    /// Shows the anxiety popup for 5 seconds when the timer reaches the anxiety start time.      
    private void ShowPopup()
    {
        anxietyPopup.SetActive(true);
        Invoke(nameof(HidePopup), 5f);
    }

    private void HidePopup()
    {
        anxietyPopup.SetActive(false);
    }


    /// Called when the task is completed to stop the timer and switch to original convenience store BGM.
    public void OnTaskCompleted()
    {
        taskCompleted = true;

        csBGM.volume = 0.6f;
        anxiousBGM.Stop();
        anxiousBGM.volume = 0f;
    }
}
