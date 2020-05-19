using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public LayerMask playerLayer;
    public GameObject hitPrefab;
    public Transform particleParent;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _bulletCollider;

    public float movementSpeed = 2f;
    public int damage = 10; 

    private void Start() {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _bulletCollider = GetComponent<Collider2D>();
    }

    private void Update() {
        transform.position = new Vector3(transform.position.x + (movementSpeed * Time.deltaTime), transform.position.y,transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (playerLayer == (playerLayer | 1 << collision.gameObject.layer)) {
            if (collision.GetComponent<IDamageable>() != null) {
                collision.GetComponent<IDamageable>().OnDamage(damage);
            }

            SpawnParticles(collision);

            UnsetBulletParameters();
            Destroy(this, 1f);
        }
    }

    private void SpawnParticles(Collider2D collision) {
        GameObject hitParticleObj;
        hitParticleObj = Instantiate(hitPrefab, collision.bounds.center, Quaternion.identity, particleParent);
        hitParticleObj.GetComponentInChildren<ParticleSystem>().Play();

        Destroy(hitParticleObj, 2f);
    }
    
    private void UnsetBulletParameters() {
        _spriteRenderer.enabled = false;
        _bulletCollider.enabled = false;
    }
}
