using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpokyEnemy : MonoBehaviour, IDamage
{
    public float movementSpeed = 1f; // Maximum Horizontal Speed
    public float movementjump = 3f; // Jump Speed (once pressed (afterwards affected by gravity))

    public int health = 100;
    public int damage = 10;

    [HideInInspector] public SpokyVision spokyVision;
    [HideInInspector] public SpokyMovement spokyMovement;
    [HideInInspector] public SpokyCombat spokyCombat;

    public Transform spookyEyes;
    public Transform raycastDetect;
    public Transform abbys;

    public SpriteRenderer _renderer;

    public bool affectedTime = false;

    [HideInInspector]
    public AudioSource audioSource;

    private void Start() {
        spokyVision = GetComponent<SpokyVision>();
        spokyMovement = GetComponent<SpokyMovement>();
        spokyCombat = GetComponent<SpokyCombat>();

        audioSource = GetComponent<AudioSource>();
    }

    public int GetDamage() {
        return damage;
    }
}
