/// <summary>
/// Billboard.cs
/// Controls the UI billboard to always face the camera.
/// </summary>
/// <author> Aralyn Han Zi Ning </author>
/// <date> 23/01/2026 </date>
/// <StudentID> S10267170A </StudentID>
using UnityEngine;

/// <summary>
/// Makes a GameObject always face the VR camera (billboard effect).
/// </summary>
public class Billboard : MonoBehaviour {
    private Transform vrCamera;

    /// <summary>
    /// Finds and stores reference to the main VR camera.
    /// </summary>
    void Start() {
        // Find the XR Rig's camera
        vrCamera = Camera.main.transform; 
    }
    /// <summary>
    /// Rotates the object every frame so it faces the camera.
    /// </summary>
    void Update() {
        if (vrCamera != null) {
            // Make the text face the camera
            Vector3 direction = transform.position - vrCamera.position;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}
