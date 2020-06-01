using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager gameManager;

    public Canvas UI;
    public bool endGame = false;

    void Awake() {
        if (gameManager != null) {
            Destroy(this);
            return;
        }
        gameManager = this;
    }

    public static IEnumerator MuteAudioOnDeath(){
        while (AudioListener.volume > 0) {
            AudioListener.volume -= 0.5f * Time.deltaTime;
            yield return null;
        }
    }

    public static void UnMuteAudio() {
        AudioListener.volume = 1;
    }
}
