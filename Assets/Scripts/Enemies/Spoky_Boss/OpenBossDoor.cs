using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBossDoor : MonoBehaviour
{
    private Animator _anim;
    private bool open = false;
    public LayerMask playerLayer;

    private void Start() {
        _anim = GetComponent<Animator>();   
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(playerLayer == (playerLayer | 1 << collision.gameObject.layer) && !open) {
            open = true;
            _anim.SetBool("Open", open);
        }
    }

    
}
