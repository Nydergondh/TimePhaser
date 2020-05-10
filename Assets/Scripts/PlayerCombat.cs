using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerAnimations playerAnim;
    public GameObject bubblePrefab;
    public Transform bubbleParent;

    public float timeBubbleCD = 4f;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && timeBubbleCD <= 0) {

            playerAnim.SetPlayerTimeBubble(true);

            timeBubbleCD = 4f;
            PlayerMovement.player.FreezeMovement();
        }

        if (timeBubbleCD > 0) {
            timeBubbleCD -= Time.deltaTime;
        }
    }

    public void InstaciateTimeBubble() {
        Instantiate(bubblePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, bubbleParent);
    }

    public void UnsetPlayerBubble() {
        playerAnim.SetPlayerTimeBubble(false);
    }

    public void SetPlayerBubble() {
        playerAnim.SetPlayerTimeBubble(true);
    }
}
