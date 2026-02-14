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

public class Throwtrash : MonoBehaviour
{
    
    public AudioSource trashAudioSource;
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