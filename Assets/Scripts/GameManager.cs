using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public Transform instaciatedFather;

    private void Start() {

        if (gameManager != null) {
            gameManager = null;
        }

        gameManager = this;

    }
    
    public Transform GetFather() {
        return instaciatedFather;
    }
}
