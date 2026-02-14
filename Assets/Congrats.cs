using UnityEngine;

public class Congrats : MonoBehaviour
{
    [Header("Popup Settings")]
    public GameObject popupPanel;
    [Tooltip("Show popup only once per tile entry")]
    public bool showOnce = true;
    [Tooltip("Auto-hide popup after seconds (0 = never)")]
    public float autoHideSeconds = 0f;

    [Header("Audio Settings")]
    public AudioSource popupSound;
    public bool playSound = false;

    private bool shown = false;

    void Start()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            if (showOnce && shown) return;

            if (popupPanel != null)
                popupPanel.SetActive(true);

            if (playSound && popupSound != null)
                popupSound.Play();

            if (autoHideSeconds > 0f && popupPanel != null)
                Invoke(nameof(HidePopup), autoHideSeconds);

            shown = true;
        }
    }

    void HidePopup()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }
}