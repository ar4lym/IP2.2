/// <summary>
/// BGMManager.cs
/// This script manages the background music in the convenience store.
/// It handles switching between calm and anxious background music based on time.
/// </summary>
/// <author> Leong Ming Hui </author>
/// <date> 06/02/2026 </date>
/// <StudentID> S10267664J </StudentID>

using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource csBGM;
    public AudioSource anxiousBGM;

    public float anxietyStartTime = 60f;
    public float fadeSpeed = 0.4f;

    private float localTimer = 0f;
    private bool anxietyActive = false;
    private bool taskCompleted = false;

    void Start()
    {
        csBGM.Play();
        anxiousBGM.volume = 0f;
    }

    void Update()
    {
        if (taskCompleted) return;

        localTimer += Time.deltaTime;

        if (localTimer >= anxietyStartTime)
        {
            anxietyActive = true;
        }

        if (anxietyActive)
        {
            csBGM.volume = Mathf.MoveTowards(csBGM.volume, 0f, fadeSpeed * Time.deltaTime);

            if (!anxiousBGM.isPlaying)
                anxiousBGM.Play();

            anxiousBGM.volume = Mathf.MoveTowards(anxiousBGM.volume, 0.8f, fadeSpeed * Time.deltaTime);
        }
    }

    public void OnTaskCompleted()
    {
        taskCompleted = true;

        csBGM.volume = 0.6f;
        anxiousBGM.Stop();
        anxiousBGM.volume = 0f;
    }
}
