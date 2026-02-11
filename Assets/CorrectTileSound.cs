using UnityEngine;

public class CorrectTileSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rig = other.transform.root;
        if (!rig.CompareTag("Player")) return;

        if (audioSource != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f); // optional polish
            audioSource.Play();

Debug.Log("Correct tile triggered!");
        }
    }
}

