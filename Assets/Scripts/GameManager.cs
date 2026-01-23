using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    private int cluesFound = 0;

    void Awake() {
        Instance = this;
    }

    public void FoundClue() {
        cluesFound++;
        Debug.Log("Clues found: " + cluesFound);
        if (cluesFound >= 3) {
            Debug.Log("Enough clues! You can identify the lost child.");
        }
    }
}
