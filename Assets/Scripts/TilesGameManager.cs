using UnityEngine;
using TMPro;

public class TilesGameManager : MonoBehaviour
{
	[Header("UI References")]
	public TextMeshProUGUI tileCounterText;
	public GameObject congratsPopup;
	public Timer timer; // Assign this in the Inspector

	[Header("Tile Settings")]
	public int[] congratsTiles = { 17, 27 };

	private int tileTeleportCount = 0;
	private System.Collections.Generic.HashSet<int> teleportedTiles = new System.Collections.Generic.HashSet<int>();

	void Start()
	{
		UpdateTileCounter();
		if (congratsPopup != null)
			congratsPopup.SetActive(false);
	}

	// Call this method when the user teleports to a tile
	// Call this method when the user teleports to a tile
	// Pass the tile GameObject as parameter
	// Call this method when the user teleports to a tile
	// Pass the tile GameObject and its tile number
	public void OnTileTeleported(GameObject tile, int tileNumber)
	{
		if (tile != null && tile.name.Contains("Correcttile1"))
		{
			// Only count unique Correcttile1 tiles
			if (teleportedTiles.Add(tileNumber))
			{
				tileTeleportCount = teleportedTiles.Count;
				UpdateTileCounter();
			}

			// Show congrats popup and stop timer if tile is a congrats tile
			if (System.Array.IndexOf(congratsTiles, tileNumber) >= 0)
			{
				ShowCongratsPopup();
				if (timer != null)
				{
					timer.StopTimer();
				}
			}
		}
		else
		{
			// Reset count and HashSet if not Correcttile1
			tileTeleportCount = 0;
			teleportedTiles.Clear();
			UpdateTileCounter();
		}
	}

	private void UpdateTileCounter()
	{
		if (tileCounterText != null)
			tileCounterText.text = $"Tiles Teleported: {tileTeleportCount}";
	}

	private void ShowCongratsPopup()
	{
		if (congratsPopup != null)
			congratsPopup.SetActive(true);
	}

	// Optionally, call this to hide the popup (e.g., from a button)
	public void HideCongratsPopup()
	{
		if (congratsPopup != null)
			congratsPopup.SetActive(false);
	}
}
