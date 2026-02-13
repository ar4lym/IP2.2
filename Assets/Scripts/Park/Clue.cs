/// <summary>
/// Clue.cs
/// Finds clue around the park area.
/// </summary>
/// <author> Aralyn Han Zi Ning </author>
/// <date> 23/01/2026 </date>
/// <StudentID> S10267170A </StudentID>
using UnityEngine;

/// <summary>
/// Activates a target GameObject only when all three clue objects are active.
/// Used to unlock missions or progress after collecting clues.
/// </summary>
public class Clue : MonoBehaviour
{
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private GameObject object3;
    [SerializeField] private GameObject targetObject;

    /// <summary>
    /// Checks every frame whether all clue objects are active,
    /// and activates the target object when conditions are met.
    /// </summary>
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