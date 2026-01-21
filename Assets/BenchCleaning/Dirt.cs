using UnityEngine;

public class Dirt : MonoBehaviour
{
    public float dirtAmount = 100f;

    public void Clean(float amount)
    {
        dirtAmount -= amount;

        if (dirtAmount <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
