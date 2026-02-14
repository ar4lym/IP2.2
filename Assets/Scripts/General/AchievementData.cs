/// <summary>
/// AchievementData.cs
/// Used to store and pass around achievement information in the scene.
/// </summary>
/// <author> Aralyn Han Zi Ning </author>
/// <date> 13/02/2026 </date>
/// <studentID> S10267170A </studentID>
public class AchievementData
{

    public string title;

    public string sceneName;

  

    public string description;

    /// Constructor to initialize an achievement with a title, scene name, and description.
  
    public AchievementData(string title, string sceneName, string description)
    {
        this.title = title;
        this.sceneName = sceneName;
        this.description = description;
    }
}
