using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Use This In conjunt with a UI to load new scene 
public class LoadScene : MonoBehaviour
{
    public int currentLevel;
    public int sceneIndex;
    //public GameObject loadingScreen;
    //public Slider slider;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return) && currentLevel == 0) {
            LoadLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Return) && currentLevel == 1 && GameManager.gameManager.endGame) {
            LoadLevel();
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void LoadLevel() {
        StartCoroutine(LoadAsynchorously());
    }

    IEnumerator LoadAsynchorously() {
        float progress;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        //loadingScreen.SetActive(true);

        while (!operation.isDone) {
            progress = Mathf.Clamp01(operation.progress / 0.9f);
            print(progress);
            //slider.value = progress;
            yield return null;
        }
    }

}
