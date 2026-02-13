/// <summary>
/// MopController.cs
/// This script is a script for 1 of the 3 ai
/// this controls the mop in the second scene
/// it is used to clean water puddles.
/// </summary>
/// <author> Lee Jia Lu </author>
/// <date> 25/01/2026 </date>
/// <StudentID> S10269187E </StudentID>
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MopController : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    public bool IsHeld { get; private set; }

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        IsHeld = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        IsHeld = false;
    }
}
