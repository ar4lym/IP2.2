using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    public bool playOnceUntilExit = true;

    private bool played = false;

    private void OnTriggerEnter(Collider other)
    {
        Transform rig = other.transform.root;
        if (!rig.CompareTag("Player")) return;

        Debug.Log("Sound trigger fired on: " + transform.root.name);

        if (clip == null)
        {
            Debug.LogError("No clip assigned on PlaySoundOnTrigger.");
            return;
        }

        if (playOnceUntilExit && played) return;

        // Guaranteed playback without needing an AudioSource component
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        played = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Transform rig = other.transform.root;
        if (!rig.CompareTag("Player")) return;

        played = false;
    }
}


