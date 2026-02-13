using UnityEngine;

public class TileLandingSFX : MonoBehaviour
{
    public AudioClip landingClip;
    [Range(0f, 1f)] public float volume = 1f;

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        Transform rig = other.transform.root;

        if (!rig.CompareTag("Player")) return;

        if (landingClip == null)
        {
            Debug.LogWarning("No landingClip assigned!");
            return;
        }

        if (hasPlayed) return;

        AudioSource.PlayClipAtPoint(landingClip, transform.position, volume);
        hasPlayed = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.root.CompareTag("Player")) return;

        hasPlayed = false;
    }
}
