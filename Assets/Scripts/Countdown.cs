using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CountdownText;
    [SerializeField] float remainingTime;
    //private bool isPaused;

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -=Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
        }

        //Debug.Log("Timer Update running on: " + gameObject.name);
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        CountdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // public void PauseGame()
    // {
    //     Time.timeScale = 0f;
    // }

    // public void ResumeGame()
    // {
    //     Time.timeScale = 1f;
    // }

    // public void StopTimer()
    // {
    //     enabled = false; // stops Update
    // }

    // public float GetElapsedTime()
    // {
    //     return elapsedTime;
    // }
}

