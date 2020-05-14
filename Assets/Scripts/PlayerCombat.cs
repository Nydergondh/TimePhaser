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

    [SerializeField]
    private int health = 100;
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
                PlayerMovement.player.FreezeMovement();
            }

            if (bubbleTimer > 0) {
                bubbleTimer -= Time.deltaTime;
            }
        }
        if (!isTimeBubbling) {
            if (Input.GetMouseButtonDown(0) && !isAttacking) {

                //if (PlayerMovement.player.deltaY <= -PlayerMovement.player.minimumDeltaY || PlayerMovement.player.deltaY >= PlayerMovement.player.minimumDeltaY) {

                //}
                SetPlayerAttack();
                PlayerMovement.player.FreezeMovement();
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
        isAttacking = true;
    }

    public void UnsetPlayerAttack() {
        playerAnim.SetAttack(false);
        isAttacking = false;
    }

    public void SetPlayerBubble() {
        playerAnim.SetPlayerTimeBubble(true);
        isTimeBubbling = true;
    }

    public void UnsetPlayerBubble() {
        playerAnim.SetPlayerTimeBubble(false);
        isTimeBubbling = false;
    }
}
