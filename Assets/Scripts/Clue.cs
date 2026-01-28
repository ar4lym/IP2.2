using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;  // required for XR events

public class Clue : MonoBehaviour {
    public TextMeshProUGUI clueText;   // assign in Inspector
    private bool clueFound = false;

    void Start() {
        if (clueText != null) {
            clueText.gameObject.SetActive(false); // start hidden
        }
    }

    // Hook this up in Inspector to XR Simple Interactable events
    public void InspectClue() {
        if (!clueFound) {
            clueFound = true;

            if (clueText != null) {
                clueText.gameObject.SetActive(true); // show text
            }

            if (gameObject.CompareTag("Clue1")) {
                GameManager.Instance.RegisterClueFound(1);
            } else if (gameObject.CompareTag("Clue2")) {
                GameManager.Instance.RegisterClueFound(2);
            } else if (gameObject.CompareTag("Clue3")) {
                GameManager.Instance.RegisterClueFound(3);
            }
        }
    }

    public void StopInspecting() {
        if (clueText != null) {
            clueText.gameObject.SetActive(false); // hide text
        }
    }
}
