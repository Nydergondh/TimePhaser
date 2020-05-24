using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerLayer;

    public int pickUpValue;

    public PickUpType pickUp;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (playerLayer == (playerLayer | 1 << collision.gameObject.layer)) {
            if(pickUp == PickUpType.Energy) {
                if (PlayerStatus.player.energy < PlayerStatus.player.maxEnergy) {
                    PlayerStatus.player.AddEnergy(pickUpValue);
                    Destroy(gameObject);
                }
            }
            else if(pickUp == PickUpType.Health) {
                if (PlayerStatus.player.health < PlayerStatus.player.maxHealth) {
                    PlayerStatus.player.AddHealth(pickUpValue);
                    Destroy(gameObject);
                }
            }
        }
    }

    public enum PickUpType {
        Energy,
        Health
    }
}
