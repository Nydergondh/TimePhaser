using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBubbleEffector : MonoBehaviour
{
    public int layerMask = 9;
    [Range(0.0F, 1.0F)]
    public float timeModifier = 0.25f;

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.layer == layerMask) {
            //slow down animator
            GetComponent<Animator>().speed = timeModifier;
            //slow down speed
            //TODO: change to enemy attributes
            GetComponent<PlayerMovement>().runSpeed = timeModifier * 5; 
            GetComponent<PlayerMovement>().airSpeed = timeModifier * 3;
            GetComponent<PlayerMovement>().jumpSpeed = timeModifier * 5;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == layerMask) {
            //slow down animator
            GetComponent<Animator>().speed = 1f;
            //slow down speed
            print("GotHere1");
            GetComponent<PlayerMovement>().runSpeed = 5;
            GetComponent<PlayerMovement>().airSpeed = 3;
            GetComponent<PlayerMovement>().jumpSpeed = 5;
        }
    }
    
}
