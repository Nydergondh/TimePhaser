﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss boss;

    public Transform core;

    public BossCombat bossCombat;
    public BossMovement bossMovement;

    public SpriteRenderer _renderer;

    public int health = 500;
    public bool figthStarted = false;

    private Animator _anim;

    public delegate void AttUI(int damage);
    public AttUI attUI;

    private void Awake() {
        if (boss != null) {
            Destroy(gameObject);
        }
        else {
            boss = this;
        }
    }

    void Start() 
    {
        _anim = GetComponent<Animator>();
        bossCombat = GetComponent<BossCombat>();
        bossMovement = GetComponent<BossMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (figthStarted) {
            bossCombat.Combat();
            bossMovement.Movement();
        }
    }

    public void SetScreaming(bool value) {
        _anim.SetBool("isScreaming", value);
    }

    public IEnumerator BossDeath() {
        _anim.SetBool("Alive", false);
        yield return new WaitForSeconds(2.5f);
        DestroyBoss();
    }

    public void DestroyBoss() {
        Transform[] herarchyTransforms;
        Rigidbody2D rb;
        float xForce, yForce;

        herarchyTransforms = GetComponentsInChildren<Transform>();

        foreach(Transform tr in herarchyTransforms) {
            if(tr.TryGetComponent<Rigidbody2D>(out rb)) {
                rb.simulated = true;

                xForce = Random.Range(-5f, 5f);
                yForce = Random.Range(2.5f, 5f);
                rb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);

                tr.DetachChildren();
            }
        }

        transform.DetachChildren();
        Destroy(GetComponent<Collider2D>());
        Destroy(_anim);
    }

    public void StartBossFigth() {
        figthStarted = true;
    }

}