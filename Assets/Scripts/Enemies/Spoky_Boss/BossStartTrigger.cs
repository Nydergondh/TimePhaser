using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class BossStartTrigger : MonoBehaviour
{
    private bool start = false;
    public LayerMask playerLayer;
    public CinemachineVirtualCamera cinemachineVirtual;
    public GameObject bossUI;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (playerLayer == (playerLayer | 1 << collision.gameObject.layer) && !start) {
            start = true;
            Boss.boss.StartBossFigth();

            cinemachineVirtual.Follow = PlayerStatus.player.followTransform;
            cinemachineVirtual.Priority = 20;
            bossUI.SetActive(true);
        }
    }

}
