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
    private AudioSource _audioSource;

    private void Start() {
        bubbleCollider = GetComponent<Collider2D>();
        _audioSource = GetComponent<AudioSource>();

        _audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.TimeBubble));

        PlayerStatus.player.WithdrawEnergy(25);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (bubbleCollider.IsTouchingLayers(effectedLayers)) {
            //slow down animator
            //slow down animator and velocities
            if (collision.GetComponent<SpokyEnemy>()) {
                if (collision.GetComponent<Animator>() != null) {
                    collision.GetComponent<Animator>().speed = timeModifier;
                }
                collision.GetComponent<SpokyEnemy>().movementSpeed *= timeModifier;

                //modify sounds to be slower
                collision.GetComponent<SpokyEnemy>().affectedTime = true;
                collision.GetComponent<SpokyEnemy>().audioSource.pitch *= timeModifier;
            }

            else if (collision.GetComponent<SpokeyShooterEnemy>()) {
                if (collision.GetComponent<Animator>() != null) {
                    collision.GetComponent<Animator>().speed *= timeModifier;
                }
                collision.GetComponent<SpokeyShooterEnemy>().movementSpeed *= timeModifier;

                collision.GetComponent<SpokeyShooterEnemy>().affectedTime = true;
                collision.GetComponent<SpokeyShooterEnemy>().audioSource.pitch *= timeModifier;
            }

            else if (collision.GetComponent<Projectile>()) {
                collision.GetComponent<Projectile>().movementSpeed *= timeModifier;
            }

            else if (collision.GetComponent<Smasher>()) {
                collision.GetComponent<Smasher>().movementSpeed *= timeModifier;

                collision.GetComponent<Smasher>().audioSource.pitch *= timeModifier;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (effectedLayers.value == (effectedLayers | (1 << collision.gameObject.layer))) {
            //speed up animator and velocities
            if (collision.GetComponent<SpokyEnemy>()) {
                if (collision.GetComponent<Animator>() != null) {
                    collision.GetComponent<Animator>().speed = 1;
                }
                collision.GetComponent<SpokyEnemy>().movementSpeed *= normalTimeModiffier;

                //modify sounds to be normal
                collision.GetComponent<SpokyEnemy>().affectedTime = false;
                collision.GetComponent<SpokyEnemy>().audioSource.pitch *= normalTimeModiffier;
            }

           else if (collision.GetComponent<SpokeyShooterEnemy>()) {
                if (collision.GetComponent<Animator>() != null) { 
                    collision.GetComponent<Animator>().speed = 1;
                }
                collision.GetComponent<SpokeyShooterEnemy>().movementSpeed *= normalTimeModiffier;

                collision.GetComponent<SpokeyShooterEnemy>().affectedTime = false;
                collision.GetComponent<SpokeyShooterEnemy>().audioSource.pitch *= normalTimeModiffier;
            }

            else if (collision.GetComponent<Projectile>()) {
                collision.GetComponent<Projectile>().movementSpeed *= normalTimeModiffier;
            }

            else if (collision.GetComponent<Smasher>()) {
                collision.GetComponent<Smasher>().movementSpeed *= normalTimeModiffier;

                collision.GetComponent<Smasher>().audioSource.pitch *= normalTimeModiffier;
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
