using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Use This In conjunt with a UI to load new scene 
public class LoadScene : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider slider;

    public void LoadLevel(int sceneIndex) {
        StartCoroutine(LoadAsynchorously(sceneIndex));
    }

    IEnumerator LoadAsynchorously(int sceneIndex) {
        float progress;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while (!operation.isDone) {
            progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }
    }
}
