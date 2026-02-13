/// <summary>
/// ClueManager.cs
/// Manages clue answering, feedback, and scene transitions.
/// </summary>
/// <author> Aralyn Han Zi Ning </author>
/// <date> 02/08/2026 </date>
/// <StudentID> S10267170A </StudentID>
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the clue interaction system by handling player answers,
/// displaying correct or wrong feedback, stopping the timer,
/// and transitioning to the next scene.
/// </summary>
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

    /// <summary>
    /// Finds Timer reference and hides feedback objects on start.
    /// </summary>
    private void Start()
    {
        timer = FindObjectOfType<Timer>();

        if (correctObject != null)
            correctObject.SetActive(false);

        if (wrongObject != null)
            wrongObject.SetActive(false);
    }
    /// <summary>
    /// Called when first answer button is clicked.
    /// </summary>
    public void OnFirstOptionClicked()
    {
        HandleAnswer(isFirstOptionCorrect);
    }

    /// <summary>
    /// Called when second answer button is clicked.
    /// </summary>
    public void OnSecondOptionClicked()
    {
        HandleAnswer(!isFirstOptionCorrect);
    }

    /// <summary>
    /// Processes player's answer, stops timer,
    /// shows feedback, and schedules scene change.
    /// </summary>
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

    /// <summary>
    /// Loads the next scene after feedback delay.
    /// </summary>
    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
