using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement player;

    public float runSpeed = 3.5f;
    public float jumpSpeed = 5f;
    public float airSpeed = 5f;
    private bool jumping;

    public float deltaX;

    public float minimumDeltaY = 0.1f;
    public float deltaY;  

    public bool isTouchingGround;

    private bool freezeMovement;

    public LayerMask groundLayer;

    private SpriteRenderer playerRenderer;
    public new Rigidbody2D rigidbody { get; set; }
    public BoxCollider2D feetCollider;

    private PlayerAnimations playerAnim;

    void Awake() {
        if (player == null) {
            player = this;
        }
        else {
            Destroy(player);
            player = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        isTouchingGround = true;

        playerAnim = GetComponent<PlayerAnimations>();
        playerRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update() {

        Movement(); //(TODO: add restriction when attacking)    
        
    }

    private void Movement() {
        Vector2 vel;
        //on ground
        if (Input.GetButton("Horizontal") && deltaY == 0) {
            deltaX = Input.GetAxis("Horizontal") * runSpeed;
        }
        //on air
        else if(Input.GetButton("Horizontal") && deltaY != 0) {
            deltaX = Input.GetAxis("Horizontal") * airSpeed;
        }
        //totaly still (used getaxis so that it conservs a little bit of momentun)
        else {
            deltaX = Input.GetAxis("Horizontal");
        }

        //Jump if on Ground
        if (Input.GetButtonDown("Jump") && isTouchingGround) {
            deltaY = jumpSpeed;
            Jump();
        }
        //Is On Air
        else if ((deltaY > 0 || deltaY < 0)) {

            //Is falling and DID came from a jump state
            if (rigidbody.velocity.y < 0 && !isTouchingGround) {
                jumping = false;
                playerAnim.SetJump(jumping);
            }
            //Is falling and DID NOT came from a jump state
            else if (rigidbody.velocity.y < -minimumDeltaY && isTouchingGround) {
                isTouchingGround = false;
            }

            deltaY = rigidbody.velocity.y; 
        }
        //if was jumping / falling and hitted the ground
        else if (rigidbody.velocity.y < minimumDeltaY && !isTouchingGround) {
            deltaY = 0f;
            playerAnim.SetJump(jumping);
            isTouchingGround = true;
        }
        ////on Ground and not Jumping
        else {
            if (feetCollider.IsTouchingLayers(groundLayer)) {
                deltaY = 0;
            }
            else {
                deltaY = rigidbody.velocity.y;
            }
        }

        ChangeDirection(deltaX);

        vel = new Vector2(deltaX, deltaY);

        playerAnim.SetVelocity(vel);
        rigidbody.velocity = vel;
    }

    private void Jump() {
        jumping = true;
        playerAnim.SetJump(jumping);
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

    public void Fall() {
        isTouchingGround = false;
    }

    public void FreezeMovement() {
        freezeMovement = true;
    }

    public void UnfreezeMovement() {
        freezeMovement = false;
    }
}
