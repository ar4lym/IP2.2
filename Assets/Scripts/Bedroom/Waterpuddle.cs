using UnityEngine;

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
        MopController mop = other.GetComponentInParent<MopController>();

        if (mop != null)
        {
            currentCleanTime = 0f;
            Debug.Log("Mopping interrupted, timer reset");
        }
    }

    private void CleanWater()
    {
        if (isCleaned) return;

        isCleaned = true;
        Debug.Log("Water cleaned!");

        // ✅ Call PuddleManager to update counter
        if (PuddleManager.Instance != null)
            PuddleManager.Instance.PuddleCleaned();
        else
            Debug.LogWarning("PuddleManager instance not found!");

        Destroy(gameObject); // remove puddle
    }
}