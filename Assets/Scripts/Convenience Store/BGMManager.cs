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
    public AudioSource csBGM;  // Convenience Store BGM
    public AudioSource anxiousBGM; // Anxious BGM

    public float anxietyStartTime = 60f;  // Time in seconds to start anxious BGM
    public float fadeSpeed = 0.4f;  // Speed of volume fade

    private float localTimer = 0f;  // Local timer to track elapsed time
    private bool anxietyActive = false;  // Flag to indicate if anxiety BGM is active
    public GameObject anxietyPopup;  // Popup to show when anxiety BGM starts
    private bool popupShown = false;  // To ensure popup is shown only once

    private bool taskCompleted = false;  // Flag to indicate if the task is completed


    /// Initialises the BGM settings
    /// Sets the convenience store BGM to play and anxious BGM volume to 0
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
        if (taskCompleted) return;  // Exit if task is completed

        localTimer += Time.deltaTime;  // Increment local timer

        if (localTimer >= anxietyStartTime)  // Start anxiety BGM after specified time
        {
            anxietyActive = true;
        }

        if (anxietyActive)
        {
            csBGM.volume = Mathf.MoveTowards(csBGM.volume, 0f, fadeSpeed * Time.deltaTime);  // Fade out convenience store BGM

            if (!anxiousBGM.isPlaying)  // Start anxious BGM if not already playing
                anxiousBGM.Play();

            anxiousBGM.volume = Mathf.MoveTowards(anxiousBGM.volume, 0.8f, fadeSpeed * Time.deltaTime);  // Fade in anxious BGM
        }

        /// Show popup once when anxiety BGM starts
        if (localTimer >= anxietyStartTime && !popupShown)  // Show popup only once
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
        Invoke(nameof(HidePopup), 5f);  // Hide after 5 seconds
    }

    /// Hides the anxiety popup after the duration has passed
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
