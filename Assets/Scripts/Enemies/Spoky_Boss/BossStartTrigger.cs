using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class BossStartTrigger : MonoBehaviour
{
    private bool start = false;

    public LayerMask playerLayer;

    public CinemachineVirtualCamera cinemachineVirtual;
    public OpenBossDoor bossCloseDoor;

    public AudioClip mainMusic;
    public AudioClip bossMusic;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (playerLayer == (playerLayer | 1 << collision.gameObject.layer) && !start) {
            start = true;
            bossCloseDoor.open = false;
            bossCloseDoor.GetComponent<Animator>().SetBool("Open",bossCloseDoor.open);

            Boss.boss.StartBossFigth();

            cinemachineVirtual.Follow = PlayerStatus.player.followTransform;
            cinemachineVirtual.Priority = 20;
            Boss.boss.bossUI.SetActive(true);

            Camera.main.GetComponentInChildren<AudioSource>().clip = bossMusic;
            Camera.main.GetComponentInChildren<AudioSource>().Play();
        }
    }

}
