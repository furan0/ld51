using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string currentSceneName;
    //[SerializeField] private string firstSceneName = "menu";
    // Start is called before the first frame update
    void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        //switchScene(firstSceneName);
    }

    public void switchScene(string sceneName) {
        //Start unloading scene if needed
        //if (!currentSceneName.Equals(""))
        //    SceneManager.UnloadSceneAsync(currentSceneName);

        //Load new scene
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        SceneManager.LoadScene(sceneName);
        currentSceneName = sceneName;
    }

    public void switchSceneAfterDelay(string sceneName, float delay) {
        StartCoroutine(doSwitch(sceneName, delay));
    }

    IEnumerator doSwitch(string sceneName, float delay) {
        yield return new WaitForSecondsRealtime(delay);
        switchScene(sceneName);
    }
}
