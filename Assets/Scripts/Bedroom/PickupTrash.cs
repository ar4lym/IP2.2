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

public class PickupTrash : MonoBehaviour
{
    private bool collected = false;

    public void Collect()
    {
        if (collected) return;

        collected = true;

        Trashmanager.Instance.TrashCollected();

        //Destroy(gameObject);
    }
}