using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpokeyShooterCombat : MonoBehaviour, IDamageable
{
    public bool inSpookRange = false; //is in range to attack
    public bool isSpooking = false; // is attacking
    public bool isSpoked = false; // is hurt (recived damage)

    private HumanoidAnimations _spokyAnim;

    public float attackRange = 4f;

    public float areaSizeY = -0.5f;// PointA.y = transform.position.y + (areaSizeY/2)
                                   // PointB.y = transform.position.y - (areaSizeY/2)
    private Vector2 _pointA;
    private Vector2 _pointB;

    private Collider2D _playerCollider;
    private SpokeyShooterEnemy _spokey;

    public LayerMask playerLayer;

    public GameObject projectilePrefab;
    public GameObject textDamage;

    public float colorChangeTimer = 0;
    public float colorChangeCD = 0.5f;

    // Start is called before the first frame update
    void Start() {
        _spokey = GetComponent<SpokeyShooterEnemy>();

        _pointA = new Vector2(_spokey.spookyEyes.position.x - (attackRange / 2), _spokey.spookyEyes.position.y - (areaSizeY / 2));
        _pointB = new Vector2(_spokey.spookyEyes.position.x + (attackRange / 2), _spokey.spookyEyes.position.y + (areaSizeY / 2));

        _spokyAnim = GetComponent<HumanoidAnimations>();
    }

    // Update is called once per frame
    void Update() {
        if (_spokey.health > 0) {

            _pointA = new Vector2(_spokey.spookyEyes.position.x - (attackRange / 2), _spokey.spookyEyes.position.y - (areaSizeY / 2));
            _pointB = new Vector2(_spokey.spookyEyes.position.x + (attackRange / 2), _spokey.spookyEyes.position.y + (areaSizeY / 2));

            CanAttackPlayer();

            if (inSpookRange && !isSpooking) {
                _spokey.spokeyMovement.ChangeDirection(PlayerStatus.player.transform.position);
                SetAttackAnim();
            }
        }
    }

    private void CanAttackPlayer() {
        if (_playerCollider = Physics2D.OverlapArea(_pointA, _pointB, playerLayer)) {
            inSpookRange = true;
        }
        else {
            inSpookRange = false;
        }
    }

    public void OnDamage(int damage) {
        GameObject damagePopUp;
        float rand;

        rand = Random.Range(0.8f, 1.2f);
        damage = (int)(rand * damage);

        if (_spokey.health > 0) {
            _spokey.health -= damage;
            if (_spokey.health > 0) {
                if (!isSpooking) {
                    isSpoked = true;
                }
                if (colorChangeTimer <= 0) {
                    StartCoroutine(ChangeColor());
                }
            }
            else {
                Destroy(gameObject, 3f);
            }
            _spokyAnim.SetHit(true, _spokey.health);

            damagePopUp = Instantiate(textDamage, _spokey.spookyEyes.position, Quaternion.identity, InstaciatedObjects.fatherReference.transform);
            damagePopUp.GetComponent<TextMeshPro>().text = damage.ToString();

            if (_spokey.health > 0) {
                _spokey.audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.EnemyHurt));
            }
            else {
                StopAttackSound();
                _spokey.audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.EnemyHurt));
            }
        }
    }


    public void PlayAttackSound() {
        //if (!_spokey.audioSource.isPlaying) {
            _spokey.audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.EnemyFire));
        //}
    }
    public void StopAttackSound() {
        _spokey.audioSource.Stop();
    }


    public void SetAttackAnim() {
        _spokyAnim.SetAttack(true);
        isSpooking = true;
    }


    public void UnsetAttackAnim() {
        _spokyAnim.SetAttack(false);
        isSpooking = false;
    }

    public void UnsetHurtAnim() {
        _spokyAnim.SetHit(false);
        isSpoked = false;
    }

    public void CreateProjectile() {

        GameObject projectile;
        projectile = Instantiate(projectilePrefab, _spokey.bulletSpawnPoint.position, Quaternion.identity, InstaciatedObjects.fatherReference.transform);

        if (transform.localScale.x < 0) {
            projectile.GetComponent<Projectile>().movementSpeed *= -1;
            projectile.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }

    }

    public IEnumerator ChangeColor() {

        int i = 0;
        float aux = 0;

        while (colorChangeTimer < colorChangeCD) {
            colorChangeTimer += Time.deltaTime;
            _spokey._renderer.material.SetFloat("_Hit", 1);

            i++;
            aux += Time.deltaTime;

            if (colorChangeTimer >= colorChangeCD) {
                colorChangeTimer = colorChangeCD;
            }

            yield return null;
        }

        while (colorChangeTimer > 0) {
            colorChangeTimer -= Time.deltaTime;
            _spokey._renderer.material.SetFloat("_Hit", colorChangeTimer);

            i++;
            aux += Time.deltaTime;

            if (colorChangeTimer <= 0) {
                colorChangeTimer = 0;
            }

            yield return null;
        }
    }

    private void OnDrawGizmosSelected() {

        try {
            Gizmos.color = Color.red;

            Vector3 pointA = _pointA;
            Vector3 pointB = new Vector2(_pointA.x, _spokey.spookyEyes.position.y + (areaSizeY / 2));

            Gizmos.DrawLine(pointA, pointB);

            pointA = pointB;
            pointB = _pointB;

            Gizmos.DrawLine(pointA, pointB);

            pointA = _pointB;
            pointB = new Vector2(_pointB.x, _spokey.spookyEyes.position.y - (areaSizeY / 2));

            Gizmos.DrawLine(pointA, pointB);

            pointA = pointB;
            pointB = _pointA;

            Gizmos.DrawLine(pointA, pointB);
        }
        catch {

        }

    }
}
