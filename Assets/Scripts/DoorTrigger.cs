using UnityEngine;
using TMPro;

public class DoorTrigger : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject optionsPanel;      // Panel with 4 options
    public TextMeshProUGUI taskPrompt;   // Text for "complete tasks first" message

    [Header("Task Flags")]
    public bool allTasksDone = false;    // Set this based on your game logic

    private void Start()
    {
        // Hide both UI elements at the start
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
        else
            Debug.LogWarning("DoorTrigger: optionsPanel not assigned!");

        if (taskPrompt != null)
            taskPrompt.gameObject.SetActive(false);
        else
            Debug.LogWarning("DoorTrigger: taskPrompt not assigned!");
    }

    // Call this when the player interacts with the door
    public void OnPlayerInteract()
    {
        Debug.Log("Door interacted! allTasksDone = " + allTasksDone);

        // Safety checks before using UI
        if (allTasksDone)
        {
            if (optionsPanel != null)
            {
                optionsPanel.SetActive(true);
                Debug.Log("Options panel shown.");
            }
            else
            {
                Debug.LogError("Cannot show optionsPanel — it is not assigned!");
            }

            if (taskPrompt != null)
                taskPrompt.gameObject.SetActive(false);
        }
        else
        {
            if (taskPrompt != null)
            {
                taskPrompt.text = "Complete tasks first!";
                taskPrompt.gameObject.SetActive(true);
                Debug.Log("Task prompt shown.");
            }
            else
            {
                Debug.LogError("Cannot show taskPrompt — it is not assigned!");
            }

            if (optionsPanel != null)
                optionsPanel.SetActive(false);
        }
    }
}