using UnityEngine;
using TMPro;

public class TileGameManager : MonoBehaviour
{
    [Header("Game Rules")]
    public int totalCorrectTiles = 9;
    public int maxHealth = 3;

    [Header("UI")]
    public TMP_Text counterText;     // e.g. "3/9"
    public TMP_Text healthText;      // e.g. "HP: 2"
    public GameObject congratsPopup; // panel popup

    private int completed = 0;
    private int health;

    private void Start()
    {
        health = maxHealth;
        if (congratsPopup != null) congratsPopup.SetActive(false);
        UpdateUI();
    }

    public void AddCompletedTile()
    {
        completed++;
        if (completed > totalCorrectTiles) completed = totalCorrectTiles;

        UpdateUI();

        if (completed >= totalCorrectTiles)
        {
            if (congratsPopup != null) congratsPopup.SetActive(true);
        }
    }

    public void Damage(int amount = 1)
    {
        health -= amount;
        if (health < 0) health = 0;
        UpdateUI();

        // Optional: handle death/reset here
        // if (health <= 0) { ... }
    }

    private void UpdateUI()
    {
        if (counterText != null) counterText.text = $"{completed}/{totalCorrectTiles}";
        if (healthText != null) healthText.text = $"HP: {health}";
    }
}
