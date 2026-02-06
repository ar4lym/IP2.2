using UnityEngine;

public class Throwtrash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PickupTrash trash = other.GetComponent<PickupTrash>();

        if (trash != null)
        {
            trash.Collect();
        }
    }
}