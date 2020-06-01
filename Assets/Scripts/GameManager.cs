using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public bool endGame = false;

    void Awake() {
        if (gameManager != null) {
            Destroy(this);
            return;
        }
        gameManager = this;
    }

}
