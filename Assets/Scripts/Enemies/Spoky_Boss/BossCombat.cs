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

        if (GetBossHealthPer() <= 0.25f) {
            attackType = AttackType.All;
        }

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
        else if(attackType == AttackType.All) {
            int rand = 3;
            int result;

            result =Random.Range(0,rand);
            
            switch (result) {
                case 0:
                    spawnPoints = Boss.boss.bossMovement.leftTransform.GetComponentsInChildren<Transform>();
                    spawn = Random.Range(0, spawnPoints.Length);

                    projectile = Instantiate(bossProjectile, spawnPoints[spawn].position, Quaternion.identity, InstaciatedObjects.fatherReference.transform);
                    break;

                case 1:
                    spawnPoints = Boss.boss.bossMovement.upTransform.GetComponentsInChildren<Transform>();
                    spawn = Random.Range(0, spawnPoints.Length);

                    projectile = Instantiate(bossProjectile, spawnPoints[spawn].position, Quaternion.identity, InstaciatedObjects.fatherReference.transform);
                    projectile.GetComponent<Projectile>().goingHorizontal = false;
                    break;

                case 2:
                    spawnPoints = Boss.boss.bossMovement.rigthTransform.GetComponentsInChildren<Transform>();
                    spawn = Random.Range(0, spawnPoints.Length);

                    projectile = Instantiate(bossProjectile, spawnPoints[spawn].position, Quaternion.identity, InstaciatedObjects.fatherReference.transform);
                    projectile.GetComponent<Projectile>().movementSpeed *= -1;
                    projectile.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                    break;
            }
            
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
                AttBossPatern();

                if (Boss.boss.health > 0) {

                    StopCoroutine(ChangeColor()); //if hitted again while doing the change color coroutine stop it and start a new one
                    colorChangeTimer = colorChangeCD;

                    StartCoroutine(ChangeColor());

                }
                _bossAnim.SetHit(true, Boss.boss.health);

            }

            damagePopUp = Instantiate(textDamage, Boss.boss.core.position, Quaternion.identity, InstaciatedObjects.fatherReference.transform);
            damagePopUp.GetComponent<TextMeshPro>().text = damage.ToString();

            Boss.boss.attUI?.Invoke(Boss.boss.health);
        }
    }

    private void AttBossPatern() {
        float aux, max, current;
        max = (float)Boss.boss.maxHealth;
        current = (float)Boss.boss.health;

        aux = (current / max);
        if (aux <= 0.75 && aux > 0.50) {
            _attackCd = 0.2f;
        }
        else if (aux <= 0.50 && aux > 0.25) {
            _attackCd = 0.1f;
        }
        else if (aux <= 0.25 && aux > 0f) {
            _attackCd = 0.1f;
        }
    }

    private float GetBossHealthPer() {
        float aux, max, current;

        max = (float)Boss.boss.maxHealth;
        current = (float)Boss.boss.health;
        aux = (current / max);

        return aux;
    }

    public IEnumerator ChangeColor() {

        int i = 0;
        float aux = 0;

        while (colorChangeTimer < colorChangeCD) {
            colorChangeTimer += Time.deltaTime;
            Boss.boss._renderer.material.SetFloat("_Hit", 1);

            i++;
            aux += Time.deltaTime;

            if (colorChangeTimer >= colorChangeCD) {
                colorChangeTimer = colorChangeCD;
            }

            yield return null;
        }

        while (colorChangeTimer > 0) {
            colorChangeTimer -= Time.deltaTime;
            Boss.boss._renderer.material.SetFloat("_Hit", colorChangeTimer);

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
        Up,
        All
    }
}
