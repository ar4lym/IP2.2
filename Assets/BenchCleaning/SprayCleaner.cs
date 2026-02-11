using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SprayCleaner : MonoBehaviour
{
    public float sprayDistance = 2f;
    public float cleanRate = 30f;

    public Transform sprayPoint;
    public ParticleSystem sprayFX;

    public AudioSource spraySound;  

    private XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();

        if (grab == null)
            Debug.LogError("XRGrabInteractable missing on Bottle!");

        if (sprayPoint == null)
            Debug.LogError("SprayPoint is NOT assigned.");

        if (sprayFX == null)
            Debug.LogWarning("SprayFX not assigned.");

        if (spraySound == null)
            Debug.LogWarning("SpraySound not assigned.");
    }

    void Update()
    {
        if (grab != null && grab.isSelected)
        {
            Spray();
        }
        else
        {
            StopSpray();
        }
    }

    void Spray()
    {
        if (sprayPoint == null) return;

        // Play spray particles
        if (sprayFX != null && !sprayFX.isPlaying)
            sprayFX.Play();

        // Play spraying sound
        if (spraySound != null && !spraySound.isPlaying)
            spraySound.Play();

        Debug.DrawRay(
            sprayPoint.position,
            sprayPoint.forward * sprayDistance,
            Color.green
        );

        Ray ray = new Ray(sprayPoint.position, sprayPoint.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, sprayDistance))
        {
            if (hit.collider.CompareTag("Dirty_Bench"))
            {
                Dirt dirt = hit.collider.GetComponent<Dirt>();
                if (dirt != null)
                {
                    dirt.Clean(Time.deltaTime * cleanRate);
                }
            }
        }
    }

    void StopSpray()
    {
        if (sprayFX != null && sprayFX.isPlaying)
            sprayFX.Stop();

        // to Stop spraying sound
        if (spraySound != null && spraySound.isPlaying)
            spraySound.Stop();
    }
}
