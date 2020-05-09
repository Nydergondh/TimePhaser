using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBubbleEffector : MonoBehaviour
{
    public LayerMask effectedLayers;

    private Collider2D bubbleCollider;
    [Range(0.0F, 1.0F)]
    public float timeModifier = 0.25f;
    private float normalTimeModiffier = 4;

    private Animator anim;

    private void Start() {
        bubbleCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (bubbleCollider.IsTouchingLayers(effectedLayers)) {
            //slow down animator
            if (collision.GetComponent<Animator>() != null) {
                collision.GetComponent<Animator>().speed = timeModifier;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (effectedLayers.value == (effectedLayers | (1 << collision.gameObject.layer))) {
            //slow down animator
            if (collision.GetComponent<Animator>() != null) {
                collision.GetComponent<Animator>().speed = normalTimeModiffier;
            }
            //if (collision.GetComponent<>() {

            //}
        }
    }
    
    public void KillBubble() {
        Destroy(gameObject, 2f);
    }

}
