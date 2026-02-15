/// <summary>
/// WaterPuddle.cs
/// This script is a script for 1 of the 3 ai
/// this controls the water puddle in the second scene
/// it needs to be cleaned with a mop.
/// </summary>
/// <author> Lee Jia Lu </author>
/// <date> 25/01/2026 </date>
/// <StudentID> S10269187E </StudentID>
using UnityEngine;
using TMPro;

/// <summary>
/// Manages a water puddle that can be cleaned by mopping.
/// Tracks cleaning progress, plays audio feedback, and destroys itself when fully cleaned.
/// </summary>
public class WaterPuddle : MonoBehaviour
{

    /// <summary>
    /// Time in seconds required to fully clean this puddle.
    /// </summary>
    public float cleanTimeRequired = 3f;

    /// <summary>
    /// UI text element displaying cleaning progress percentage.
    /// </summary>
    public TMP_Text progressText; 

    [Header("Audio")]
    /// <summary>
    /// Audio clip to play while mopping this puddle.
    /// </summary>
    public AudioClip moppingSound;


    /// <summary>
    /// AudioSource component for playing mopping sound effects.
    /// </summary>
    private AudioSource audioSource;

    

    /// <summary>
    /// Current accumulated cleaning time in seconds.
    /// </summary>
    private float currentCleanTime = 0f;

    /// <summary>
    /// Whether this puddle has been completely cleaned.
    /// </summary>
    private bool isCleaned = false;
    
    /// <summary>
    /// Tracks if mopping is currently in progress to manage sound playback.
    /// </summary>
    private bool isCurrentlyMopping = false; // Track if mopping is in progress to manage sound playback

    /// <summary>
    /// Initializes the AudioSource component with 3D spatial sound settings.
    /// </summary>
    private void Awake()
    {
        // Create and configure AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = moppingSound;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 3D sound
        audioSource.volume = 0.5f;
    }

    /// <summary>
    /// Detects when a mop is actively cleaning this puddle.
    /// Accumulates cleaning time and updates progress while mop is held and within trigger.
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (isCleaned) return;

        MopController mop = other.GetComponentInParent<MopController>();

        if (mop != null && mop.IsHeld)
        {
            if (!isCurrentlyMopping)
            {
                isCurrentlyMopping = true;
                if (moppingSound != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }

            currentCleanTime += Time.deltaTime;
            Debug.Log("Mopping... " + currentCleanTime.ToString("F2"));

            ProgressTextUpdate();

            if (currentCleanTime >= cleanTimeRequired)
            {
                CleanWater();
            }
        }
    }


    /// <summary>
    /// Detects when the mop exits the puddle's trigger area.
    /// Stops mopping sound and resets cleaning progress.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        MopController mop = other.GetComponentInParent<MopController>();

        if (mop != null)
        {
                        // Stop mopping sound
            // Stop mopping sound
            if (isCurrentlyMopping)
            {
                isCurrentlyMopping = false;
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }

            currentCleanTime = 0f;
            if (progressText != null)
                progressText.text = ""; // <-- Explicitly clear text on interruption
            Debug.Log("Mopping interrupted, timer reset");
        }
    }

    /// <summary>
    /// Completes the cleaning process: stops audio, notifies PuddleManager, and destroys the puddle GameObject.
    /// </summary>
    private void CleanWater()
    {
        if (isCleaned) return;

        isCleaned = true;

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        Debug.Log("Water cleaned!");

        //  Call PuddleManager to update counter
        if (PuddleManager.Instance != null)
            PuddleManager.Instance.PuddleCleaned();
        else
            Debug.LogWarning("PuddleManager instance not found!");

        if (progressText != null)
            progressText.text = ""; // <-- Clear text when cleaned

        Destroy(gameObject); // remove puddle
    }

    /// <summary>
    /// Updates the progress text UI with current cleaning percentage.
    /// Clears text when cleaning is complete.
    /// </summary>
    private void ProgressTextUpdate()
    {
        if (progressText != null)
        {
            float progress = Mathf.Clamp01(currentCleanTime / cleanTimeRequired);
            if (progress >= 1f || isCleaned)
            {
                progressText.text = ""; // Reset/clear text after finished
            }
            else
            {
                progressText.text = $"Cleaning: {(progress * 100f).ToString("F0")}%";
            }
        }
    }
}