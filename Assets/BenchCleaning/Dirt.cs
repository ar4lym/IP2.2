// using UnityEngine;

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


public class Dirt : MonoBehaviour
{
    public float dirtAmount = 100f;
    private bool isCleaned = false;

    public void Clean(float amount)
    {
        if (isCleaned) return;

        dirtAmount -= amount;

        if (dirtAmount <= 0)
        {
            isCleaned = true;

            BenchGameManager.Instance.BenchCleaned();

            gameObject.SetActive(false);
        }
    }
}
