using UnityEngine;

public class Clue : MonoBehaviour
{
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private GameObject object3;
    [SerializeField] private GameObject targetObject;

    void Update()
    {
        // Check if all three objects are active
        bool allActive = object1.activeSelf && object2.activeSelf && object3.activeSelf;

        // Set the target object active only when all three are active
        if (targetObject != null)
        {
            targetObject.SetActive(allActive);
        }
    }
}