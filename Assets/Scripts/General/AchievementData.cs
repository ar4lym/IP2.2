/// <summary>
/// AchievementData.cs
/// Used to store and pass around achievement information in the scene.
/// </summary>
/// <author> Aralyn Han Zi Ning </author>
/// <date> 13/02/2026 </date>
/// <studentID> S10267170A </studentID>
public class AchievementData
{
    /// <summary>
    /// The display title of the achievement.
    /// </summary>
    public string title;

    /// <summary>
    /// The name of the scene associated with this achievement.
    /// Used to track which scene must be completed to unlock this achievement.
    /// </summary>
    public string sceneName;


    /// <summary>
    /// A description explaining how to earn or what the achievement represents.
    /// </summary>
    public string description;

    /// Constructor to initialize an achievement with a title, scene name, and description.
  
    /// <summary>
    /// Initializes a new instance of the AchievementData class.
    /// </summary>
    public AchievementData(string title, string sceneName, string description)
    {
        this.title = title;
        this.sceneName = sceneName;
        this.description = description;
    }
}
