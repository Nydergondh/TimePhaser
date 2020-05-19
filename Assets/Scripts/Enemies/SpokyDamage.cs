using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpokyDamage : MonoBehaviour
{
    public LayerMask playerLayer;
    public GameObject hitPrefab;
    public Transform particleParent;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (playerLayer == (playerLayer | 1 << collision.gameObject.layer)) {
            if (collision.GetComponent<IDamageable>() != null) {
                collision.GetComponent<IDamageable>().OnDamage(GetComponentInParent<SpokyEnemy>().damage);
            }

            SpawnParticles(collision);
        }
    }

    private void SpawnParticles(Collider2D collision) {
        GameObject hitParticleObj;
        hitParticleObj = Instantiate(hitPrefab, collision.bounds.center, Quaternion.identity, particleParent);
        hitParticleObj.GetComponentInChildren<ParticleSystem>().Play();

        Destroy(hitParticleObj, 2f);
    }
}
