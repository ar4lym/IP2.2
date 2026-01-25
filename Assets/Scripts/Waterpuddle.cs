using UnityEngine;

/*
public class WaterPuddle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        MopController mop = other.GetComponentInParent<MopController>();
        Debug.Log (mop);
        if (mop != null && mop.IsHeld)
        {
            CleanWater();
            Debug.Log ("Mop picked up", mop);
        }
    }
    


    private void CleanWater()
    {
        // Optional: add particles or sound here
        Destroy(gameObject);
    }
}
*/

public class WaterPuddle : MonoBehaviour
{
    public float cleanTimeRequired = 3f;

    private float currentCleanTime = 0f;
    private bool isCleaned = false;

    private void OnTriggerStay(Collider other)
    {
        if (isCleaned) return;

        MopController mop = other.GetComponentInParent<MopController>();

        if (mop != null && mop.IsHeld)
        {
            currentCleanTime += Time.deltaTime;
            Debug.Log("Mopping... " + currentCleanTime.ToString("F2"));

            if (currentCleanTime >= cleanTimeRequired)
            {
                CleanWater();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset progress if mop leaves the water
        currentCleanTime = 0f;
        Debug.Log("Mopping interrupted, timer reset");
    }

    private void CleanWater()
    {
        isCleaned = true;
        Debug.Log("Water cleaned!");
        Destroy(gameObject);
    }
}
