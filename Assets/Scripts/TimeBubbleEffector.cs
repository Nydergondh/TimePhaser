﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBubbleEffector : MonoBehaviour
{
    public LayerMask effectedLayers;

    private Collider2D bubbleCollider;
    [Range(0.0F, 1.0F)]
    public float timeModifier = 0.25f;
    private float normalTimeModiffier = 1;

    private Animator anim;

    private void Start() {
        bubbleCollider = GetComponent<Collider2D>();
        
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (bubbleCollider.IsTouchingLayers(effectedLayers)) {
            //slow down animator
            if (collision.GetComponent<SpokyEnemy>()) {
                if (collision.GetComponent<Animator>() != null) {
                    collision.GetComponent<Animator>().speed = timeModifier;
                }
                collision.GetComponent<SpokyEnemy>().movementSpeed = timeModifier;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (effectedLayers.value == (effectedLayers | (1 << collision.gameObject.layer))) {
            //speed up animator
            if (collision.GetComponent<SpokyEnemy>()) {
                if (collision.GetComponent<Animator>() != null) {
                    collision.GetComponent<Animator>().speed = normalTimeModiffier;
                }
                collision.GetComponent<SpokyEnemy>().movementSpeed = normalTimeModiffier;
            }

        }
    }
    
    public void KillBubble() {
        Destroy(gameObject, 2f);
    }

}
