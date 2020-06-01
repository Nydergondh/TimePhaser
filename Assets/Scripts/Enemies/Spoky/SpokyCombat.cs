using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpokyCombat : MonoBehaviour, IDamageable
{

    public bool inSpookRange = false; //is in range to attack
    public bool isSpooking = false; // is attacking
    public bool isSpoked = false; // is hurt (recived damage)

    private HumanoidAnimations _spokyAnim;

    public float attackMaxRange = 0.75f;  // PointA.x = transform.position.x + visionMimRange
    public float attackMimRange = 0.25f;// PointB.x = transform.position.x + visionMaxRange

    public float areaSizeY = -0.5f;// PointA.y = transform.position.y + (areaSizeY/2)
                                  // PointB.y = transform.position.y - (areaSizeY/2)
    private Vector2 _pointA;
    private Vector2 _pointB;

    private Collider2D _playerCollider;
    private SpokyEnemy _spoky;

    public LayerMask playerLayer;

    public float colorChangeTimer = 0;
    public float colorChangeCD = 0.5f;

    public GameObject textDamage;
    // Start is called before the first frame update
    void Start()
    {
        _spoky = GetComponent<SpokyEnemy>();

        _pointA = new Vector2(_spoky.spookyEyes.position.x + attackMimRange, _spoky.spookyEyes.position.y - (areaSizeY / 2));
        _pointB = new Vector2(_spoky.spookyEyes.position.x + attackMaxRange, _spoky.spookyEyes.position.y + (areaSizeY / 2));

        _spokyAnim = GetComponent<HumanoidAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spoky.health > 0) {

            _pointA = new Vector2(_spoky.spookyEyes.position.x + attackMimRange, _spoky.spookyEyes.position.y - (areaSizeY / 2));
            _pointB = new Vector2(_spoky.spookyEyes.position.x + attackMaxRange, _spoky.spookyEyes.position.y + (areaSizeY / 2));

            FlipPoints();

            CanAttackPlayer();

            if (inSpookRange) {
                SetAttackAnim();
            }
        }
    }

    private void CanAttackPlayer() {
        if (_playerCollider = Physics2D.OverlapArea(_pointA, _pointB, playerLayer)) { 
            inSpookRange = true;
            //_playerCollider.GetComponent<PlayerCombat>().OnDamage(_spoky.damage);
        }
        else {
            inSpookRange = false;
        }
    }

    private void FlipPoints() {
        if (transform.localScale.x == -1 && attackMaxRange == Mathf.Abs(attackMaxRange)) {
            attackMaxRange *= -1;
            attackMimRange *= -1;
        }
        else if (transform.localScale.x == 1 && attackMaxRange != Mathf.Abs(attackMaxRange)) {
            attackMaxRange = Mathf.Abs(attackMaxRange);
            attackMimRange = Mathf.Abs(attackMimRange);
        }
    }

    public void OnDamage(int damage) {
        GameObject damagePopUp;
        float rand;

        rand = Random.Range(0.8f, 1.2f);
        damage = (int)(rand * damage);

        if (_spoky.health > 0) {
            _spoky.health -= damage;
            if (_spoky.health > 0) {
                if (!isSpooking) {
                    isSpoked = true;
                }
                if (colorChangeTimer <= 0) {
                    StartCoroutine(ChangeColor());
                }
            }
            else {
                Destroy(gameObject,3f);
            }

            rand = Random.Range(0.8f, 1.2f);
            damage = (int)(rand * damage);

            damagePopUp = Instantiate(textDamage, _spoky.spookyEyes.position, Quaternion.identity, InstaciatedObjects.fatherReference.transform);
            damagePopUp.GetComponent<TextMeshPro>().text = damage.ToString();

            _spokyAnim.SetHit(true, _spoky.health);

            if (_spoky.health > 0) {
                if (_spoky.affectedTime) {
                    _spoky.audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.EnemyHurt));
                }
            }
            else { //equal to other condition but I may change it later
                if(_spoky.affectedTime) {
                    _spoky.audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.EnemyHurt));
                }
            }
        }
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

    public void PlayAttackSound() {
        _spoky.audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.EnemyAttack));
    }


    public IEnumerator ChangeColor() {

        int i = 0;
        float aux = 0;

        while (colorChangeTimer < colorChangeCD) {
            colorChangeTimer += Time.deltaTime;
            _spoky._renderer.material.SetFloat("_Hit", 1);

            i++;
            aux += Time.deltaTime;

            if (colorChangeTimer >= colorChangeCD) {
                colorChangeTimer = colorChangeCD;
            }

            yield return null;
        }

        while (colorChangeTimer > 0) {
            colorChangeTimer -= Time.deltaTime;
            _spoky._renderer.material.SetFloat("_Hit", colorChangeTimer);

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
            Vector3 pointB = new Vector2(_pointA.x, _spoky.spookyEyes.position.y + (areaSizeY / 2));

            Gizmos.DrawLine(pointA, pointB);

            pointA = pointB;
            pointB = _pointB;

            Gizmos.DrawLine(pointA, pointB);

            pointA = _pointB;
            pointB = new Vector2(_pointB.x, _spoky.spookyEyes.position.y - (areaSizeY / 2));

            Gizmos.DrawLine(pointA, pointB);

            pointA = pointB;
            pointB = _pointA;

            Gizmos.DrawLine(pointA, pointB);
        }
        catch {

        }
        
    }
}
