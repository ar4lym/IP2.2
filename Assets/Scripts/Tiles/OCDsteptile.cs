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
    public float convertDelay = 3f;

    // State
    private bool isConvertedGreen = false;
    private bool converting = false;
    private bool counted = false;
    private bool playerOnTile = false;

    private void Reset()
    {
        tileRenderer = GetComponentInParent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rig = other.transform.root;
        if (!rig.CompareTag("Player")) return;

        playerOnTile = true;

        if (tileType == TileType.CorrectRed)
        {
            if (!isConvertedGreen && !converting)
                StartCoroutine(ConvertToGreenAfterDelay());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Transform rig = other.transform.root;
        if (!rig.CompareTag("Player")) return;

        playerOnTile = false;
    }

    private IEnumerator ConvertToGreenAfterDelay()
    {
        converting = true;

        yield return new WaitForSeconds(convertDelay);

        // Must still be standing on the tile
        if (!playerOnTile)
        {
            converting = false;
            yield break;
        }

        // Turn tile green
        isConvertedGreen = true;
        converting = false;

        if (tileRenderer != null && greenMat != null)
            tileRenderer.material = greenMat;

        // Count once
        if (!counted)
        {
            counted = true;
            if (manager != null)
                manager.AddCompletedTile();
        }
    }

    // Optional: reset tile back to red
    public void ForceRed()
    {
        isConvertedGreen = false;
        converting = false;
        counted = false;

        if (tileRenderer != null && redMat != null)
            tileRenderer.material = redMat;
    }
}
