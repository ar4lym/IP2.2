/// <summary>
/// SocketProgress.cs
/// This script handles the logic for individual item sockets in the convenience store level.
/// It checks if the correct item is placed into the socket and updates the overall progress accordingly.
/// When the correct item is placed, it notifies the ConviStoreManager to update the progress.
/// When an incorrect item is placed, it triggers a warning sound and notifies the manager to show a warning UI.
/// </summary>
/// <author> Leong Ming Hui </author>
/// <date> 06/02/2026 </date>
/// <StudentID> S10267664J </StudentID>


using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class SocketProgress : MonoBehaviour
{
    [Header("Correct Item")]
    public string correctItemTag;

    [Header("Progress Manager")]
    public ConviStoreManager storeManager;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip correctSFX;
    public AudioClip wrongSFX;

    // To prevent multiple counts for the same correct item
    private bool counted = false;



    // Called when an item is placed into the socket
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject placedItem = args.interactableObject.transform.gameObject; // The item placed into the socket

        /// CORRECT item
        if (placedItem.CompareTag(correctItemTag))
        {
            audioSource.PlayOneShot(correctSFX);  // Play correct placement sound

            if (!counted)
            {
                counted = true;  // Mark as counted to prevent double counting
                storeManager.AddItem();  // Notify the manager of correct placement
            }
        }
        /// WRONG item
        else
        {
            audioSource.PlayOneShot(wrongSFX);  // Play wrong placement sound
            storeManager.ShowWrongItemUI();

        }
    }


}