using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour, IDamageable
{
    private PlayerAnimations playerAnim;
    public GameObject bubblePrefab;
    public Transform bubbleParent;

    private float bubbleTimer = 0f;
    public float timeBubbleCD = 4f;

    public int damage = 50;

    [SerializeField]
    private bool isAttacking = false;
    [SerializeField]
    private bool isTimeBubbling = false;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking) {
            if (Input.GetKeyDown(KeyCode.Q) && bubbleTimer <= 0) {

                SetPlayerBubble();
                bubbleTimer = timeBubbleCD;

                PlayerStatus.player.playerMovement.FreezeMovement();
            }

            if (bubbleTimer > 0) {
                bubbleTimer -= Time.deltaTime;
            }
        }
        if (!isTimeBubbling) {

            if (Input.GetMouseButtonDown(0) && !isAttacking) {
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
        print("GOT HIT! (Player)");
    }

    private void SetPlayerAttack() {
        playerAnim.SetAttack(true);
        PlayerStatus.player.playerMovement.StopDash();
        isAttacking = true;
    }

    public void UnsetPlayerAttack() {
        playerAnim.SetAttack(false);
        isAttacking = false;
    }


    public bool GetPlayerAttack() {
        return isAttacking;
    }

    public void SetPlayerBubble() {
        playerAnim.SetPlayerTimeBubble(true);
        PlayerStatus.player.playerMovement.StopDash();
        isTimeBubbling = true;
    }

    public void UnsetPlayerBubble() {
        playerAnim.SetPlayerTimeBubble(false);
        isTimeBubbling = false;
    }
}
