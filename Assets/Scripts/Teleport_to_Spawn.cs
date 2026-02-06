using UnityEngine;

public class TeleportToSpawn : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        Transform rigTransform = other.transform.root;

        if (!rigTransform.CompareTag("Player"))
            return;

        var characterController = rigTransform.GetComponent<CharacterController>();
        if (characterController != null)
            characterController.enabled = false;

        rigTransform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

        if (characterController != null)
            characterController.enabled = true;
    }
}

