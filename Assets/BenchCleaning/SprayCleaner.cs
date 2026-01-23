using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SprayCleaner : MonoBehaviour
{
    public float sprayDistance = 2f;   // How far the spray reaches
    public float cleanRate = 30f;      // How fast the dirt is cleaned

    public Transform sprayPoint;

    public ParticleSystem sprayFX;

    private XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        if (grab == null)
        {
            Debug.LogError("XRGrabInteractable missing on Bottle!");
        }

        if (sprayPoint == null)
        {
            Debug.LogError("SprayPoint is NOT assigned. Assign it manually in the Inspector.");
        }

        if (sprayFX == null)
        {
            Debug.LogWarning("SprayFX not assigned. Spray will still work but no visuals.");
        }
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

        if (sprayFX != null && !sprayFX.isPlaying)
            sprayFX.Play();

        // Debug ray to confirm direction
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
    }
}
