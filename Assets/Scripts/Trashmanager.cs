using UnityEngine;
using TMPro;

public class Trashmanager : MonoBehaviour
{
    public static Trashmanager Instance;

    public int totalTrash = 5;
    // public int totalPuddles = 0;

    private int trashCollected = 0;
    // private int puddlesCleaned = 0;

    public TextMeshProUGUI trashCounterText;
    public TextMeshProUGUI CounterText;
    //public GameObject levelCompleteUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateTrashUI();
        //levelCompleteUI.SetActive(false);
    }

    public void TrashCollected()
    {
        trashCollected++;
        UpdateTrashUI();
        //CheckCompletion();
    }

    // public void PuddleCleaned()
    // {
    //     puddlesCleaned++;
    //     //CheckCompletion();
    // }

    void UpdateTrashUI()
    {
        trashCounterText.text = trashCollected + " / " + totalTrash;
    }

    // void CheckCompletion()
    // {
    //     if (trashCollected >= totalTrash && puddlesCleaned >= totalPuddles)
    //     {
    //         LevelComplete();
    //     }
    // }

    // void LevelComplete()
    // {
    //     Debug.Log("LEVEL COMPLETE");
    //     levelCompleteUI.SetActive(true);
    // }
}