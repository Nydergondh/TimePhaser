using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement player;

    public float runSpeed = 3.5f;
    public float jumpSpeed = 5f;

    public float deltaX;
    public float deltaY;
    public float deltaYMinimum;
    private float jumpingVelocity; // will be used in the future to keep moementum of the jump

    public bool isTouchingGround;

    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    private SpriteRenderer playerRenderer;
    public new Rigidbody2D rigidbody { get; set; }

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
        
        if (isTouchingGround && rigidbody.velocity.y <= 0f) {//0.5f is the maximum (TODO: add restriction when attacking)
            rigidbody.drag = 5f;
        }
        else {
            rigidbody.drag = 0f;
        }

    }

    private void Movement() {
        Vector2 vel;
        //if can move (there are condissions so that you can move)
        if (Input.GetButton("Horizontal")) {
            deltaX = Input.GetAxis("Horizontal") * runSpeed;
        }
        else {
            deltaX = Input.GetAxis("Horizontal");
        }

        //Jump if on Ground
        if (Input.GetButtonDown("Jump")) {
            deltaX = Input.GetAxis("Horizontal") * runSpeed;
            deltaY = jumpSpeed;
        }
        //Change Variables if was jumping and hitted the ground
        else if (rigidbody.velocity.y == 0 && !isTouchingGround) {
            deltaY = 0f;
        }
        //Is On Air
        else if ((rigidbody.velocity.y > 0 || rigidbody.velocity.y < 0)) {
            deltaY = rigidbody.velocity.y; 
            print(deltaY);
        }
        //on Ground and not Jumping
        else {
            deltaY = rigidbody.velocity.y;
        }

        ChangeDirection(deltaX);

        vel = new Vector2(deltaX, deltaY);

        playerAnim.SetVelocity(vel);
        rigidbody.velocity = vel;
    }

    private void ChangeDirection(float delta) {

        if (delta < 0 || (transform.localScale.x < 0 && delta == 0)) {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

    }

    public void CrouchAfterJump() {

    }

    public void FinishCrouchJump() {
        isTouchingGround = true;
    }


    public void Fall() {
        isTouchingGround = false;
    }

    public float GetYSpeed() {
        return deltaY;
    }

}
