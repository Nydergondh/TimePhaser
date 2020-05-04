using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollision : MonoBehaviour
{

    private CapsuleCollider2D feetCollider;
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        feetCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //if is on the air and touch the ground
        if (feetCollider.IsTouchingLayers(groundLayer) && PlayerMovement.player.deltaY > 0) {
            print("GotHere");
            PlayerMovement.player.isTouchingGround = true;
        }
        //if is not jumping, but is falling and touch the ground
        else if (feetCollider.IsTouchingLayers(groundLayer) && PlayerMovement.player.deltaY < 0) {
            PlayerMovement.player.isTouchingGround = true;
            print("GotHere1");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer == 8) {
            print(collision.gameObject.layer);
            if (!feetCollider.IsTouchingLayers(groundLayer) && PlayerMovement.player.deltaY <= 0) {
                PlayerMovement.player.Fall();
                print("GotHere2");
            }
        }
    }

}
