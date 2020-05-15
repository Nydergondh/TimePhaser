using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus player;

    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;

    void Awake()
    {
        if (player != null) {
            player = null;
        }
        player = this;
    }

    void Start() {
        playerCombat = GetComponent<PlayerCombat>();
        playerMovement = GetComponent<PlayerMovement>();
    }

}
