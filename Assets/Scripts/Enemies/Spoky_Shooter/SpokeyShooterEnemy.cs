using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpokeyShooterEnemy : MonoBehaviour {
    public float movementSpeed = 1f; // Maximum Horizontal Speed
    public float movementjump = 3f; // Jump Speed (once pressed (afterwards affected by gravity))

    public int health = 100;
    public int damage = 10;

    public SpokeyShooterVision spokeyVision;
    public SpokeyShooterMovement spokeyMovement;
    public SpokeyShotCombat spokeyCombat;

    public Transform spookyEyes;
    public Transform bulletSpawnPoint;

    public SpriteRenderer _renderer;

    private void Start() {
        spokeyVision = GetComponent<SpokeyShooterVision>();
        spokeyMovement = GetComponent<SpokeyShooterMovement>();
        spokeyCombat = GetComponent<SpokeyShotCombat>();
    }

}
