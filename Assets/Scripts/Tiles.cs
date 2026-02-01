using UnityEngine;

public class Tiles : MonoBehaviour
{
    public Transform spawnPoint;

    //public AudioSource lava;
    
    /// <summary>
    /// When player touch it, they respawn to spawn point
    /// </summary>
    
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