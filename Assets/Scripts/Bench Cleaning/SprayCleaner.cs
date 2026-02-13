/// <summary>
/// SprayCleaner.cs
/// Controls the spray bottle interaction in the bench cleaning mechanic.
/// When bottle is grabbed, it casts a ray forward to detect dirt objects.
/// If dirt object is hit, its dirt value decreases.
/// Also manages spray particle effects and spraying sound.
/// </summary>
/// <author> Schanelle Leah Jackson </author>
/// <date> 13/02/2026 </date>
/// <StudentID> S10269101G </StudentID>

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


    /// <summary>
    /// Called once per frame.
    /// Checks if the spray bottle is currently grabbed.
    /// If grabbed, spraying is triggered.
    /// If not grabbed, spraying effects stops.
    /// </summary>
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

    /// <summary>
    /// Activates the spraying behaviour.
    /// Plays particle and audio effects, casts a ray forward
    /// from the spray point and reduces the dirt amount
    /// of bench dirt object ob bench detected within range.
    /// </summary>
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

    /// <summary>
    /// Stops the spraying effects.
    /// Disables the spray particle system and spraying sound
    /// when bottle is not being grabbed.
    /// </summary>
    void StopSpray()
    {
        // to stop VFX
        if (sprayFX != null && sprayFX.isPlaying)
            sprayFX.Stop();

        // to stop spraying sound
        if (spraySound != null && spraySound.isPlaying)
            spraySound.Stop();
    }
}
