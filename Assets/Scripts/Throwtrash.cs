using UnityEngine;

public class Throwtrash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Trash trash = other.GetComponent<Trash>();

        if (trash != null)
        {
            trash.Collect();
        }
    }
}