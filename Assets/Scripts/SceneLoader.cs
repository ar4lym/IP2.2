using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Name of the scene to load
    public string sceneName;

    // This function will be called when the button is clicked
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
