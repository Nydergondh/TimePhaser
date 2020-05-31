using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour, IDamage
{
    public int damage = 10;

    public LayerMask playerLayer;
    public GameObject hitPrefab;
    public Transform particleParent;

    public int GetDamage() {
        return damage;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (playerLayer == (playerLayer | 1 << collision.gameObject.layer)) {
            if (!PlayerStatus.player.playerCombat.GetInvicibility() && PlayerStatus.player.health > 0) {
                if (collision.GetComponent<IDamageable>() != null) {
                    collision.GetComponent<IDamageable>().OnDamage(GetComponentInParent<IDamage>().GetDamage());
                }
                SpawnParticles(collision);
            }
        }
    }

    private void SpawnParticles(Collider2D collision) {
        GameObject hitParticleObj;
        hitParticleObj = Instantiate(hitPrefab, collision.bounds.center, Quaternion.identity, particleParent);
        hitParticleObj.GetComponentInChildren<ParticleSystem>().Play();

        Destroy(hitParticleObj, 2f);
    }

}
