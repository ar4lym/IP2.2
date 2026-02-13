/// <summary>
/// Handles scanning of objects in VR by holding them for a set duration.
/// </summary>
/// <author> Aralyn Han Zi Ning </author>
/// <date> 09/02/2026 </date>
/// <StudentID> S10267170A </StudentID>
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Handles scanning interaction for a grabbable object.
/// Player must hold the object for a set time to complete the scan,
/// which then shows information and activates a clue.
/// </summary>
public class ScannableObject : MonoBehaviour
{
    public float holdTime = 3f;
    public Image radialScanImage; // assign the circular Image
    public Canvas scanCanvas;     // optional, shows bar
    public GameObject infoPanel; // assign the info panel to show after scan

    public GameObject objectDestory; // assign the info panel to show after scan
    
    public GameObject clue; // assign the info clue1 to show after scan
     

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Coroutine holdCoroutine;
    private bool hasBeenScanned = false;

/// <summary>
/// Gets XRGrabInteractable component and hides scan UI on start.
/// </summary>
    void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (scanCanvas != null)
            scanCanvas.gameObject.SetActive(false);
    }

/// <summary>
/// Registers grab and release events when object becomes active.
/// </summary>
    void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

/// <summary>
///  Unregisters grab and release events when object is disabled.
/// </summary>
    void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

/// <summary>
/// Starts scan timer when player grabs the object.
/// </summary>
    void OnGrab(SelectEnterEventArgs args)
    {
        if (!hasBeenScanned)
            holdCoroutine = StartCoroutine(HoldTimer());
    }

/// <summary>
/// Stops scan and resets UI when player releases the object.
/// </summary>
    void OnRelease(SelectExitEventArgs args)
    {
        if (holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            holdCoroutine = null;
        }

        ResetScanUI();
    }

/// <summary>
/// Handles scanning progress while object is held.
/// Updates radial UI until scan completes.
/// </summary>
    IEnumerator HoldTimer()
    {
        if (scanCanvas != null)
            scanCanvas.gameObject.SetActive(true);

        float timer = 0f;

        while (timer < holdTime)
        {
            timer += Time.deltaTime;

            if (radialScanImage != null)
                radialScanImage.fillAmount = timer / holdTime;

            yield return null;
        }

        ScanObject();
    }
/// <summary>
/// Called when scan finishes.
/// Shows info panel, activates clue, and disables scanned object.
/// </summary>
    void ScanObject()
    {
        hasBeenScanned = true;
        ResetScanUI();
        infoPanel.SetActive(true);
        clue.SetActive(true);
        objectDestory.SetActive(false);
    }
/// <summary>
/// Hides scan UI and resets radial progress bar.
/// </summary>
    void ResetScanUI()
    {
        if (scanCanvas != null)
            scanCanvas.gameObject.SetActive(false);

        if (radialScanImage != null)
            radialScanImage.fillAmount = 0f;
    }
}
