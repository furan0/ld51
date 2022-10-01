using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 using UnityEngine.SceneManagement;

public class ReloadSceneOnInput : MonoBehaviour
{
    bool isEnabled = false;
    public float enableDelay = 1.0f;

    public void OnShot(InputAction.CallbackContext context_) {
        if (isEnabled) {
            if(context_.performed) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //Reload scene
                //Scene scene = SceneManager.GetActiveScene();
                //SceneManager.LoadScene(scene.name);
            }else{
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //Scene scene = SceneManager.GetActiveScene();
                //SceneManager.LoadScene(scene.name);
            }

        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void enableAfterDelay() {
        StartCoroutine(enableAfterDelayRoutine());
    }

    IEnumerator enableAfterDelayRoutine() {
        yield return new WaitForSecondsRealtime(enableDelay);

        isEnabled = true;
    }
}
