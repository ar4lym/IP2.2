using UnityEngine;

public class SocketProgress : MonoBehaviour
{

     public ConviStoreManager progressManager;
     private bool isCompleted = false;

     public void OnItemPlaced()
     {
         if (isCompleted) return;

         isCompleted = true;
         progressManager.AddItem();
     }
}
