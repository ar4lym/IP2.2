using UnityEngine;


public class SprayCleaner : MonoBehaviour
{
    [Header("Spray Settings")]
    public float sprayDistance = 2f;   // How far the spray reaches
    public float cleanRate = 30f;      // How fast the dirt is cleaned

    [Header("References")]
    public Transform sprayPoint;       // Tip of the bottle (child of bottle)
    public ParticleSystem sprayFX;     // Particle system for spray

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;   // Grab component

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grab == null)
            Debug.LogError("XRGrabInteractable missing on Bottle!");
        
        // Auto-assign children if not manually assigned
        if (sprayPoint == null)
        {
            sprayPoint = GetComponentInChildren<Transform>();
            Debug.LogWarning("SprayPoint not assigned, using first child transform.");
        }

        if (sprayFX == null)
            sprayFX = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (grab != null && grab.isSelected)
        {
            // SprayPoint stays at the nozzle, but ray direction follows the bottle's forward
            Spray();
        }
        else
        {
            if (sprayFX != null && sprayFX.isPlaying)
                sprayFX.Stop();
        }
    }

    void Spray()
    {
        if (sprayFX != null && !sprayFX.isPlaying)
            sprayFX.Play();

        // Draw debug ray from sprayPoint along bottle's forward
        Debug.DrawRay(sprayPoint.position, transform.forward * sprayDistance, Color.green);

        // Raycast using bottle's forward instead of sprayPoint.forward
        Ray ray = new Ray(sprayPoint.position, transform.forward);
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
}
