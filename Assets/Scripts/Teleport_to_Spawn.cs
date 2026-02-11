using UnityEngine;
using System.Collections;

public class TeleportToSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public float teleportDelay = 0.3f;

    private bool armed = true;
    private bool teleporting = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!armed || teleporting) return;

        Transform rig = other.transform.root;
        if (!rig.CompareTag("Player")) return;

        StartCoroutine(TeleportPlayer(rig));
    }

    private IEnumerator TeleportPlayer(Transform rig)
    {
        armed = false;
        teleporting = true;

        yield return new WaitForSeconds(teleportDelay);

        var cc = rig.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        rig.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

        if (cc != null) cc.enabled = true;

        teleporting = false;
    }

    private void OnTriggerExit(Collider other)
    {
        Transform rig = other.transform.root;
        if (!rig.CompareTag("Player")) return;

        armed = true; // only re-arm when fully stepped off
        Debug.Log("Triggered from: " + transform.root.name);
    }
}