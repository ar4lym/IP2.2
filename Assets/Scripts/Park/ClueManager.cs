using UnityEngine;
using UnityEngine.SceneManagement;

public class ClueManager : MonoBehaviour
{
    [Header("Answer Settings")]
    public bool isFirstOptionCorrect = true;   // Set in Inspector

    [Header("Feedback Objects")]
    public GameObject correctObject;
    public GameObject wrongObject;

    [Header("Scene")]
    public string nextSceneName = "SceneVariation";

    private Timer timer;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();

        if (correctObject != null)
            correctObject.SetActive(false);

        if (wrongObject != null)
            wrongObject.SetActive(false);
    }

    // Assign this to Button 1
    public void OnFirstOptionClicked()
    {
        HandleAnswer(isFirstOptionCorrect);
    }

    // Assign this to Button 2
    public void OnSecondOptionClicked()
    {
        HandleAnswer(!isFirstOptionCorrect);
    }

    private void HandleAnswer(bool isCorrect)
    {
        // Stop timer immediately
        if (timer != null)
            timer.StopTimer();
        else
            Debug.LogWarning("Timer not found!");

        if (isCorrect)
        {
            if (correctObject != null)
                correctObject.SetActive(true);
        }
        else
        {
            if (wrongObject != null)
                wrongObject.SetActive(true);
        }

        // After 5 seconds load next scene
        Invoke(nameof(LoadNextScene), 6.7f);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
