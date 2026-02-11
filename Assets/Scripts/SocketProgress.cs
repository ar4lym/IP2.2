using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class SocketProgress : MonoBehaviour
{
    [Header("Correct Item")]
    public string correctItemTag;

    [Header("Progress Manager")]
    public ConviStoreManager storeManager;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip correctSFX;
    public AudioClip wrongSFX;

    [Header("Visual Feedback")]
    public float blinkDuration = 0.15f;
    public Color wrongColor = Color.red;

    private bool counted = false;

    // XR Socket → Select Entered
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject placedItem = args.interactableObject.transform.gameObject;

        // CORRECT item
        if (placedItem.CompareTag(correctItemTag))
        {
            audioSource.PlayOneShot(correctSFX);

            if (!counted)
            {
                counted = true;
                storeManager.AddItem();
            }
        }
        // WRONG item
        else
        {
            audioSource.PlayOneShot(wrongSFX);
            StartCoroutine(BlinkWrongItem(placedItem));
        }
    }

    private IEnumerator BlinkWrongItem(GameObject item)
    {
        Renderer rend = item.GetComponentInChildren<Renderer>();
        if (rend == null) yield break;

        Color originalColor = rend.material.color;
        rend.material.color = wrongColor;

        yield return new WaitForSeconds(blinkDuration);

        rend.material.color = originalColor;
    }
}