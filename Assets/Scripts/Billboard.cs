using UnityEngine;

public class Billboard : MonoBehaviour {
    private Transform vrCamera;

    void Start() {
        // Find the XR Rig's camera
        vrCamera = Camera.main.transform; 
    }

    void Update() {
        if (vrCamera != null) {
            // Make the text face the camera
            Vector3 direction = transform.position - vrCamera.position;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}
