using UnityEngine;

public class Fanspinning : MonoBehaviour
{
    public Vector3 rotation;

    void Update()
    {
        this.transform.Rotate(rotation * 1 * Time.deltaTime);
    }
}


// public class NewMonoBehaviourScript : MonoBehaviour
// {
//     public Vector3 rotation;

//     void Update()
//     {
//         this.transform.Rotate(rotation * 1 * Time.deltaTime);
//     }
// }