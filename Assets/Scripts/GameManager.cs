using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    private bool clue1Found = false;
    private bool clue2Found = false;
    private bool clue3Found = false;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void RegisterClueFound(int clueID) {
        switch (clueID) {
            case 1: clue1Found = true; break;
            case 2: clue2Found = true; break;
            case 3: clue3Found = true; break;
        }

        Debug.Log($"Clue {clueID} found!");

        if (clue1Found && clue2Found && clue3Found) {
            UnlockChildren();
        }
    }

    void UnlockChildren() {
        Debug.Log("All clues found! Children interaction unlocked.");
        // Later: enable NPC scripts here
    }
}
