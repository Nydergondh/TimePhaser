using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus player;

    public PlayerMovement playerMovement;
    public PlayerCombat playerCombat;
    public PlayerGroundCollision playerGround;

    public Transform damageUISpawnPoint;

    void Awake()
    {
        if (player != null) {
            player = null;
        }
        player = this;

        playerCombat = GetComponent<PlayerCombat>();
        playerMovement = GetComponent<PlayerMovement>();
        playerGround = GetComponent<PlayerGroundCollision>();
    }

    void Update() {
        playerCombat.Attack();
        playerCombat.TimeBubbling();
        playerGround.FallPlataform();
        playerMovement.Movement();
    }
}
