using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void Sceneloader()
    {
        SceneManager.LoadScene(sceneName);
        StartCoroutine(SendSceneData(sceneName));
    }

    private IEnumerator SendSceneData(string scene)
    {
        string url = "https://yourserver.com/api/scene"; // Replace with your backend endpoint
        WWWForm form = new WWWForm();
        form.AddField("sceneName", scene);
        form.AddField("timestamp", System.DateTime.UtcNow.ToString("o"));

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending scene data: " + www.error);
            }
            else
            {
                Debug.Log("Scene data sent successfully!");
            }
        }
    }
}
