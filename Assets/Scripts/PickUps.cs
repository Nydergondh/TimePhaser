using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerLayer;

    [SerializeField]
    private bool respawnable = false;
    [SerializeField]
    private float respawnTime = 10f;
    private bool isRespawning = false;

    public int pickUpValue;
    public PickUpType pickUp;

    private SpriteRenderer _renderer;

    private void Start() {
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!isRespawning) {
            if (playerLayer == (playerLayer | 1 << collision.gameObject.layer)) {
                if(pickUp == PickUpType.Energy) {
                    if (PlayerStatus.player.energy < PlayerStatus.player.maxEnergy) {

                        PlayerStatus.player.AddEnergy(pickUpValue);
                        if (!respawnable) {
                            Destroy(gameObject);
                        }
                        else {
                            StartCoroutine(Respawn());
                        }

                    }
                }
                else if(pickUp == PickUpType.Health) {
                    if (PlayerStatus.player.health < PlayerStatus.player.maxHealth) {
                        PlayerStatus.player.AddHealth(pickUpValue);
                        if (!respawnable) {
                            Destroy(gameObject);
                        }
                        else {
                            StartCoroutine(Respawn());
                        }
                    }
                }
            }
        }
    }

    private void DisableSprite() {
        _renderer.enabled = false;
    }

    private void EnableSprite() {
        _renderer.enabled = true;
    }

    private IEnumerator Respawn() {
        isRespawning = true;
        DisableSprite();
        yield return new WaitForSeconds(respawnTime);
        EnableSprite();
        isRespawning = false;
    }

    public enum PickUpType {
        Energy,
        Health
    }
}
