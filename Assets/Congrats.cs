helpusing UnityEngine;

public class Congrats : MonoBehaviour
{
    public GameObject popupPanel;
    private bool shown = false;

    void Start()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!shown && other.transform.root.CompareTag("Player"))
        {
            if (popupPanel != null)
                popupPanel.SetActive(true);
            shown = true;
        }
    }
}
