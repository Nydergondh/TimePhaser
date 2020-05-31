using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBubbleEffector : MonoBehaviour
{
    public LayerMask effectedLayers;

    private Collider2D bubbleCollider;
    private List<Collider2D> effectdColliders = new List<Collider2D>(); //used to resolve bug in the center of the when object exists in the center of the bubble

    [Range(0.0F, 1.0F)]
    public float timeModifier = 0.25f;
    private float normalTimeModiffier = 4;

    private Animator anim;

    private void Start() {
        bubbleCollider = GetComponent<Collider2D>();

        PlayerStatus.player.WithdrawEnergy(25);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (bubbleCollider.IsTouchingLayers(effectedLayers)) {
            //slow down animator
            if (collision.GetComponent<SpokyEnemy>()) {
                if (collision.GetComponent<Animator>() != null) {
                    collision.GetComponent<Animator>().speed = timeModifier;
                }
                collision.GetComponent<SpokyEnemy>().movementSpeed *= timeModifier;
            }
            else if (collision.GetComponent<SpokeyShooterEnemy>()) {
                if (collision.GetComponent<Animator>() != null) {
                    collision.GetComponent<Animator>().speed *= timeModifier;
                }
                collision.GetComponent<SpokeyShooterEnemy>().movementSpeed *= timeModifier;
            }

            else if (collision.GetComponent<Projectile>()) {
                collision.GetComponent<Projectile>().movementSpeed *= timeModifier;
            }

            else if (collision.GetComponent<Smasher>()) {
                collision.GetComponent<Smasher>().movementSpeed *= timeModifier;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (effectedLayers.value == (effectedLayers | (1 << collision.gameObject.layer))) {
            //effectdColliders.Remove(collision);
            //speed up animator
            if (collision.GetComponent<SpokyEnemy>()) {
                if (collision.GetComponent<Animator>() != null) {
                    collision.GetComponent<Animator>().speed = 1;
                }
                collision.GetComponent<SpokyEnemy>().movementSpeed *= normalTimeModiffier;
            }

           else if (collision.GetComponent<SpokeyShooterEnemy>()) {
                if (collision.GetComponent<Animator>() != null) { 
                    collision.GetComponent<Animator>().speed = 1;
                }
                collision.GetComponent<SpokeyShooterEnemy>().movementSpeed *= normalTimeModiffier;
            }
            else if (collision.GetComponent<Projectile>()) {
                collision.GetComponent<Projectile>().movementSpeed *= normalTimeModiffier;
            }
            else if (collision.GetComponent<Smasher>()) {
                collision.GetComponent<Smasher>().movementSpeed *= normalTimeModiffier;
            }
        }
    }
    
    public void KillBubble() {
        Destroy(gameObject, 2f);
    }
    //called in animation (NO NEED TO USE [FOR NOW])

    //public void UnslowCenterObjects() {
    //    foreach (Collider2D col in effectdColliders) {
    //        print(col.name);
    //        if (col.GetComponent<SpokyEnemy>()) {
    //            if (col.GetComponent<Animator>() != null) {
    //                col.GetComponent<Animator>().speed = 1;
    //            }
    //            col.GetComponent<SpokyEnemy>().movementSpeed *= normalTimeModiffier;
    //        }

    //        else if (col.GetComponent<SpokeyShooterEnemy>()) {
    //            if (col.GetComponent<Animator>() != null) {
    //                col.GetComponent<Animator>().speed = 1;
    //            }
    //            col.GetComponent<SpokeyShooterEnemy>().movementSpeed *= normalTimeModiffier;
    //        }
    //        else if (col.GetComponent<Projectile>()) {
    //            col.GetComponent<Projectile>().movementSpeed *= normalTimeModiffier;
    //        }
    //    }
    //    effectdColliders.Clear();
    //}

}
