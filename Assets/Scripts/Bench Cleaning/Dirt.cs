/// <summary>
/// Dirt.cs
/// Handles the dirt behaviour on each bench.
/// Reduces dirt amount when sprayed and triggers
/// the bench cleaned event once fully cleaned.
/// </summary>
/// <author> Schanelle Leah Jackson </author>
/// <date> 13/02/2026 </date>
/// <StudentID> S10269101G </StudentID>

using UnityEngine;

public class Dirt : MonoBehaviour
{
    public float dirtAmount = 100f;
    private bool isCleaned = false;

    public AudioSource cleanSound;   

    public void Clean(float amount)
    {
        if (isCleaned) return;

        dirtAmount -= amount;

        if (dirtAmount <= 0)
        {
            isCleaned = true;

            // to play bench cleaned sound
            if (cleanSound != null)
                cleanSound.Play();

            BenchGameManager.Instance.BenchCleaned();

            gameObject.SetActive(false);
        }
    }
}