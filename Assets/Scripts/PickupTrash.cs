using UnityEngine;

public class PickupTrash : MonoBehaviour
{
    private bool collected = false;

    public void Collect()
    {
        if (collected) return;

        collected = true;

        Trashmanager.Instance.TrashCollected();

        Destroy(gameObject);
    }
}