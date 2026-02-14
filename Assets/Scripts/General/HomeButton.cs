/// <summary>
/// HomeButton.cs
/// This script handles the functionality of the Home Button,
/// which allows the player to navigate back to the Scene Variation scene.
/// </summary>
/// <author> Leong Ming Hui </author>
/// <date> 12/02/2026 </date>
/// <StudentID> S10267664J </StudentID>

using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    public void GoToNextScene()
    {
        SceneManager.LoadScene("SceneVariation");
    }
}
