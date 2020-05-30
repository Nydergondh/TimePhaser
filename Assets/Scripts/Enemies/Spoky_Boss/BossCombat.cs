using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossCombat : MonoBehaviour, IDamageable
{
    public int bossStage;
    public bool isAttacking;

    public GameObject bossProjectile;
    public GameObject textDamage;

    private HumanoidAnimations _bossAnim;

    [SerializeField]
    private float _timeToWait = 5f;
    private float _currentWatingTime;

    [SerializeField]
    private float _attackCd = 0.25f;
    private float _curretnAttackTime;

    public AttackType attackType;

    public float colorChangeTimer = 0;
    public float colorChangeCD = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _bossAnim = GetComponent<HumanoidAnimations>();
        _currentWatingTime = _timeToWait;
    }

    public void Combat() {
        if (isAttacking && Boss.boss.health > 0) {
            if (_currentWatingTime > 0) {
                _currentWatingTime -= Time.deltaTime;
                DoAttack();
            }
            else if (_currentWatingTime <= 0) {
                isAttacking = false;
                _curretnAttackTime = _attackCd;
                _currentWatingTime = _timeToWait;

                Boss.boss.bossMovement.moving = true;
                Boss.boss.bossMovement.SelectMove();
            }
        }
    }

    public void DoAttack() {

        if (_curretnAttackTime > 0) {
            _curretnAttackTime -= Time.deltaTime;
        }
        else if (_curretnAttackTime <= 0) {
            CreateProjectile();
            _curretnAttackTime = _attackCd;
        }

    }

    private void CreateProjectile() {

        Transform[] spawnPoints;
        GameObject projectile;
        int spawn;

        if (attackType == AttackType.Rigth) {
            spawnPoints = Boss.boss.bossMovement.rigthTransform.GetComponentsInChildren<Transform>();
            spawn = Random.Range(0, spawnPoints.Length);

            projectile = Instantiate(bossProjectile, spawnPoints[spawn].position, Quaternion.identity, InstaciatedObjects.fatherReference.transform);
            projectile.GetComponent<Projectile>().movementSpeed *= -1;
            projectile.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }

        else if (attackType == AttackType.Up) {
            spawnPoints = Boss.boss.bossMovement.upTransform.GetComponentsInChildren<Transform>();
            spawn = Random.Range(0, spawnPoints.Length);

            projectile = Instantiate(bossProjectile, spawnPoints[spawn].position, Quaternion.identity, InstaciatedObjects.fatherReference.transform);
            projectile.GetComponent<Projectile>().goingHorizontal = false;
        }

        else if (attackType == AttackType.Left) {
            spawnPoints = Boss.boss.bossMovement.leftTransform.GetComponentsInChildren<Transform>();
            spawn = Random.Range(0, spawnPoints.Length);

            projectile = Instantiate(bossProjectile, spawnPoints[spawn].position, Quaternion.identity, InstaciatedObjects.fatherReference.transform);
        }

    }

    public void OnDamage(int damage) {
        GameObject damagePopUp;
        float rand;

        if (Boss.boss.figthStarted) {
            rand = Random.Range(0.8f, 1.2f);
            damage = (int)(rand * damage);

            if (Boss.boss.health > 0) {
                Boss.boss.health -= damage;
                if (Boss.boss.health > 0) {
                    if (colorChangeTimer <= 0) {
                        StartCoroutine(ChangeColor());
                    }
                }
                _bossAnim.SetHit(true, Boss.boss.health);

            }

            damagePopUp = Instantiate(textDamage, Boss.boss.core.position, Quaternion.identity, InstaciatedObjects.fatherReference.transform);
            damagePopUp.GetComponent<TextMeshPro>().text = damage.ToString();

            Boss.boss.attUI?.Invoke(Boss.boss.health);
        }
    }

    public IEnumerator ChangeColor() {

        int i = 0;
        float aux = 0;

        while (colorChangeTimer < colorChangeCD) {
            colorChangeTimer += Time.deltaTime;
            //Boss.boss._renderer.material.SetFloat("_Hit", 1);

            i++;
            aux += Time.deltaTime;

            if (colorChangeTimer >= colorChangeCD) {
                colorChangeTimer = colorChangeCD;
            }

            yield return null;
        }

        while (colorChangeTimer > 0) {
            colorChangeTimer -= Time.deltaTime;
            //Boss.boss._renderer.material.SetFloat("_Hit", colorChangeTimer);

            i++;
            aux += Time.deltaTime;

            if (colorChangeTimer <= 0) {
                colorChangeTimer = 0;
            }

            yield return null;
        }
    }

    public enum AttackType {
        Rigth,
        Left,
        Up
    }
}
