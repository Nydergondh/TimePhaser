using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpokyEnemy : MonoBehaviour
{
    public float movementSpeed = 1f; // Maximum Horizontal Speed
    public float movementjump = 3f; // Jump Speed (once pressed (afterwards affected by gravity))

    public int health = 100;
    public int damage = 10;

    public SpokyVision spokyVision;
    public SpokyMovement spokyMovement;
    public SpokyCombat spokyCombat;

    public Transform spookyEyes;
    public Transform raycastDetect;
    public Transform abbys;

    public SpriteRenderer _renderer;

    private void Start() {
        spokyVision = GetComponent<SpokyVision>();
        spokyMovement = GetComponent<SpokyMovement>();
        spokyCombat = GetComponent<SpokyCombat>();
    }

}
