using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpokeyShooterMovement : MonoBehaviour
{
    public LayerMask groundLayer;

    private SpokeyShooterEnemy _spokey;

    private float _minimumTargetDistance = 0.1f;
    private float _xOffset = 0.5F;

    private float wanderTimer = 2f;
    private float timeToWait = 3f;

    private Collider2D _collider;

    public float wanderRange = 2.5f;

    private Vector2 _wanderPos;
    private Vector2 _oldWanderPos;

    private HumanoidAnimations _enemyAnim;

    // Start is called before the first frame update
    void Start() {
        _spokey = GetComponent<SpokeyShooterEnemy>();

        SetWanderDestination();

        _oldWanderPos = transform.position;
        _wanderPos = transform.position;

        _enemyAnim = GetComponent<HumanoidAnimations>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update() {
        if (_spokey.health > 0 && !_spokey.spokeyCombat.isSpoked) {
            if (!_spokey.spokeyCombat.inSpookRange && !_spokey.spokeyCombat.isSpooking) { // player is not in vision 
                Wander();
            }
            else {
                if (_spokey.spokeyCombat.inSpookRange || _spokey.spokeyCombat.isSpooking) { // player is in vision
                    _enemyAnim.SetVelocity(Vector2.zero);
                }
            }
        }
    }

    private void Wander() {
        if (wanderTimer < 0) {

            if (Vector2.Distance(transform.position, _wanderPos) >= _minimumTargetDistance && !GoingToTheAbyss()) { //TODO Check This
                Movement(_wanderPos);
                ChangeDirection(_wanderPos); //TODO: if enough time is left check for more efficient call
            }
            else {

                wanderTimer = timeToWait;
                _enemyAnim.SetVelocity(Vector2.zero);

                SetWanderDestination();
            }

        }
        else {
            wanderTimer -= Time.deltaTime;
            _enemyAnim.SetVelocity(Vector2.zero);
        }
    }

    private void Movement(Vector2 target) {
        float velX;
        Vector2 velocity;

        velX = Mathf.MoveTowards(transform.position.x, target.x, _spokey.movementSpeed * Time.deltaTime);
        velocity = new Vector2(velX, transform.position.y);

        transform.position = velocity;

        if (transform.position.x <= target.x) {
            _enemyAnim.SetVelocity(new Vector2(_spokey.movementSpeed, transform.position.y));
        }
        else if (transform.position.x > target.x) {
            _enemyAnim.SetVelocity(new Vector2(-_spokey.movementSpeed, transform.position.y));
        }

    }

    private void SetWanderDestination() {

        RaycastHit2D raycastHitRigth;
        RaycastHit2D raycastHitLeft;

        float rigthTarget;
        float leftTarget;

        //raycast rigth
        raycastHitRigth = Physics2D.Raycast(_spokey.spookyEyes.position, Vector2.right, wanderRange, groundLayer);
        //raycast left
        raycastHitLeft = Physics2D.Raycast(_spokey.spookyEyes.position, Vector2.left, wanderRange, groundLayer);

        #region CHECKING WHERE TO PUT WANDER POINTS
        if (raycastHitRigth = Physics2D.Raycast(_spokey.spookyEyes.position, Vector2.right, wanderRange, groundLayer)) {
            if (transform.position.x + _xOffset <= raycastHitRigth.point.x - _xOffset) {
                rigthTarget = Random.Range(transform.position.x + _xOffset, raycastHitRigth.point.x - _xOffset);
            }
            else {
                rigthTarget = Random.Range(transform.position.x, raycastHitRigth.point.x);
            }
        }
        else {
            rigthTarget = Random.Range(transform.position.x + _xOffset, (transform.position.x + wanderRange));
        }

        if (raycastHitLeft = Physics2D.Raycast(_spokey.spookyEyes.position, Vector2.left, wanderRange, groundLayer)) {
            if (transform.position.x - _xOffset >= raycastHitLeft.point.x + _xOffset) {
                leftTarget = Random.Range(transform.position.x - _xOffset, raycastHitLeft.point.x + _xOffset);
            }
            else {
                leftTarget = Random.Range(transform.position.x, raycastHitLeft.point.x);
            }
        }
        else {
            leftTarget = Random.Range(transform.position.x - _xOffset, (transform.position.x - wanderRange));
        }
        #endregion

        #region CHOOSING ONE OF THE TWO POINTS


        //if went rigth last time | go left
        if (GetTransformOldDirection() == 1) {
            _oldWanderPos = _wanderPos;
            _wanderPos = new Vector2(leftTarget, transform.position.y);
        }
        //if went left last time | go rigth
        else if (GetTransformOldDirection() == -1) {
            _oldWanderPos = _wanderPos;
            _wanderPos = new Vector2(rigthTarget, transform.position.y);
        }
        //if is exactly where the last target was random choice
        else {
            if (Random.Range(0, 2) == 1) {
                _wanderPos = new Vector2(rigthTarget, transform.position.y);
            }
            else {
                _wanderPos = new Vector2(leftTarget, transform.position.y);
            }

        }

        #endregion

    }

    public int GetTransformOldDirection() {
        if (_wanderPos.x > _oldWanderPos.x) {
            return 1;
        }
        else if (_wanderPos.x < _oldWanderPos.x) {
            return -1;
        }
        return 0;
    }

    private bool GoingToTheAbyss() {
        RaycastHit2D abyss;

        if (abyss = Physics2D.Raycast(_spokey.bulletSpawnPoint.position, Vector2.down, 0.3f , groundLayer)) {
            return false;
        }
        return true;
    }

    public void ChangeDirection(Vector2 target) {

        if (target.x < transform.position.x) {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

    }

    private void OnDrawGizmosSelected() {
        try {
            Gizmos.color = Color.green;

            Vector3 pointA = _spokey.bulletSpawnPoint.position;
            Vector3 pointB = new Vector3(_spokey.bulletSpawnPoint.position.x, _spokey.bulletSpawnPoint.position.y - 0.3f, _spokey.bulletSpawnPoint.position.z);

            Gizmos.DrawLine(pointA, pointB);
        }
        catch {

        }
    }

}
