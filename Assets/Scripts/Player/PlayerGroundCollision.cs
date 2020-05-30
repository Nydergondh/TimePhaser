using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollision : MonoBehaviour
{
    public Collider2D feetCollider;
    public LayerMask thinPlataformLayer;

    public bool isFalling = false;

    private PlayerAnimations playerAnim;

    private Collider2D _plataformCollider;

    //private float _canDuckTimer = 0f;
    //private float _canDuckCD = 0.25f;

    private ContactFilter2D colFilter = new ContactFilter2D();
    private Collider2D[] result = new Collider2D[1];
    private PlatformEffector2D _plataformEffector;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<PlayerAnimations>();
        colFilter.SetLayerMask(thinPlataformLayer);

        _plataformEffector = null;
        _plataformCollider = null;
    }

    // Update is called once per frame
    void Update() {
        if (PlayerStatus.player.health > 0) {
            FallPlataform();
        }
         

    }

    public void FallPlataform() {
        if (Input.GetButton("Duck") && !PlayerStatus.player.playerMovement.GetFreezeMovement()) {

            if (feetCollider.OverlapCollider(colFilter, result) > 0) {
                if (_plataformEffector != null) {
                    _plataformEffector.rotationalOffset = 0f;
                }
                feetCollider.OverlapCollider(colFilter, result);
                _plataformCollider = result[0];
                _plataformEffector = result[0].GetComponent<PlatformEffector2D>();
                SetRotationPlataform(_plataformEffector, true);
                isFalling = true;
            }
            else {
                isFalling = false;
            }

        }

        else if (isFalling) {
            if (feetCollider.OverlapCollider(colFilter, result) > 0) {
                _plataformCollider = result[0];
                _plataformEffector = result[0].GetComponent<PlatformEffector2D>();
                SetRotationPlataform(_plataformEffector, true);
                isFalling = true;
            }
            else {
                isFalling = false;
            }
        }

        else if (Input.GetButton("Jump")) {
            if (_plataformEffector != null) {
                SetRotationPlataform(_plataformEffector, false);
                _plataformEffector = null;
                isFalling = false;
            }
        }
    }

    private void SetRotationPlataform(PlatformEffector2D effector, bool rotatePlat) {
        if (rotatePlat) {
            effector.rotationalOffset = 180f;
        }
        else { 
            effector.rotationalOffset = 0f;
        }
        
    }
}
