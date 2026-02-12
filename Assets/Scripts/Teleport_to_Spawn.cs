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
        Transform rig = other.transform.root;
        if (!rig.CompareTag("Player")) return;

        // If player steps onto spawn tile, re-arm teleport
        if (rig.position == spawnPoint.position)
        {
            armed = true;
            Debug.Log("Teleport re-armed after stepping onto spawn tile.");
            return; // Don't teleport if just re-arming
        }

        if (!armed || teleporting) return;

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
        // ...existing code...
    }
}