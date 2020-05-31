using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBossDoor : MonoBehaviour
{
    private Animator _anim;
    public bool open;
    public LayerMask playerLayer;

    private void Start() {
        _anim = GetComponent<Animator>();
        _anim.SetBool("Open", open);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(playerLayer == (playerLayer | 1 << collision.gameObject.layer) && !open) {
            open = true;
            _anim.SetBool("Open", open);
            GetComponent<AudioSource>().PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.Door));
        }
    }

    public void StopPlayingSound() {
        if (GetComponent<AudioSource>() != null) {
            GetComponent<AudioSource>().Stop();
        }
    }
}
