using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkelyBoy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(wanderTimer <= 0f) {
            Movement();
        }
        else {
            wanderTimer -= Time.deltaTime;
        }
    }

    public void Movement() {

        velocity.x = Mathf.MoveTowards(velocity.x, movementSpeed * GetTransformDirection(), xAcceleration * Time.deltaTime);
        velocity.y = 0; //Mathf.MoveTowards(velocity.y, moveSpeed * verticalMovement, aceleration * Time.deltaTime);

        transform.Translate(velocity * Time.deltaTime);

        if (Vector2.Distance(transform.position, _target.position) <= _minimumTargetDistance && wanderTimer <= 0) {
            wanderTimer = 2f;
            if (_target.position.x == patrolStartPoint.x) {

                SetTarget(patrolEndPoint);
            }
            else {
                SetTarget(patrolStartPoint);
            }
        }

    }

}
