using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCombat : MonoBehaviour, IDamageable
{
    private PlayerAnimations _playerAnim;
    public GameObject bubblePrefab;
    public Transform bubbleParent;

    public GameObject textDamage;

    private float _bubbleTimer = 0f;
    public float timeBubbleCD = 4f;

    [SerializeField]
    private bool _isAttacking = false;
    [SerializeField]
    private bool _isTimeBubbling = false;
    public bool _isHurt = false;
    private bool _isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnim = GetComponent<PlayerAnimations>();
    }

    public void TimeBubbling() {
        if (!_isAttacking && !_isHurt && PlayerStatus.player.health > 0) {
            if (Input.GetKeyDown(KeyCode.Q) && _bubbleTimer <= 0 && PlayerStatus.player.energy > 0) {

                SetPlayerBubble();
                _bubbleTimer = timeBubbleCD;

                PlayerStatus.player.playerMovement.FreezeMovement();
            }

            if (_bubbleTimer > 0) {
                _bubbleTimer -= Time.deltaTime;
            }
        }
    }

    public void Attack() {
        if (!_isTimeBubbling && !_isHurt && PlayerStatus.player.health > 0) {
            if (Input.GetMouseButtonDown(0) && !_isAttacking) {
                //if on the ground then stop moving to attack (if on the air then just stop moving if you hit something)
                if (PlayerStatus.player.playerMovement.isTouchingGround) {
                    PlayerStatus.player.playerMovement.FreezeMovement();
                }
                SetPlayerAttack();
            }
        }
    }

    public void InstaciateTimeBubble() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 desiredPos = new Vector3(mousePos.x, mousePos.y, 0);

        Instantiate(bubblePrefab, desiredPos, Quaternion.identity, bubbleParent);
    }

    public void SetInvincible(bool value) {
        _isInvincible = value;
    }

    public void OnDamage(int damage) {

        float rand;
        GameObject damagePopUp;

        if (PlayerStatus.player.health > 0) { //kinda shitty logic here (test health twice) Too bad :/

            if (!_isInvincible) {
                rand = Random.Range(0.8f, 1.2f);
                damage = (int)(rand * damage);

                if (PlayerStatus.player.health > 0) {
                    PlayerStatus.player.health -= damage;

                    if (_isAttacking || _isTimeBubbling) {
                        UnsetPlayerAttack();
                        UnsetPlayerBubble();
                        PlayerStatus.player.playerMovement.UnfreezeMovement();
                    }
                    _isHurt = true;

                    _playerAnim.SetHit(true, PlayerStatus.player.health);

                    damagePopUp = Instantiate(textDamage, PlayerStatus.player.damageUISpawnPoint.position, Quaternion.identity, InstaciatedObjects.fatherReference.transform);
                    damagePopUp.GetComponent<TextMeshPro>().text = damage.ToString();
                    //add invicibility frames
                    StartCoroutine(InivisibilityFrames());
                    //update UI
                    PlayerStatus.player.attUI?.Invoke(PlayerStatus.player.health, UISliderController.SliderType.Health);
                    //play sound
                    if (PlayerStatus.player.health > 0) {
                        PlayerStatus.player.audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.Hurt));
                    }
                    else {
                        PlayerStatus.player.audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.Death));
                    }
                }
                else {
                    damagePopUp = Instantiate(textDamage, PlayerStatus.player.damageUISpawnPoint.position, Quaternion.identity, InstaciatedObjects.fatherReference.transform);
                    damagePopUp.GetComponent<TextMeshPro>().text = damage.ToString();

                    PlayerStatus.player.attUI?.Invoke(0, UISliderController.SliderType.Health);
                }

            }
        }
    }

    public void PlayWooshSound() {
        PlayerStatus.player.audioSource.PlayOneShot(SoundManager.GetSound(SoundAudios.Sound.PunchWoosh));
    }

    private void SetPlayerAttack() {

        _playerAnim.SetAttack(true);
        _isAttacking = true;

        PlayerStatus.player.playerMovement.StopDash();
    }

    public void UnsetPlayerAttack() {
        _playerAnim.SetAttack(false);
        _isAttacking = false;
    }


    public bool GetPlayerAttack() {
        return _isAttacking;
    }

    public void SetPlayerBubble() {

        _playerAnim.SetPlayerTimeBubble(true);
        _isTimeBubbling = true;

        PlayerStatus.player.playerMovement.StopDash();
    }

    public void UnsetPlayerBubble() {
        _playerAnim.SetPlayerTimeBubble(false);
        _isTimeBubbling = false;
    }


    public void UnsetHurtAnim() {
        _playerAnim.SetHit(false);
        _isHurt = false;
    }

    public void SetHurtAnim() {
        _playerAnim.SetHit(true);
        _isHurt = true;
    }

    public bool GetInvicibility() {
        return _isInvincible;
    }

    public IEnumerator InivisibilityFrames() {
        _isInvincible = true;
        yield return new WaitForSeconds(1f);
        _isInvincible = false;
    }

}
