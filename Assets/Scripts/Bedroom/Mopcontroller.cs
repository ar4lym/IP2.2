/// <summary>
/// MopController.cs
/// This script controls the mop in the second scene, 
/// it tracks whether the mop is being held by the player or not.
/// The mop is used to clean water puddles in the bedroom scene.
/// </summary>
/// <author> Lee Jia Lu </author>
/// <date> 25/01/2026 </date>
/// <StudentID> S10269187E </StudentID>
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Controls mop interaction state for VR, tracking whether the mop is currently held by the player.
/// Uses XR Interaction Toolkit to detect grab and release events.
/// </summary>
public class MopController : MonoBehaviour
{
    /// <summary>
    /// Reference to the XR Grab Interactable component that handles VR interactions.
    /// </summary>
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    /// <summary>
    /// Gets whether the mop is currently being held by the player.
    /// </summary>
    public bool IsHeld { get; private set; }

    /// <summary>
    /// Initializes the XRGrabInteractable component reference.
    /// </summary>
    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    /// <summary>
    /// Subscribes to grab and release events when the component is enabled.
    /// </summary>
    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    /// <summary>
    /// Unsubscribes from grab and release events when the component is disabled.
    /// </summary>
    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }
    /// <summary>
    /// Called when the mop is grabbed by the player.
    /// sets IsHeld to true.
    /// </summary>
    private void OnGrab(SelectEnterEventArgs args)
    {
        IsHeld = true;
    }

    /// <summary>
    /// Called when the mop is released by the player.
    /// Sets IsHeld to false.
    /// </summary>
    private void OnRelease(SelectExitEventArgs args)
    {
        IsHeld = false;
    }
}
