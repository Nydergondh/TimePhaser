using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public LayerMask playerLayer;
    public LayerMask groundLayer;

    public GameObject hitPrefab;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _bulletCollider;
    private AudioSource _audioSource;

    private bool _collided= false;
    public bool goingHorizontal = true;
    public bool penetrateWall = false;

    public float movementSpeed = 2f;
    public int damage = 10; 

    private void Start() {

        Destroy(gameObject, 7f);

        _audioSource = GetComponent<AudioSource>();
        _bulletCollider = GetComponent<Collider2D>();
    }

    private void Update() {
        if (!_collided) {
            if (goingHorizontal) { // Going Horizontaly (Direction setted in the enemy)
                transform.position = new Vector3(transform.position.x + (movementSpeed * Time.deltaTime), transform.position.y,transform.position.z);
            }
            else { // Going Downwards
                transform.position = new Vector3(transform.position.x, transform.position.y - (movementSpeed * Time.deltaTime), transform.position.z); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if (playerLayer == (playerLayer | 1 << collision.gameObject.layer)) {
            if (collision.GetComponent<IDamageable>() != null) {
                
                _collided = true; //makes the projectile stop
                collision.GetComponent<IDamageable>().OnDamage(damage); // applys damage

                SpawnParticles(collision);
                _audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.ProjectileCollide)); // plays audio on collision

                UnsetBulletParameters();//disable sprite and collider

                Destroy(gameObject, 0.5f);
            }

        }
        else if (groundLayer == (groundLayer | 1 << collision.gameObject.layer) && !penetrateWall) {
            _collided = true;

            SpawnParticles(collision);
            _audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.ProjectileCollide));

            UnsetBulletParameters();

            Destroy(gameObject,0.5f);
        }

    }

    private void SpawnParticles(Collider2D collision) {
        GameObject hitParticleObj;
        hitParticleObj = Instantiate(hitPrefab, collision.ClosestPoint(transform.position), Quaternion.identity, InstaciatedObjects.fatherReference.transform);
        hitParticleObj.GetComponentInChildren<ParticleSystem>().Play();

        Destroy(hitParticleObj, 2f);
    }
    
    private void UnsetBulletParameters() {

        foreach (Transform obj in transform) {
            obj.gameObject.SetActive(false);
        }

        _bulletCollider.enabled = false;
    }
}
