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
    public float convertDelay = 2f; // Changed to 2s as per your original request

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
        // Only look for objects with the "Player" tag
        if (!other.transform.root.CompareTag("Player")) return;

        playerOnTile = true;

        if (tileType == TileType.CorrectRed)
        {
            if (!isConvertedGreen && !converting)
            {
                StartCoroutine(ConvertToGreenAfterDelay());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.root.CompareTag("Player")) return;
        playerOnTile = false;
    }

    private IEnumerator ConvertToGreenAfterDelay()
    {
        converting = true;
        yield return new WaitForSeconds(convertDelay);

        // Fail-safe: If player leaves before 2 seconds, stop converting
        if (!playerOnTile)
        {
            converting = false;
            yield break;
        }

        isConvertedGreen = true;
        converting = false;

        if (tileRenderer != null && greenMat != null)
            tileRenderer.material = greenMat;

        if (!counted)
        {
            counted = true;
            if (manager != null)
                manager.AddCompletedTile();
        }
    }

    public void ForceRed()
    {
        isConvertedGreen = false;
        converting = false;
        counted = false;
        if (tileRenderer != null && redMat != null)
            tileRenderer.material = redMat;
    }
}