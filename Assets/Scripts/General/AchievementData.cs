/// <summary>
/// SceneEntryData.cs
///  Used to  store and pass around achievement information in the scene.
/// <author> Aralyn Han Zi Ning </author>
/// <date> 13/02/2026 </date>
/// <StudentID> S10267170A </StudentID>
public class AchievementData
{
    public string title;
    public string sceneName;
    public string description;

    public AchievementData(string title, string sceneName, string description)
    {
        this.title = title;
        this.sceneName = sceneName;
        this.description = description;
    }
}