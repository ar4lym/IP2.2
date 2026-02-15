/// <summary>
/// Throwtrash.cs
/// This script is a script for 1 of the 3 ai
/// this controls the trash in the second scene
/// it is thrown by the player.
/// </summary>
/// <author> Lee Jia Lu </author>
/// <date> 25/01/2026 </date>
/// <StudentID> S10269187E </StudentID>
using UnityEngine;

/// <summary>
/// Detects when trash items are thrown into a trash bin or collection area.
/// Plays audio feedback and triggers the collection process.
/// </summary>
public class Throwtrash : MonoBehaviour
{
    /// <summary>
    /// Audio source that plays a sound effect when trash is successfully thrown in.
    /// </summary>
    public AudioSource trashAudioSource;

    /// <summary>
    /// Called when a collider enters the trash bin's trigger zone.
    /// Checks if the object is trash and collects it if valid.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        PickupTrash trash = other.GetComponent<PickupTrash>();

        if (trash != null)
        {
              if (trashAudioSource != null)
            trashAudioSource.Play();

            trash.Collect();
        }
    }
}