using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;

public class ParkTP : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void LoadNextScene()
    {
        int trash = Trashmanager.Instance.GetCollectedTrash();
        int puddles = PuddleManager.Instance.GetCleanedPuddles();

        if (trash == 5 && puddles == 3)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("Not finished yet!");
        }
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GameObject spawn = GameObject.Find("SpawnPoint");

        if (spawn != null)
        {
            XROrigin xrOrigin = FindObjectOfType<XROrigin>();

            if (xrOrigin != null)
            {
                xrOrigin.transform.position = spawn.transform.position;
                xrOrigin.transform.rotation = spawn.transform.rotation;
            }
        }
    }
}