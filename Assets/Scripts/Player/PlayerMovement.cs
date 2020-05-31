using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float runSpeed = 3.5f;
    public float dashSpeed = 5f;
    public float jumpSpeed = 5f;
    public float airSpeed = 5f;

    private bool _jumping;
    private bool _freezeMovement;

    private bool _dashing;
    private float _dashCD = 1f;
    private float _dashTimer = 0f;
    private float _dashMaxTime = 0.25f;

    public float deltaX;

    public float minimumDeltaY = 0.01f;
    public float deltaY;  

    public bool isTouchingGround;

    public LayerMask groundLayer;

    private SpriteRenderer _playerRenderer;
    public new Rigidbody2D rigidbody { get; set; }
    public BoxCollider2D feetCollider;

    private PlayerAnimations playerAnim;

    private ContactFilter2D colFilter;
    private Collider2D[] results; 

    // Start is called before the first frame update
    void Start()
    {
        isTouchingGround = true;

        playerAnim = GetComponent<PlayerAnimations>();
        _playerRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();

        results = new Collider2D[1];
        colFilter = new ContactFilter2D();

        colFilter.SetLayerMask(groundLayer);
    }

    public void Movement() {
        Vector2 vel;
        //on ground
        if (!_freezeMovement && PlayerStatus.player.health > 0 && !PlayerStatus.player.playerCombat._isHurt) {
            #region Horizontal Movement
            //stop X movement Input if attacking
            if (!PlayerStatus.player.playerCombat.GetPlayerAttack()) {
                //get the dash input and apply force
                if (Input.GetButtonDown("Dash") && !_dashing && _dashTimer <= 0) {
                    
                    deltaX = dashSpeed * transform.localScale.x;
                    rigidbody.gravityScale = 0;
                    _dashing = true;
                    _jumping = false;

                }
                //dashing
                else if (_dashTimer < _dashMaxTime && _dashing) {
                    _dashTimer += Time.deltaTime;
                    deltaY = 0;
                    if (_dashTimer >= _dashMaxTime) {
                        _dashing = false;
                        rigidbody.gravityScale = 1;
                        StartCoroutine(StartDashCD());
                    }
                
                }
                //walking
                else if (Input.GetButton("Horizontal") && deltaY == 0 && !_dashing) {
                    deltaX = Input.GetAxis("Horizontal") * runSpeed;
                }
                //on air
                else if (Input.GetButton("Horizontal") && deltaY != 0 && !_dashing) {
                    deltaX = Input.GetAxis("Horizontal") * airSpeed;
                }
                //totaly still (used getaxis so that it conservs a little bit of momentun)
                else {
                    deltaX = Input.GetAxis("Horizontal");
                }
            }
            //if on the ground Stop X input
            else if(deltaY == 0 && isTouchingGround) {
                //print("GotHere2");
                deltaX = 0;
            }
            ////else mantain XSpeed momentum
            //else if(deltaY != 0) {
            //    print("GotHere3");
            //    //deltaX = airSpeed * transform.localScale.x;
            //}
            #endregion
            //if is not dashing then can jump
            if (!_dashing) {
                #region Vertical Movement
                SetGrounded();

                //Jump if on Ground
                if (Input.GetButtonDown("Jump") && isTouchingGround) {
                    deltaY = jumpSpeed;
                    Jump();
                }
                //if was on thin platform
                else if (deltaY <= 0 && PlayerStatus.player.playerGround.isFalling) {
                    deltaY = rigidbody.velocity.y;
                }
                //Is On Air
                else if ((deltaY > 0 || deltaY < 0)) {

                    //Is falling and DID came from a jump state
                    if (rigidbody.velocity.y < minimumDeltaY && !isTouchingGround) {
                        _jumping = false;
                        deltaY = rigidbody.velocity.y;
                    }
                    //if is falling and hitted the ground
                    else if (rigidbody.velocity.y < minimumDeltaY && isTouchingGround) {
                        _jumping = false;
                        deltaY = 0;
                    }
                    else {
                        deltaY = rigidbody.velocity.y;
                    }
                }
                //if stoped dashing to attack
                else if (deltaY == 0 && !isTouchingGround) {
                    deltaY = rigidbody.velocity.y;
                }
                //on Ground and not Jumping
                else {
                    deltaY = 0;
                }
                #endregion
            }

            ChangeDirection(deltaX);//flip sprite depending on its movement direction

            vel = new Vector2(deltaX, deltaY);
            rigidbody.velocity = vel;
            //update animator
            playerAnim.SetVelocity(vel);
            playerAnim.SetPlayerDash(_dashing);
            playerAnim.SetJump(_jumping);

        }

        else {

            if (PlayerStatus.player.health > 0) {
                vel = Vector2.zero;
                playerAnim.SetVelocity(vel);
                rigidbody.velocity = vel;
            }
            else {
                SetGrounded(); //check if is hitting ground
                if (isTouchingGround) {
                    playerAnim.SetVelocity(rigidbody.velocity);
                    rigidbody.velocity = Vector2.zero;
                }
                else {
                    playerAnim.SetVelocity(rigidbody.velocity);
                }
                
            }
        }

    }

    private void SetGrounded() {
        int i = Physics2D.OverlapCollider(feetCollider, colFilter, results);
        if (i > 0) {
            isTouchingGround = true;
        }
        else {
            isTouchingGround = false;
        }
    }

    private void Jump() {
        _jumping = true;
        isTouchingGround = false;
    }

    private void ChangeDirection(float delta) {

        if (delta < 0 || (transform.localScale.x < 0 && delta == 0)) {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

    }

    public void FreezeMovement() {
        _freezeMovement = true;
        rigidbody.gravityScale = 0;
    }

    public void UnfreezeMovement() {
        if (_freezeMovement) {
            _freezeMovement = false;
            rigidbody.gravityScale = 1;
        }
    }

    public bool GetFreezeMovement() {
        return _freezeMovement;
    }

    public void StopDash() {
        _dashing = false;
        _jumping = false;
        playerAnim.SetJump(false);

        deltaY = 0;
        rigidbody.gravityScale = 1f;

        playerAnim.SetPlayerDash(false);
        _dashTimer = 0;
    }

    public IEnumerator MiniFreezePlayer() {
        if (!_freezeMovement) { 
            FreezeMovement();
            yield return new WaitForSeconds(0.1f);
            UnfreezeMovement();
        }
    }

    public IEnumerator StartDashCD() {
        while (_dashTimer < _dashCD) {
            _dashTimer += Time.deltaTime;
            yield return null;
        }
        _dashTimer = 0;
    }

}
