using UnityEngine;
using TMPro;

public class Clue : MonoBehaviour {
    public TextMeshProUGUI clueText; // assign in Inspector
    private bool found = false;

    private void OnTriggerEnter(Collider other) {
        if (!found && other.CompareTag("PlayerHand")) {
            found = true;
            clueText.gameObject.SetActive(true); // show floating text
            clueText.text = "This looks carefully arranged...";
            GameManager.Instance.FoundClue();
        }
    }
}
