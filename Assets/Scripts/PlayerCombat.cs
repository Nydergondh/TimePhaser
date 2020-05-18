using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamageable
{
    private PlayerAnimations _playerAnim;
    public GameObject bubblePrefab;
    public Transform bubbleParent;

    private float _bubbleTimer = 0f;
    public float timeBubbleCD = 4f;

    public int damage = 50;
    public int health = 50;

    [SerializeField]
    private bool _isAttacking = false;
    [SerializeField]
    private bool _isTimeBubbling = false;
    public bool _isHurt = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnim = GetComponent<PlayerAnimations>();
    }

    public void TimeBubbling() {
        if (!_isAttacking && !_isHurt && health > 0) {
            if (Input.GetKeyDown(KeyCode.Q) && _bubbleTimer <= 0) {

                SetPlayerBubble();
                _bubbleTimer = timeBubbleCD;

                PlayerStatus.player.playerMovement.FreezeMovement();
            }

            if (_bubbleTimer > 0) {
                _bubbleTimer -= Time.deltaTime;
            }
        }
    }

    public void Attack() {
        if (!_isTimeBubbling && !_isHurt && health > 0) {

            if (Input.GetMouseButtonDown(0) && !_isAttacking) {
                //if on the ground then stop moving to attack (if on the air then just stop moving if you hit something)
                if (PlayerStatus.player.playerMovement.isTouchingGround) {
                    PlayerStatus.player.playerMovement.FreezeMovement();
                }
                SetPlayerAttack();
            }

        }
    }

    public void InstaciateTimeBubble() {
        Instantiate(bubblePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, bubbleParent);
    }

    public void OnDamage(int damage) {
        //print("GOT HIT! Health " + (health-damage));
        if (health > 0) {
            health -= damage;      

            if (_isAttacking || _isTimeBubbling) {
                print("GOT HIT!");
                UnsetPlayerAttack();
                UnsetPlayerBubble();
                PlayerStatus.player.playerMovement.UnfreezeMovement();
            }
            _isHurt = true;            
           
            _playerAnim.SetHit(true, health); //play the animation
        }
    }

    private void SetPlayerAttack() {
        _playerAnim.SetAttack(true);
        PlayerStatus.player.playerMovement.StopDash();
        _isAttacking = true;
    }

    public void UnsetPlayerAttack() {
        _playerAnim.SetAttack(false);
        _isAttacking = false;
    }


    public bool GetPlayerAttack() {
        return _isAttacking;
    }

    public void SetPlayerBubble() {

        _playerAnim.SetPlayerTimeBubble(true);
        _isTimeBubbling = true;

        PlayerStatus.player.playerMovement.StopDash();
    }

    public void UnsetPlayerBubble() {

        _playerAnim.SetPlayerTimeBubble(false);
        _isTimeBubbling = false;

    }


    public void UnsetHurtAnim() {
        _playerAnim.SetHit(false);
        _isHurt = false;
    }

    public void SetHurtAnim() {
        _playerAnim.SetHit(true);
        _isHurt = true;
    }

    //public IEnumerator ChangeColor() {

    //    int i = 0;
    //    float aux = 0;

    //    while (colorChangeTimer < colorChangeCD) {
    //        colorChangeTimer += Time.deltaTime;
    //        _spoky._renderer.material.SetFloat("_Hit", 1);

    //        i++;
    //        aux += Time.deltaTime;

    //        if (colorChangeTimer >= colorChangeCD) {
    //            colorChangeTimer = colorChangeCD;
    //        }

    //        yield return null;
    //    }

    //    while (colorChangeTimer > 0) {
    //        colorChangeTimer -= Time.deltaTime;
    //        _spoky._renderer.material.SetFloat("_Hit", colorChangeTimer);

    //        i++;
    //        aux += Time.deltaTime;

    //        if (colorChangeTimer <= 0) {
    //            colorChangeTimer = 0;
    //        }

    //        yield return null;
    //    }
    //    print(i + " " + aux);
    //}
}
