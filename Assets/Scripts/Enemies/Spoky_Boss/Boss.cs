using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss boss;

    public Transform core;

    [HideInInspector] public BossCombat bossCombat;
    [HideInInspector] public BossMovement bossMovement;

    public SpriteRenderer _renderer;
    public GameObject bossUI;

    public int maxHealth = 500;
    public int health = 500;
    public bool figthStarted = false;

    [HideInInspector] public AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (figthStarted && health > 0) {
            bossCombat.Combat();
            bossMovement.Movement();
        }
    }

    public void SetScreaming(bool value) {
        _anim.SetBool("isScreaming", value);
    }

    public IEnumerator BossDeath() {
        _anim.SetBool("Alive", false);
        PlayerStatus.player.playerCombat.SetInvincible(true);
        yield return new WaitForSeconds(2.5f);
        DestroyBoss();
        yield return new WaitForSeconds(10f);
        GameManager.gameManager.UI.GetComponent<Animator>().SetBool("EndGame", true);
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
        PlayExplosionSound();
        bossUI.SetActive(false);
        Destroy(_anim);

        GameManager.gameManager.endGame = true;
    }

    public void StartBossFigth() {
        figthStarted = true;
    }

    public void PlayScreamSound() {
        audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.BossScream));
    }

    public void PlayExplosionSound() {
        audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.BossExplode));
    }
}
