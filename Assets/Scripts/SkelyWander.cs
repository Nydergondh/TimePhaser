using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelyWander : MonoBehaviour
{
    public LayerMask groundLayer;

    private Enemy _enemyComponent;

    private float _minimumTargetDistance = 0.1f;
    private float _xOffset = 0.5F;

    private float wanderTimer = 2f;
    private float timeToWait = 3f;

    private Animator _animator;

    private Collider2D _collider;

    [SerializeField]
    private Transform enemyEyes;
    public float wanderRange = 2.5f;

    private Vector2 _wanderPos;
    private Vector2 _oldWanderPos;

    private HumanoidAnimations _enemyAnim;    

    // Start is called before the first frame update
    void Start()
    {
        SetWanderDestination();
        _oldWanderPos = transform.position;
        _wanderPos = transform.position;

        _enemyComponent = GetComponent<Enemy>();
        _enemyAnim = GetComponent<HumanoidAnimations>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wanderTimer < 0) {

            if(Vector2.Distance(transform.position, _wanderPos) >= _minimumTargetDistance) {
                Movement();
                ChangeDirection();
            }
            else {
                wanderTimer = timeToWait;
                _enemyAnim.SetVelocity(Vector2.zero);

                SetWanderDestination();
            }

        }
        else {
            wanderTimer -= Time.deltaTime;
        }

    }

    private void Movement() {
        float velX;
        Vector2 velocity;
        
        velX = Mathf.MoveTowards(transform.position.x,_wanderPos.x, _enemyComponent.movementSpeed * Time.deltaTime);
        velocity = new Vector2(velX, transform.position.y);

        transform.position = velocity;

        _enemyAnim.SetVelocity(velocity);
    }

    private void SetWanderDestination() {

        RaycastHit2D raycastHitRigth;
        RaycastHit2D raycastHitLeft;

        float rigthTarget;
        float leftTarget;

        //raycast rigth
        raycastHitRigth = Physics2D.Raycast(enemyEyes.position, Vector2.right, wanderRange, groundLayer);
        //raycast left
        raycastHitLeft = Physics2D.Raycast(enemyEyes.position, Vector2.left, wanderRange, groundLayer);

        #region CHECKING WHERE TO PUT WANDER POINTS
        if (raycastHitRigth = Physics2D.Raycast(enemyEyes.position, Vector2.right, wanderRange, groundLayer)) {
            rigthTarget = Random.Range(transform.position.x + _xOffset, raycastHitRigth.point.x - _xOffset);
        }
        else {
            rigthTarget = Random.Range(transform.position.x + _xOffset, (transform.position.x + wanderRange));
        }

        if (raycastHitLeft = Physics2D.Raycast(enemyEyes.position, Vector2.left, wanderRange, groundLayer)) {
            leftTarget = Random.Range(transform.position.x - _xOffset, raycastHitLeft.point.x + _xOffset);
        }
        else {
            leftTarget = Random.Range(transform.position.x - _xOffset, (transform.position.x - wanderRange));
        }
        #endregion

        #region CHOOSING ONE OF THE TWO POINTS


        //if went rigth last time go left
        if (GetTransformOldDirection() == 1) {
            _oldWanderPos = _wanderPos;
            _wanderPos = new Vector2(leftTarget,transform.position.y);
        }
        //if went left last time go rigth
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
        //print(_wanderPos.x);

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



    private void ChangeDirection() {

        if(_wanderPos.x < transform.position.x) {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }

    }

    //private void OnTriggerExit2D(Collider2D collision) {
    //    print("OFF limits! Gonna fall");
    //}
}
