// public class Dirt : MonoBehaviour
// {
//     public float dirtAmount = 100f;

//     public void Clean(float amount)
//     {
//         dirtAmount -= amount;

//         if (dirtAmount <= 0)
//         {
//             gameObject.SetActive(false);
//         }
//     }
// }


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