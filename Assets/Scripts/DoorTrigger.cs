using UnityEngine;
using TMPro;

public class DoorTrigger : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject optionsPanel;      // Panel with 4 options
    public TextMeshProUGUI taskPrompt;   // Text for "complete tasks first" message

    [Header("Task Flags")]
    public bool allTasksDone = false;    // Set this based on your game logic

    // Call this when the player interacts with the door
    public void OnPlayerInteract()
    {
        if (allTasksDone)
        {
            // Show the 4 options for places to move
            if (optionsPanel != null)
                optionsPanel.SetActive(true);
        }
        else
        {
            // Show prompt to complete tasks
            if (taskPrompt != null)
            {
                taskPrompt.text = "You must complete all tasks first!";
                taskPrompt.gameObject.SetActive(true);
            }
        }
    }
}