/// <summary>
/// PickupTrash.cs
/// This script is a script for 1 of the 3 ai
/// this controls the trash in the second scene
/// it is collected by the player.
/// </summary>
/// <author> Lee Jia Lu </author>
/// <date> 25/01/2026 </date>
/// <StudentID> S10269187E </StudentID>
using UnityEngine;

/// <summary>
/// Represents a collectible trash item that can be picked up once.
/// Notifies the TrashManager singleton when collected.
/// </summary>
public class PickupTrash : MonoBehaviour
{   

    /// <summary>
    /// Tracks whether this trash item has already been collected to prevent duplicate collection.
    /// </summary>
    private bool collected = false;

    /// <summary>
    /// Collects this trash item and notifies the TrashManager.
    /// Can only be called once per trash item.
    /// </summary>
    public void Collect()
    {
        if (collected) return;

        collected = true;

        Trashmanager.Instance.TrashCollected();

        //Destroy(gameObject);
    }
}