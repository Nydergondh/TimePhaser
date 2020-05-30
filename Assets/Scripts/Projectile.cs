using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public LayerMask playerLayer;
    public LayerMask groundLayer;

    public GameObject hitPrefab;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _bulletCollider;

    public bool goingHorizontal = true;
    public bool penetrateWall = false;

    public float movementSpeed = 2f;
    public int damage = 10; 

    private void Start() {

        if (penetrateWall) {
            Destroy(gameObject, 7f);
        }

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _bulletCollider = GetComponent<Collider2D>();
    }

    private void Update() {
        if (goingHorizontal) { // Going Horizontaly (Direction setted in the enemy)
            transform.position = new Vector3(transform.position.x + (movementSpeed * Time.deltaTime), transform.position.y,transform.position.z);
        }
        else { // Going Downwards
            transform.position = new Vector3(transform.position.x, transform.position.y - (movementSpeed * Time.deltaTime), transform.position.z); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (playerLayer == (playerLayer | 1 << collision.gameObject.layer)) {
            if (collision.GetComponent<IDamageable>() != null) {
                collision.GetComponent<IDamageable>().OnDamage(damage);
            }

            SpawnParticles(collision);
            //UnsetBulletParameters();
            Destroy(gameObject);
        }
        else if (groundLayer == (groundLayer | 1 << collision.gameObject.layer) && !penetrateWall) {
            SpawnParticles(collision);
            Destroy(gameObject);
        }
    }

    private void SpawnParticles(Collider2D collision) {
        GameObject hitParticleObj;
        hitParticleObj = Instantiate(hitPrefab, collision.ClosestPoint(transform.position), Quaternion.identity, InstaciatedObjects.fatherReference.transform);
        hitParticleObj.GetComponentInChildren<ParticleSystem>().Play();

        Destroy(hitParticleObj, 2f);
    }
    
    private void UnsetBulletParameters() {
        _spriteRenderer.enabled = false;
        _bulletCollider.enabled = false;
    }
}
