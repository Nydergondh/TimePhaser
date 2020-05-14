using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public LayerMask spokyLayer;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (spokyLayer == (spokyLayer | 1 << collision.gameObject.layer)) {
            if (collision.GetComponent<IDamageable>() != null) {
                collision.GetComponent<IDamageable>().OnDamage(GetComponentInParent<PlayerCombat>().damage);
                //StartCoroutine(PlayerMovement.player.MiniFreezePlayer());
            }
        }
    }

}
