/// <summary>
/// OCDsteptile.cs
/// Controls the behavior of the step tiles in the OCD mechanic.
/// Player uses teleportation to step on tiles. If they step on the correct red tile and stay for 2 seconds, it turns green and counts towards completion.
/// Also manages the color change of tile from red to green 
/// Communicates with the TileGameManager to update the count of completed tiles.
/// </summary>
/// <author> Raeanne Ho </author>
/// <date> 14/02/2026 </date>
/// <StudentID> S10265738J </StudentID>


using System.Collections;
using UnityEngine;

public class OCDStepTile : MonoBehaviour
{
    public enum TileType { CorrectRed, WrongGreen }

    [Header("Assign")]
    public TileType tileType = TileType.WrongGreen;
    public TileGameManager manager;

    [Header("Correct Tile Settings")]
    public Renderer tileRenderer;
    public Material redMat;
    public Material greenMat;
    public float convertDelay = 2f; // 2 seconds delay when color turn red to green 

    private bool isConvertedGreen = false;
    private bool converting = false;
    private bool counted = false;
    private bool playerOnTile = false;


/// <summary>
    /// Called when another box collider enters the tile's trigger area.
    /// Starts the changing red to green if conditions are met.
    /// Conditions: Player must step on the tile, tile must be a correct red tile, and it should not already be green or in the process of converting.
    ///
    private void OnTriggerEnter(Collider other)
    {
        // Look for the "Player" tag
        if (!other.transform.root.CompareTag("Player")) return;

        playerOnTile = true;

        if (tileType == TileType.CorrectRed && !isConvertedGreen && !converting)
        {
            StartCoroutine(ConvertToGreenAfterDelay());
        }
    }

     /// <summary>
    /// Called when the player exits the tile's trigger area.
    /// Stops tile color change if the player leaves before 2 secs.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.root.CompareTag("Player")) return;
        playerOnTile = false;
    }
    
    
    /// <summary>
    /// Waits for the specified delay (2sec) before converting the tile to green.
    /// If the player leaves before the delay ends, color change is cancelled.
    /// Adds +1 to the counter in TileGameManager when color change is successful 
    /// </summary>
    private IEnumerator ConvertToGreenAfterDelay()
    {
        converting = true;
        yield return new WaitForSeconds(convertDelay);

        // If player left the tile, stop the process
        if (!playerOnTile)
        {
            converting = false;
            yield break;
        }

        isConvertedGreen = true;
        converting = false;

        // Change visual to green
        if (tileRenderer != null && greenMat != null)
            tileRenderer.material = greenMat;

        // Send the count to the manager
        if (!counted)
        {
            counted = true;
            if (manager != null)
                manager.AddCompletedTile();
        }
    }
}