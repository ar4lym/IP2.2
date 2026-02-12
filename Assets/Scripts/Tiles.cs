using UnityEngine;

public class Tiles : MonoBehaviour
{
    public Transform spawnPoint;

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (spawnPoint != null)
            {
                other.transform.position = spawnPoint.position;
                Debug.Log("Teleporting to: " + spawnPoint.position);
                //lava.Play();

                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.Sleep();
                }

                Physics.SyncTransforms();
            }
            else
            {
                Debug.LogWarning("Spawn location not assigned!");
            }
        }


    }

}