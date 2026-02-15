/// <summary>
/// Fanspinning.cs
/// this controls the fan in the second scene
/// it spins continuously.
/// </summary>
/// <author> Lee Jia Lu </author>
/// <date> 25/01/2026 </date>
/// <StudentID> S10269187E </StudentID>
using UnityEngine;

public class Fanspinning : MonoBehaviour
{
    /// <summary>
    /// Rotation speed and direction on each axis.
    /// Putting it (0, 676.7, 0) will spin on Y axis.
    /// </summary>
    public Vector3 rotation;

    /// <summary>
    /// Called once per frame.
    /// Rotates the object smoothly using Time.deltaTime.
    /// </summary>
    void Update()
    {
        this.transform.Rotate(rotation * 1 * Time.deltaTime);
    }
}
