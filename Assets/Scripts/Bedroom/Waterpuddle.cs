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

public class WaterPuddle : MonoBehaviour
{
    public float cleanTimeRequired = 3f;

    [Header("Audio")]
    public AudioClip moppingSound;
    private AudioSource audioSource;

    private float currentCleanTime = 0f;
    private bool isCleaned = false;
    private bool isCurrentlyMopping = false; // ✅ Added this variable
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

            if (currentCleanTime >= cleanTimeRequired)
            {
                CleanWater();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MopController mop = other.GetComponentInParent<MopController>();

        if (mop != null)
        {
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
            Debug.Log("Mopping interrupted, timer reset");
        }
    }

    private void CleanWater()
    {
        if (isCleaned) return;

        isCleaned = true;

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        Debug.Log("Water cleaned!");

        // ✅ Call PuddleManager to update counter
        if (PuddleManager.Instance != null)
            PuddleManager.Instance.PuddleCleaned();
        else
            Debug.LogWarning("PuddleManager instance not found!");

        Destroy(gameObject); // remove puddle
    }
}