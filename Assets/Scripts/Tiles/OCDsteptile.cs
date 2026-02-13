using System.Collections;
using UnityEngine;

public class OCDStepTile : MonoBehaviour
{
    public enum TileType { CorrectRed, WrongGreen }

    [Header("Assign")]
    public TileType tileType = TileType.WrongGreen;
    public TileGameManager manager;

    [Header("Correct Tile Settings")]
    public Renderer tileRenderer;      // the mesh renderer to change material
    public Material redMat;
    public Material greenMat;
    public float convertDelay = 2f;

    [Header("Wrong Tile Settings")]
    public float damageCooldown = 1.0f; // prevents hp melting instantly

    private bool playerOnTile = false;

    // Correct tile state
    private bool isConvertedGreen = false;
    private bool converting = false;
    private bool counted = false;

    // Wrong tile cooldown
    private float nextDamageTime = 0f;

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
            // If already converted, do nothing
            if (isConvertedGreen || converting) return;
            StartCoroutine(ConvertToGreenAfterDelay());
        }
        else
        {
            TryDamage();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // In case they "stand" on wrong tile after teleport
        Transform rig = other.transform.root;
        if (!rig.CompareTag("Player")) return;

        if (tileType == TileType.WrongGreen)
            TryDamage();
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

        // Optional rule: require them to still be on the tile when time finishes
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
            if (manager != null) manager.AddCompletedTile();
        }
    }

    private void TryDamage()
    {
        if (Time.time < nextDamageTime) return;
        nextDamageTime = Time.time + damageCooldown;

        if (manager != null)
            manager.Damage(1);
    }

    // Optional helper if you want to set the starting mat
    public void ForceRed()
    {
        isConvertedGreen = false;
        converting = false;
        counted = false;

        if (tileRenderer != null && redMat != null)
            tileRenderer.material = redMat;
    }
}

