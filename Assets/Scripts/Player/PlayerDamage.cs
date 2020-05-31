using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public LayerMask spokyLayer;
    public GameObject particlePrefab;
    public Transform particleParent;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (spokyLayer == (spokyLayer | 1 << collision.gameObject.layer)) {

            if (collision.GetComponent<IDamageable>() != null) {

                collision.GetComponent<IDamageable>().OnDamage(PlayerStatus.player.damage);

                PlayPunchSound();

                if (PlayerStatus.player.playerMovement.deltaY != 0) {
                    StartCoroutine(PlayerStatus.player.playerMovement.MiniFreezePlayer());
                }

                SpawnParticle(collision);
            }
        }
    }

    private void SpawnParticle(Collider2D collision) {
        GameObject hitParticleObj = Instantiate(particlePrefab, collision.bounds.center, Quaternion.identity, particleParent);
        hitParticleObj.GetComponentInChildren<ParticleSystem>().Play();

        Destroy(hitParticleObj, 2f);
    }

    public void PlayPunchSound() {
        PlayerStatus.player.audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.PunchHit));
    }
}
