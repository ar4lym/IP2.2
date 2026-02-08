using UnityEngine;
using TMPro;

public class DoorTrigger : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject optionsPanel;      // Panel with 4 options
    public TextMeshProUGUI taskPrompt;   // Text for "complete tasks first" message

    [Header("Task Flags")]
    public bool allTasksDone = false;    // Set this based on your game logic

    // This runs when the scene starts
    private void Start()
    {
        // Hide both UI elements at the start
        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        if (taskPrompt != null)
            taskPrompt.gameObject.SetActive(false);
    }

    // Call this when the player interacts with the door
    public void OnPlayerInteract()
    {
        if (allTasksDone)
        {
            // Show options panel and hide task prompt
            if (optionsPanel != null)
                optionsPanel.SetActive(true);

            if (taskPrompt != null)
                taskPrompt.gameObject.SetActive(false);
        }
        else
        {
            // Show task prompt and hide options panel
            if (taskPrompt != null)
            {
                taskPrompt.text = "Complete tasks first!";
                taskPrompt.gameObject.SetActive(true);
            }

            if (optionsPanel != null)
                optionsPanel.SetActive(false);
        }
    }
}