using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpokyDamage : MonoBehaviour
{
    public LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (playerLayer == (playerLayer | 1 << collision.gameObject.layer)) {
            if (collision.GetComponent<IDamageable>() != null) {
                collision.GetComponent<IDamageable>().OnDamage(GetComponentInParent<SpokyEnemy>().damage);
            }
        }
    }
}
